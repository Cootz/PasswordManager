using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Passwords.PasData
{
    public static class PasswordController
    {
        private static DBController DB = new DBController();
        public static string path = @"\Passwords";
        private static readonly string PaswFile = "Password.psw";

        public static async Task Initialize()
        {
            path = Directory.GetCurrentDirectory() + path;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await DB.Initialize();
        }

        public static async void SavePasswords(Profile[] Data)
        {            
            foreach (Profile prof in Data)
            {
                await DB.Add(prof);
            }          
        }     

        public static async Task<List<Profile>> SearhProfiles(string keyWord)
        {
            DataSet data = await DB.Select(keyWord);

            return null;
        }
    }
}
