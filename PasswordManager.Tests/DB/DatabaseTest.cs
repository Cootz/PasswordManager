using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.IO;
using Realms;

namespace PasswordManager.Tests.DB
{

    public class DatabaseTest
    {
        static TempStorage? tempStorage;
        static RealmController? controller;
        static DatabaseService? database;

        [SetUp]
        public static void Setup()
        {
            tempStorage = new TempStorage();
            controller = new RealmController(tempStorage);
            database = new(controller);

            database.Initialize().Wait();
        }

        protected void RunTestWithDatabase(Action<DatabaseService> testRun)
        {
            testRun(database!);
        }

        protected async void RunTestWithDatabaseAsync(Func<DatabaseService, Task> testRun)
        {
            await testRun(database!);
        }

        [TearDown]
        public async Task TearDown()
        {
            await controller!.RealmQuerry(async (realm) =>
            {
                DeleteRealmWithRetries(realm);
            });
        }

        protected static bool DeleteRealmWithRetries(Realm realm)
        {
            for (var i = 0; i < 100; i++)
            {
                try
                {
                    Realm.DeleteRealm(realm.Config);
                    return true;
                }
                catch
                {
                    Task.Delay(50).Wait();
                }
            }

            return false;
        }
    }
}
