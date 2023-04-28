using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using Realms;

namespace PasswordManager.Tests.DB;

public class RealmTest : IController
{
    private static ulong realm_verson = 1;

    private readonly RealmConfiguration config = new($"test-db-{Guid.NewGuid()}")
    {
        SchemaVersion = realm_verson
    };

    private Storage dataStorage { get; set; }

    private Realm realm = null!;
    private bool isInitialized = false;

    public RealmTest(Storage storage)
    {
        dataStorage = storage;
        realm = Realm.GetInstance(config);

        realm_verson++;
    }

    public async Task Add<T>(T info) where T : IRealmObject
    {
        await realm.WriteAsync(() => { realm.Add(info); });
    }

    public void Dispose()
    {
        realm = null!;
    }

    public async Task Initialize()
    {
        //Preparing default values
        IQueryable<ServiceInfo>? services = realm.All<ServiceInfo>();
        List<ServiceInfo>? servicesToAdd = new();

        foreach (var service in ServiceInfo.DefaultServices)
            if (realm.Find<ServiceInfo>(service.ID) is null)
                servicesToAdd.Add(service);

        if (servicesToAdd.Count > 0) await realm.WriteAsync(() => servicesToAdd.ForEach(s => realm.Add(s)));

        isInitialized = true;
    }

    public bool IsInitialized()
    {
        return isInitialized;
    }

    public Task Refresh()
    {
        return realm.RefreshAsync();
    }

    public Task Remove<T>(T info) where T : IRealmObject
    {
        return realm.WriteAsync(() => realm.Remove(info));
    }

    public IQueryable<T> Select<T>() where T : IRealmObject
    {
        return realm.All<T>();
    }
}