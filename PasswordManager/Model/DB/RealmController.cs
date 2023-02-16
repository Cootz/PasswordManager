using Microsoft.Extensions.Logging;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using Realms;
using System.Diagnostics;

namespace PasswordManager.Model.DB
{
    public sealed class RealmController : IController
    {
        private const ulong schema_version = 3;

        private Realm realm;

        private bool isInitialized = false;

        public bool IsInitialized() => isInitialized;

        public RealmController() 
        {
            Initialize().Wait();
        }

        public async Task Add(Profile profile)
        {
            await realm.WriteAsync(() =>
            {
                realm.Add(profile);
            });
        }

        public void Dispose()
        {
            realm.Dispose();
        }

        public async Task Initialize()
        {
            //Prevent double initialization
            if (IsInitialized())
                return;

            var config = new RealmConfiguration(Path.Combine(AppDirectoryManager.Data, "Psw.realm"))
            {
                SchemaVersion = schema_version,
                MigrationCallback = OnMigration
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
            var services = realm.All<Service>();
            var servicesToAdd = new List<Service>();

            foreach (var service in Service.defaultServices)
                if (realm.Find<Service>(service.ID) is null)
                    servicesToAdd.Add(service);

            if (servicesToAdd.Count > 0)
                await realm.WriteAsync(() => servicesToAdd.ForEach(s => realm.Add(s)));

            Debug.WriteLine(realm.All<Service>().FirstOrDefault().Name);

            isInitialized = true;
        }

        private void OnMigration(Migration migration, ulong lastSchemaVersion)
        {
            for (ulong i = 1; i <= schema_version; i++)
                applyMigrationsForVestions(migration, (ulong)i);
        }

        private void applyMigrationsForVestions(Migration migration, ulong targetVersion)
        {
            switch (targetVersion)
            {
                case 2:
                    var newProfiles = migration.NewRealm.All<Profile>();

                    for (int i = 0; i < newProfiles.Count(); i++)
                        newProfiles.ElementAt(i).ID = Guid.NewGuid();

                    break;
            }
        }

        public IQueryable<T> Select<T>() where T : IRealmObject => realm?.All<T>();

        public Task Remove(Profile profile) => realm?.WriteAsync(() => realm.Remove(profile));
    }
}
