using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model.Storage
{
    public class AppStorage : Storage
    {
        public AppStorage(string path) : base(path) { }

        public override Storage GetStorageForDirectory(string directory) => new AppStorage(Path.Combine(WorkingDirectory, directory));
    }
}
