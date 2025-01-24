using NSubstitute;
using PasswordManager.Model.DB;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.IO;

namespace PasswordManager.Tests.DB;

[TestFixture]
[NonParallelizable]
public class DatabaseTest
{
    private static TempStorage? tempStorage;
    private static ISecureStorage? secureStorage;

    // This property cannot be disposed because Realm can only have one instance per application.
    // Once this instance disposed Realm will throw an exception if you try to create a new instance of Realm.
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
    private static RealmController? controller;
    private static DatabaseService? database;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

    [OneTimeSetUp]
    public static void Setup()
    {
        if (tempStorage is not null)
            return;

        secureStorage = Substitute.For<ISecureStorage>();
        secureStorage.GetAsync("realm_key").Returns(Task.FromResult(@"PeShVmYq3t6w9z$C&F)J@McQfTjWnZr4"));

        tempStorage = new TempStorage();
        controller = new RealmController(tempStorage, secureStorage);
        database = new DatabaseService(controller);
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
        await controller!.RealmQueryAsync(async (realm) =>
        {
            await realm.WriteAsync(() =>
            {
                realm.RemoveAll<ProfileInfo>();

                foreach (ServiceInfo? service in realm.All<ServiceInfo>())
                    if (!ServiceInfo.DefaultServices.Contains(service))
                        realm.Remove(service);
            });

            while (realm.IsInTransaction) await Task.Delay(50);

            realm.Refresh();
        });
    }
}