using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;
using Remotion.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.DB
{
    public class InMemoryRealm : IController
    {
        private Realm realm = null!;
        private bool isInitialized = false;

        public InMemoryRealm()
        {
            var config = new InMemoryConfiguration("test-db");
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

        public Task Initialize()
        {
            isInitialized = true;
            return Task.CompletedTask;
        }

        public bool IsInitialized() => isInitialized; 

        public Task Remove(Profile profile) => realm.WriteAsync(() => realm.Remove(profile));

        public IQueryable<T> Select<T>() where T : IRealmObject => realm.All<T>();
    }
}
