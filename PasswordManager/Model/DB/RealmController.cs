using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model.DB
{
    public class RealmController : IController
    {
        private Realm realm;

        public RealmController() { }

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
            var config = new RealmConfiguration(Path.Combine(AppDirectoryManager.AppData, "Psw.realm"));
            realm = Realm.GetInstance(config);
        }

        public IEnumerable<Profile> Select(Func<Profile, bool> predicate)
        {
            return realm?.All<Profile>()?.Where(predicate);
        }
    }
}
