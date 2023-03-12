using PasswordManager.Model.DB;
using PasswordManager.Model.IO;
using PasswordManager.Services;
using PasswordManager.Tests.IO;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.DB
{
    [TestFixture]
    public class DatabaseTest
    {
        static TempStorage tempStorage;
        static RealmController controller;
        static DatabaseService database;

        [OneTimeSetUp]
        public static void Setup()
        {
            tempStorage = new TempStorage();
            controller = new RealmController(tempStorage);
            database = new(controller);

            database.Initialize().Wait();
        }

        protected void RunTestWithDatabase(Action<DatabaseService> testRun)
        {
            testRun(database);
            
            
        }

        protected async void RunTestWithDatabaseAsync(Func<DatabaseService, Task> testRun)
        {
            await testRun(database);
        }
    }
}
