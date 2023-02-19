using System.IO;

namespace PasswordManager.Model.IO
{
    /// <summary>
    /// Handles backup things
    /// </summary>
    public static class BackupManager
    {
        /// <summary>
        /// Creates copy with .bak extension form given file 
        /// </summary>
        /// <param name="file">File to backup</param>
        /// <param name="externalDirectory">Directory to save backup in. If null file copies to the directory of given file</param>
        public static void Backup(FileInfo file, Storage externalStorage = null)
        {
            if (file.Exists)
            {
                if (externalStorage is not null)
                    file.CopyTo(Path.Combine(externalStorage.WorkingDirectory, $"{file.Name}.bak"));
                else
                    file.CopyTo($"{file.FullName}.bak");
            }
            else
                throw new FileNotFoundException($"File {file.Name} does not exist. Backup is failed!");
        }
    }
}
