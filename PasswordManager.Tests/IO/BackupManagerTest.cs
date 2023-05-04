using PasswordManager.Model.IO;

namespace PasswordManager.Tests.IO;

[TestFixture]
public class BackupManagerTest
{
    private const string filename = "testfile.txt";
    private const string different_path_message = "File saved in a different directory";

    [Test]
    public void BackupFileOnceTest()
    {
        TempStorage storage = new();
        string filePath = Path.Combine(storage.WorkingDirectory, filename);

        File.Create(filePath).Dispose();

        FileInfo fileInfo = new(filePath);

        FileInfo? backupFileInfo = BackupManager.Backup(fileInfo);

        Assert.Multiple(() =>
        {
            Assert.That(backupFileInfo, Is.Not.Null);
            Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
            Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(fileInfo.DirectoryName), different_path_message);
        });
    }

    [Test]
    public void BackupFileMultipleTimesTest()
    {
        TempStorage storage = new();
        string filePath = Path.Combine(storage.WorkingDirectory, filename);

        File.Create(filePath).Dispose();

        FileInfo fileInfo = new(filePath);

        List<FileInfo> backupFileInfos = new();

        for (int i = 0; i < 100; i++) backupFileInfos.Add(BackupManager.Backup(fileInfo));

        Assert.Multiple(() =>
        {
            foreach (FileInfo? backupFileInfo in backupFileInfos)
            {
                Assert.That(backupFileInfo, Is.Not.Null);
                Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
                Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(fileInfo.DirectoryName), different_path_message);
            }
        });
    }

    [Test]
    public void BackupNonExistingFile()
    {
        TempStorage storage = new();
        string filePath = Path.Combine(storage.WorkingDirectory, filename);

        FileInfo fileInfo = new(filePath);

        Assert.Throws<FileNotFoundException>(() => BackupManager.Backup(fileInfo));
    }

    [Test]
    public void BackupFileInADifferentDirectory()
    {
        TempStorage storage = new();

        TempStorage backupStorage = (TempStorage)storage.GetStorageForDirectory("backup");

        string filePath = Path.Combine(storage.WorkingDirectory, filename);

        File.Create(filePath).Dispose();

        FileInfo fileInfo = new(filePath);

        FileInfo? backupFileInfo = BackupManager.Backup(fileInfo, backupStorage);

        Assert.Multiple(() =>
        {
            Assert.That(backupFileInfo, Is.Not.Null);
            Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
            Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(backupStorage.WorkingDirectory),
                different_path_message);
        });
    }
}