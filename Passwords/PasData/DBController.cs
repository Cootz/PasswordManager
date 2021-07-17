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
        private static readonly string dbname = PasswordController.path + @"\PswDB.sdf";
        private static readonly string com = $"DataSource=\"{dbname}\"; Password=\"scpassw1\"";
        private SqlCeEngine eng;
        private SqlCeConnection connection;
        private SqlCeCommand cmd;
        private int _id;

        private async Task opencon()
        {
            connection = new SqlCeConnection(com);
            await connection.OpenAsync();
        }

        private async Task<DataSet> createcommand(string command)
        {
            cmd = new SqlCeCommand(command, connection);
            await cmd.ExecuteNonQueryAsync();
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        public async void Initialize()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + dbname))
            {
                eng = new SqlCeEngine(com);
                eng.CreateDatabase();

                await opencon();

                await createcommand("CREATE TABLE Profiles " +
                       "(ProfileID int NOT NULL," +
                       "Service Ntext NOT NULL," +
                       "Email Ntext NOT NULL," +
                       "Password Ntext NOT NULL," +
                       "Username Ntext);");
            }
            else
            {
                await opencon();
            }
            
        }

        public async Task Add(Profile profile)
        {
            await createcommand("INSERT INTO Profiles (ProfileID, Service, Email, Password, Username)" +
                $"VALUES (\'{_id}\', \'{profile.Service}\',\'{profile.Email.Adress}\',\'{profile.Password}\',\'{profile.Username}\')");

            _id++;
        }

        public async Task<List<Profile>> Select(string condition)
        {
            List<Profile> profiles = new List<Profile>();

            DataSet dataset = await createcommand("SELECT * IN Profiles " +
                "WHERE " + condition);

            if (true) { }

            return new List<Profile>();
        }


    }
}
