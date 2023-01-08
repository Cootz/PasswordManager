using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using PasswordManager.Model.DB.Schema;

namespace PasswordManager.Model.DB
{
    public static class PasswordController
    {
        private static IController DB = new DBController();
        public static string Path = @"\Passwords";

        public static async Task Initialize()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path;

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            await DB.Initialize();
        }

        public static async void SavePasswords(Profile[] data)
        {
            foreach (Profile prof in data)
            {
                await DB.Add(prof);
            }
        }

        public static async Task<List<Profile>> SearhProfiles(string keyWord)
        {
            List<Profile> profiles = new List<Profile>();

            DataSet data = await DB.Select(keyWord);

            DataRowCollection dataRows = data.Tables[0].Rows;

            foreach (DataRow row in dataRows)
            {
                object[] items = row.ItemArray;

                Profile profile = new Profile();

                profile.Service = (string)items[1];
                profile.Email = new EMail() { Adress = (string)items[2] };
                profile.Password = (string)items[3];
                profile.Username = (string)items[4];

                profiles.Add(profile);
            }

            return profiles;
        }
    }
}
