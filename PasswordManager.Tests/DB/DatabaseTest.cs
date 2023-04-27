using NSubstitute;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.IO;

namespace PasswordManager.Tests.DB
{
    [TestFixture]
    [NonParallelizable]
    public class DatabaseTest
    {
        private static TempStorage? tempStorage;
        private static ISecureStorage? secureStorage;
        private static RealmController? controller;
        private static DatabaseService? database;

        [OneTimeSetUp]
        public static void Setup()
        {
            if (tempStorage is null)
            {
                secureStorage = Substitute.For<ISecureStorage>();
                secureStorage.GetAsync("realm_key").Returns(Task.FromResult(@"PeShVmYq3t6w9z$C&F)J@McQfTjWnZr4"));

                tempStorage = new TempStorage();
                controller = new RealmController(tempStorage, secureStorage);
                database = new DatabaseService(controller);

                database.Initialize().Wait();
            }
        }

        protected void RunTestWithDatabase(Action<DatabaseService> testRun) => testRun(database!);

        protected async void RunTestWithDatabaseAsync(Func<DatabaseService, Task> testRun) => await testRun(database!);

        [TearDown]
        public async Task TearDown() =>
            await controller!.RealmQuerry(async (realm) =>
            {
                await realm.WriteAsync(() =>
                {
                    realm.RemoveAll<ProfileInfo>();

                    foreach (ServiceInfo service in realm.All<ServiceInfo>())
                    {
                        if (!ServiceInfo.DefaultServices.Contains(service))
                        {
                            realm.Remove(service);
                        }
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