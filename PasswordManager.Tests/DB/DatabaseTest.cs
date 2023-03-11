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
        protected void RunTestWithDatabase(Action<DatabaseService> testRun)
        {
            TempStorage tempStorage = new TempStorage();
            IController controller = new RealmController(tempStorage);
            
            DatabaseService database = new(controller);
           
            database.Initialize().Wait();
            testRun(database);
        }

        protected async void RunTestWithDatabaseAsync(Func<DatabaseService, Task> testRun)
        {
            TempStorage tempStorage = new TempStorage();
            using IController controller = new RealmController(tempStorage);
            using DatabaseService database = new(controller);
            database.Initialize().Wait();
            await testRun(database);
        }
    }
}
