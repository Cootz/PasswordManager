using PasswordManager.Model.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.IO
{
    public class TempStorage : Storage
    {
        public TempStorage() : base(Path.Combine(Path.GetTempPath(), $"{MauiProgram.AppName}-{Guid.NewGuid().ToString()}")) { }
        public TempStorage(string directory) : base(directory) { }

        public override Storage GetStorageForDirectory(string directory) => new TempStorage(Path.Combine(WorkingDirectory, directory));
    }
}
