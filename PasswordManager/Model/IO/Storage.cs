using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
