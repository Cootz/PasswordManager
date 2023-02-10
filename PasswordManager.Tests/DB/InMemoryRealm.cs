using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.DB
{
    public class InMemoryRealm : IController
    {
        private Realm realm = null;

        public InMemoryRealm()
        {
            var config = new InMemoryConfiguration("test-db");
            var realm = Realm.GetInstance(config);
        }

        public async Task Add(Profile profile)
        {
            await realm.WriteAsync(() =>
            {
                realm.Add(profile);
            });
        }

        public void Dispose() => realm.Dispose();

        public Task Initialize() => Task.CompletedTask;

        public Task Remove(Profile profile) => realm.WriteAsync(() => realm.Remove(profile));

        public IQueryable<T> Select<T>() where T : class => typeof(T) switch
        {
            var value when value == typeof(Profile) => (IQueryable<T>)realm?.All<Profile>(),
            _ => null
        };
    }
}
