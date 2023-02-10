using PasswordManager.Model.DB.Schema;
using PasswordManager.Model.IO;
using Realms;

namespace PasswordManager.Model.DB
{
    public class RealmController : IController
    {
        private const ulong schema_version = 2;

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

        public Task Initialize()
        {
            var config = new RealmConfiguration(Path.Combine(AppDirectoryManager.Data, "Psw.realm"))
            { 
                SchemaVersion = schema_version,
                MigrationCallback = OnMigration
            };

            realm = Realm.GetInstance(config);

            return Task.CompletedTask;
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

        public IQueryable<T> Select<T>()
            where T : class
        {
            var type = typeof(T);
            return type switch
            {
                var value when value == typeof(Profile) => (IQueryable<T>)realm?.All<Profile>(),
                _ => null
            };
        }

        public Task Remove(Profile profile) => realm?.WriteAsync(() => realm.Remove(profile));
    }
}
