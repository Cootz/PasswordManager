using PasswordManager.Model.DB.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model.DB
{
    internal interface IController
    {
        public Task Initialize();
        public Task<DataSet> Select(string condition);
        public Task Add(Profile profile);
    }
}
