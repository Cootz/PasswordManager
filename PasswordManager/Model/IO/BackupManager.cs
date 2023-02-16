namespace PasswordManager.Model.IO
{
    public static class BackupManager
    {
        public static void Backup(FileInfo file)
        {
            if (file.Exists)
            {
                file.CopyTo($"{file.FullName}.bak");
            }
            else
                throw new Exception($"File {file.Name} does not exist. Backup is failed!");
        }
    }
}
