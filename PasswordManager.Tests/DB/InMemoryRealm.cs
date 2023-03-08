using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;

namespace PasswordManager.Tests.DB
{
    public class InMemoryRealm : IController
    {
        private readonly InMemoryConfiguration config = new($"test-db-{Guid.NewGuid()}");

        private Realm realm = null!;
        private bool isInitialized = false;

        public InMemoryRealm() => realm = Realm.GetInstance(config);

        public async Task Add<T>(T info) where T : IRealmObject
        {
            await realm.WriteAsync(() =>
            {
                realm.Add(info);
            });
        }

        public void Dispose()
        {
            realm.Dispose();

            Realm.DeleteRealm(config);
        }

        public async Task Initialize()
        {
            //Preparing default values
            var services = realm.All<ServiceInfo>();
            var servicesToAdd = new List<ServiceInfo>();

            foreach (var service in ServiceInfo.DefaultServices)
                if (realm.Find<ServiceInfo>(service.ID) is null)
                    servicesToAdd.Add(service);

            if (servicesToAdd.Count > 0)
                await realm.WriteAsync(() => servicesToAdd.ForEach(s => realm.Add(s)));

            isInitialized = true;
        }

        public bool IsInitialized() => isInitialized; 

        public Task Remove<T>(T info) where T : IRealmObject => realm.WriteAsync(() => realm.Remove(info));

        public IQueryable<T> Select<T>() where T : IRealmObject => realm.All<T>();
    }
}
