using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    public static class PasswordController
    {
        private static DBController DB = new DBController();
        public static string path = @"\Passwords";
        private static readonly string PaswFile = "Password.psw";

        public static void Initialize()
        {
            path = Directory.GetCurrentDirectory() + path;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DB.Initialize();
        }

        public static async void SavePasswords(Profile[] Data)
        {            
            foreach (Profile prof in Data)
            {
                await DB.Add(prof);
            }          
        }     

        public static List<Profile> SearhProfiles(string keyWord)
        {
            DB.Select(keyWord);

            return null;
        }
    }
}
