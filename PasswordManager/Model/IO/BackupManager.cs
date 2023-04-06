using System.IO.Compression;

namespace PasswordManager.Model.IO
{
    /// <summary>
    /// Handles backup things
    /// </summary>
    public static class BackupManager
    {
        public const string DateTimeFormat = "yyyy-M-dd--HH-mm-ss-ffff";

        /// <summary>
        /// Creates copy of given file and compress it to zip archive
        /// </summary>
        /// <param name="file">File to backup</param>
        /// <param name="externalStorage">Storage with directory to save backup in. If null file copies to the directory of given file</param>
        /// <returns><see cref="FileInfo"/> of created backup</returns>
        public static FileInfo Backup(FileInfo file, Storage externalStorage = null)
        {
            if (file.Exists)
            {
                string fileName = $"{file.Name}-{DateTime.Now.ToString(DateTimeFormat)}.zip";
                string archivePath = Path.Combine(externalStorage?.WorkingDirectory ?? file.DirectoryName, fileName);

                using (ZipArchive zip = ZipFile.Open(archivePath, ZipArchiveMode.Create))
                    zip.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Optimal);

                return new FileInfo(archivePath);
            }
            else
                throw new FileNotFoundException($"File {file.Name} does not exist. Backup is failed!");
        }
    }
}
