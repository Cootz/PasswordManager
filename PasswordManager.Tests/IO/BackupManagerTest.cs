using PasswordManager.Model.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Tests.IO
{
    [TestFixture]
    public class BackupManagerTest
    {
        const string filename = "testfile.txt";

        [Test]
        public void BackupFileOnseTest()
        { 
            TempStorage storage = new TempStorage();
            string filePath = Path.Combine(storage.WorkingDirectory, filename);

            File.Create(filePath).Dispose();

            var fileinfo = new FileInfo(filePath);

            var backupFileInfo = BackupManager.Backup(fileinfo);

            Assert.That(backupFileInfo, Is.Not.Null);
            Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
        }

        [Test]
        public void BackupFileMultipleTimesTest()
        {
            TempStorage storage = new TempStorage();
            string filePath = Path.Combine(storage.WorkingDirectory, filename);

            File.Create(filePath).Dispose();

            var fileinfo = new FileInfo(filePath);

            List<FileInfo> backupFileInfos = new();
            
            for (int i = 0; i < 100; i++)
                 backupFileInfos.Add(BackupManager.Backup(fileinfo));

            foreach (var backupFileInfo in backupFileInfos)
            {
                Assert.That(backupFileInfo, Is.Not.Null);
                Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
            }
        }

        [Test]
        public void BackupNoExistingFile()
        {
            TempStorage storage = new TempStorage();
            string filePath = Path.Combine(storage.WorkingDirectory, filename);

            var fileinfo = new FileInfo(filePath);

            Assert.Throws<FileNotFoundException>(() => BackupManager.Backup(fileinfo));
        }
    }
}
