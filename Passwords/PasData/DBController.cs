using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity.SqlServerCompact;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;

namespace Passwords.PasData
{
    public class DBController
    {
        private static readonly string DBName = PasswordController.Path + @"\PswDB.sdf";
        private static readonly string Com = $"DataSource=\"{DBName}\"; Password=\"scpassw1\"";
        private SqlCeEngine _engine;
        private SqlCeConnection _connection;
        private SqlCeCommand _cmd;
        private int _id;

        private async Task OpenCon()
        {
            _connection = new SqlCeConnection(Com);
            await _connection.OpenAsync();
        }

        private async Task<DataSet> CreateCommand(string command)
        {
            _cmd = new SqlCeCommand(command, _connection);
            await _cmd.ExecuteNonQueryAsync();
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(_cmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        public async Task Initialize()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + DBName))
            {
                try
                {
                    _engine = new SqlCeEngine(Com);
                    _engine.CreateDatabase();
                }
                catch { }
                
                await OpenCon();

                try
                {
                    await CreateCommand("CREATE TABLE Profiles " +
                           "(ProfileID int NOT NULL UNIQIE," +
                           "Service nvarchar(4000) NOT NULL," +
                           "Email nvarchar(4000) NOT NULL," +
                           "Password nvarchar(4000) NOT NULL," +
                           "Username nvarchar(4000));");
                }
                catch { }
            }
            else
            {
                await OpenCon();
            }
            
        }

        public async Task Add(Profile profile)
        {
            await CreateCommand("INSERT INTO Profiles (ProfileID, Service, Email, Password, Username)" +
                $"VALUES (\'{_id}\', \'{profile.Service}\',\'{profile.Email.Adress}\',\'{profile.Password}\',\'{profile.Username}\')");

            _id++;
        }

        public async Task<DataSet> Select(string condition)
        {
            DataSet dataSet = await CreateCommand("SELECT * FROM Profiles " +
                "WHERE " + condition + ";");
           
            return dataSet;
        }


    }
}
