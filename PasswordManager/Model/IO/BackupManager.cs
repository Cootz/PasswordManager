namespace PasswordManager.Model.IO
{
    /// <summary>
    /// Handles backup things
    /// </summary>
    public static class BackupManager
    {
        public const string DateTimeFormat = "yyyy-M-dd--HH-mm-ss-ffff";

        /// <summary>
        /// Creates copy with .bak extension form given file 
        /// </summary>
        /// <param name="file">File to backup</param>
        /// <param name="externalStorage">Storage with directory to save backup in. If null file copies to the directory of given file</param>
        /// <returns><see cref="FileInfo"/> of created backup</returns>
        public static FileInfo Backup(FileInfo file, Storage externalStorage = null)
        {
            if (file.Exists)
            {
                if (externalStorage is not null)
                    return file.CopyTo(Path.Combine(externalStorage.WorkingDirectory, $"{file.Name}-{DateTime.Now.ToString(DateTimeFormat)}.bak"));
                else
                    return file.CopyTo($"{file.FullName}-{DateTime.Now.ToString(DateTimeFormat)}.bak");
            }
            else
                throw new FileNotFoundException($"File {file.Name} does not exist. Backup is failed!");
        }
    }
}
