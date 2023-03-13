namespace PasswordManager.Model.IO
{
    /// <summary>
    /// Defines storage
    /// </summary>
    public abstract class Storage
    {
        public string WorkingDirectory { get; private set; }

        public Storage(string path)
        {
            var dir = Directory.CreateDirectory(path);

            WorkingDirectory = path;
        }

        public abstract Storage GetStorageForDirectory(string directory);
    }
}
