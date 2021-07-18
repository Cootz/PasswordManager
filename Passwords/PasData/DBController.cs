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

        public async Task Initialize()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + dbname))
            {
                try
                {
                    eng = new SqlCeEngine(com);
                    eng.CreateDatabase();
                }
                catch { }
                
                await opencon();

                try
                {
                    await createcommand("CREATE TABLE Profiles " +
                           "(ProfileID int NOT NULL," +
                           "Service nvarchar(4000) NOT NULL," +
                           "Email nvarchar(4000) NOT NULL," +
                           "Password nvarchar(4000) NOT NULL," +
                           "Username nvarchar(4000));");
                }
                catch(Exception e) { }
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

        public async Task<DataSet> Select(string condition)
        {
            List<Profile> profiles = new List<Profile>();

            DataSet dataset = await createcommand("SELECT * FROM Profiles " +
                "WHERE " + condition + ";");

            return dataset;
        }


    }
}
