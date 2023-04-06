using NUnit.Framework;
using PasswordManager.Model.IO;

namespace PasswordManager.Tests.IO
{
    [TestFixture]
    public class BackupManagerTest
    {
        const string FILENAME = "testfile.txt";
        const string DIFFERENT_PATH_MESSAGE = "File saved in a different directory";

        [Test]
        public void BackupFileOnseTest()
        {
            TempStorage storage = new TempStorage();
            string filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

            File.Create(filePath).Dispose();

            var fileinfo = new FileInfo(filePath);

            var backupFileInfo = BackupManager.Backup(fileinfo);

            Assert.That(backupFileInfo, Is.Not.Null);
            Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
            Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(fileinfo.DirectoryName), message: DIFFERENT_PATH_MESSAGE);
        }

        [Test]
        public void BackupFileMultipleTimesTest()
        {
            TempStorage storage = new TempStorage();
            string filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

            File.Create(filePath).Dispose();

            var fileinfo = new FileInfo(filePath);

            List<FileInfo> backupFileInfos = new();

            for (int i = 0; i < 100; i++)
                backupFileInfos.Add(BackupManager.Backup(fileinfo));

            foreach (var backupFileInfo in backupFileInfos)
            {
                Assert.That(backupFileInfo, Is.Not.Null);
                Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
                Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(fileinfo.DirectoryName), message: DIFFERENT_PATH_MESSAGE);
            }
        }

        [Test]
        public void BackupNonExistingFile()
        {
            TempStorage storage = new TempStorage();
            string filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

            var fileinfo = new FileInfo(filePath);

            Assert.Throws<FileNotFoundException>(() => BackupManager.Backup(fileinfo));
        }

        [Test]
        public void BackupFileInADifferentDirectory()
        {
            TempStorage storage = new TempStorage();

            TempStorage backupStorage = (TempStorage)storage.GetStorageForDirectory("backup");

            string filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

            File.Create(filePath).Dispose();

            var fileinfo = new FileInfo(filePath);

            var backupFileInfo = BackupManager.Backup(fileinfo, backupStorage);

            Assert.That(backupFileInfo, Is.Not.Null);
            Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
            Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(backupStorage.WorkingDirectory), message: DIFFERENT_PATH_MESSAGE);
        }
    }
}
