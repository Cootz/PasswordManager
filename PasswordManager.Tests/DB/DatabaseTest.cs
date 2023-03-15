using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.IO;

namespace PasswordManager.Tests.DB
{
    [TestFixture]
    public class DatabaseTest
    {
        static TempStorage? tempStorage;
        static RealmController? controller;
        static DatabaseService? database;

        [OneTimeSetUp]
        public static void Setup()
        {
            if (tempStorage is null)
            {
                tempStorage = new TempStorage();
                controller = new RealmController(tempStorage);
                database = new(controller);

                database.Initialize().Wait();
            }
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
            await controller!.RealmQuerry(async (realm) => {
                await realm.WriteAsync(() =>
                {
                    realm.RemoveAll<ProfileInfo>();

                    foreach (ServiceInfo service in realm.All<ServiceInfo>())
                    { 
                        if (!ServiceInfo.DefaultServices.Contains(service))
                            realm.Remove(service);
                    }
                });

                while (realm.IsInTransaction)
                {
                    await Task.Delay(50);
                }

                realm.Refresh();
            });
        }
    }
}
