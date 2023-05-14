using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using PasswordManager.Utils;
using Realms;
using System.Diagnostics;

// ReSharper disable MethodHasAsyncOverload

namespace PasswordManager.Model.DB;

/// <summary>
/// Handles data transferring to/from realm
/// </summary>
public sealed class RealmController : IController
{
    /// <summary>
    /// Version     Changes
    /// 3       -   Update <see cref="ServiceInfo"/> and <see cref="ProfileInfo"/> after migration from Sqlite
    /// </summary>
    private const ulong schema_version = 3;

    private readonly Storage dataStorage;

    private readonly ISecureStorage secureStorage;

    private Realm realm;

    public RealmController(Storage storage, ISecureStorage secureStorage)
    {
        this.secureStorage = secureStorage;
        dataStorage = storage.GetStorageForDirectory("data");

        Initialization = InitializeAsync();
    }

    public async Task Add<T>(T info) where T : IRealmObject => await realm.WriteAsync(() => { realm.Add(info); });

    public void Dispose() => realm.Dispose();

    public Task Initialization { get; private set; }

    public async Task InitializeAsync()
    {
        //Prevent double initialization
        if (Initialization.IsCompletedSuccessfully) return;

        byte[] key = new byte[64];

        string encKeyString = await secureStorage.GetAsync("realm_key");

        if (encKeyString == null || encKeyString.ToKey().Length != 64)
        {
            string databasePath = Path.Combine(dataStorage.WorkingDirectory, "data.realm");

            if (File.Exists(databasePath))
            {
                BackupManager.Backup(new FileInfo(databasePath));
                File.Delete(databasePath);
            }

            key = EncryptionHelper.GenerateKey();
            await secureStorage.SetAsync("realm_key", key.ToKeyString());
        }
        else
        {
            key = encKeyString.ToKey();
        }

        Debug.Assert(key.Length == 64);

        RealmConfiguration config = new(Path.Combine(dataStorage.WorkingDirectory, "data.realm"))
        {
            SchemaVersion = schema_version,
            MigrationCallback = OnMigration,
            EncryptionKey = key
        };

        try
        {
            realm = Realm.GetInstance(config);
        }
        catch
        {
            string databasePath = config.DatabasePath;

            BackupManager.Backup(new FileInfo(databasePath));
            File.Delete(databasePath);

            realm = Realm.GetInstance(config);
        }

        //Preparing default values
        List<ServiceInfo> servicesToAdd = ServiceInfo.DefaultServices
            .Where(service => realm.Find<ServiceInfo>(service.ID) is null)
            .ToList();

        if (servicesToAdd.Count > 0)
            realm.Write(() =>
            {
                foreach (ServiceInfo service in servicesToAdd) realm.Add(service);
            });

        Debug.Assert(realm.All<ServiceInfo>().ToArray().Intersect(ServiceInfo.DefaultServices).Count()
                     == ServiceInfo.DefaultServices.Length);
    }

    private void OnMigration(Migration migration, ulong lastSchemaVersion)
    {
        for (ulong i = 1; i <= schema_version; i++) applyMigrationsForVersions(migration, i);
    }

    private void applyMigrationsForVersions(Migration migration, ulong targetVersion)
    {
        switch (targetVersion)
        {
            case 2:
                IQueryable<ProfileInfo> newProfiles = migration.NewRealm.All<ProfileInfo>();

                for (int i = 0; i < newProfiles.Count(); i++) newProfiles.ElementAt(i).ID = Guid.NewGuid();

                break;
        }
    }

    public IQueryable<T> Select<T>() where T : IRealmObject => realm.All<T>();

    public Task Refresh() => realm.RefreshAsync();

    public async Task RealmQuery(Func<Realm, Task> action)
    {
        await action(realm);
    }

    public Task Remove<T>(T info) where T : IRealmObject
    {
        return realm?.WriteAsync(() => realm.Remove(info));
    }
}