using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Passwords.PasData
{
    public static class PasswordManager
    {
        private static char FieldSplit = ':';
        private static char ProfileSplit = ';';

        private static string path = @"\Passwords";
        private static string PaswFile = "Password.JSON";

        public static void Initialize()
        {
            path = Directory.GetCurrentDirectory() + path;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                File.Create(path + @"\" + PaswFile);
            }

        }

        private static void SavePasswords(List<Profile> Data)
        { 
            
        }


    }
}
