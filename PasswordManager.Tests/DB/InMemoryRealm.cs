using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;

namespace PasswordManager.Tests.DB
{
    public class InMemoryRealm : IController
    {
        private Realm realm = null!;
        private bool isInitialized = false;

        public InMemoryRealm()
        {
            var config = new InMemoryConfiguration($"test-db-{Guid.NewGuid()}");
            realm = Realm.GetInstance(config);
        }

        public async Task Add(Profile profile)
        {
            await realm.WriteAsync(() =>
            {
                realm.Add(profile);
            });
        }

        public void Dispose() => realm.Dispose();

        public async Task Initialize()
        {
            //Preparing default values
            var services = realm.All<Service>();
            var servicesToAdd = new List<Service>();

            foreach (var service in Service.DefaultServices)
                if (realm.Find<Service>(service.ID) is null)
                    servicesToAdd.Add(service);

            if (servicesToAdd.Count > 0)
                await realm.WriteAsync(() => servicesToAdd.ForEach(s => realm.Add(s)));

            isInitialized = true;
        }

        public bool IsInitialized() => isInitialized; 

        public Task Remove(Profile profile) => realm.WriteAsync(() => realm.Remove(profile));

        public IQueryable<T> Select<T>() where T : IRealmObject => realm.All<T>();
    }
}
