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
        public static void Backup(FileInfo file, DirectoryInfo externalDirectory = null)
        {
            if (file.Exists)
            {
                if (externalDirectory is not null && externalDirectory.Exists)
                    file.CopyTo(Path.Combine(externalDirectory.FullName, $"{file.Name}.bak"));
                else
                    file.CopyTo($"{file.FullName}.bak");
            }
            else
                throw new FileNotFoundException($"File {file.Name} does not exist. Backup is failed!");
        }
    }
}
