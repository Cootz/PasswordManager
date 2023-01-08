﻿using System.Data;
using System.Data.SqlServerCe;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB
{
    public class DBController : IController
    {
        private static readonly string DBName = PasswordController.Path + @"\PswDB.sdf";
        private static readonly string Com = $"DataSource=\"{DBName}\"; Password=\"scpassw1\"";
        private SqlCeEngine _engine;
        private SqlCeConnection _connection;
        private SqlCeCommand _cmd;

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
            File.Delete(DBName);

            if (!File.Exists(DBName))
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
                           "(ProfileID int IDENTITY(1,1) PRIMARY KEY," +
                           "Service nvarchar(4000) NOT NULL," +
                           "Email nvarchar(4000) NOT NULL," +
                           "Password nvarchar(4000) NOT NULL," +
                           "Username nvarchar(4000));");
                    _engine.Upgrade();
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
            await CreateCommand("INSERT INTO Profiles (Service, Email, Password, Username)" +
                $"VALUES (\'{profile.Service}\',\'{profile.Email.Adress}\',\'{profile.Password}\',\'{profile.Username}\')");
        }

        public async Task<DataSet> Select(string condition)
        {
            DataSet dataSet = await CreateCommand("SELECT * FROM Profiles " +
                "WHERE " + condition + ";");

            return dataSet;
        }
    }
}
