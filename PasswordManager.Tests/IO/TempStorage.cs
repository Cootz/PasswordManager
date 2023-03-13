using PasswordManager.Model.IO;

namespace PasswordManager.Tests.IO
{
    public class TempStorage : Storage
    {
        public TempStorage() : base(Path.Combine(Path.GetTempPath(), $"{MauiProgram.AppName}-{Guid.NewGuid().ToString()}")) { }
        public TempStorage(string directory) : base(directory) { }

        public override Storage GetStorageForDirectory(string directory) => new TempStorage(Path.Combine(WorkingDirectory, directory));
    }
}
