using PasswordManager.Model.IO;

namespace PasswordManager.Tests.IO;

[TestFixture]
public class BackupManagerTest
{
    private const string FILENAME = "testfile.txt";
    private const string DIFFERENT_PATH_MESSAGE = "File saved in a different directory";

    [Test]
    public void BackupFileOnseTest()
    {
        TempStorage storage = new();
        var filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

        File.Create(filePath).Dispose();

        FileInfo? fileinfo = new(filePath);

        var backupFileInfo = BackupManager.Backup(fileinfo);

        Assert.That(backupFileInfo, Is.Not.Null);
        Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
        Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(fileinfo.DirectoryName), DIFFERENT_PATH_MESSAGE);
    }

    [Test]
    public void BackupFileMultipleTimesTest()
    {
        TempStorage storage = new();
        var filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

        File.Create(filePath).Dispose();

        FileInfo? fileinfo = new(filePath);

        List<FileInfo> backupFileInfos = new();

        for (var i = 0; i < 100; i++) backupFileInfos.Add(BackupManager.Backup(fileinfo));

        foreach (var backupFileInfo in backupFileInfos)
        {
            Assert.That(backupFileInfo, Is.Not.Null);
            Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
            Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(fileinfo.DirectoryName), DIFFERENT_PATH_MESSAGE);
        }
    }

    [Test]
    public void BackupNonExistingFile()
    {
        TempStorage storage = new();
        var filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

        FileInfo? fileinfo = new(filePath);

        Assert.Throws<FileNotFoundException>(() => BackupManager.Backup(fileinfo));
    }

    [Test]
    public void BackupFileInADifferentDirectory()
    {
        TempStorage storage = new();

        var backupStorage = (TempStorage)storage.GetStorageForDirectory("backup");

        var filePath = Path.Combine(storage.WorkingDirectory, FILENAME);

        File.Create(filePath).Dispose();

        FileInfo? fileinfo = new(filePath);

        var backupFileInfo = BackupManager.Backup(fileinfo, backupStorage);

        Assert.That(backupFileInfo, Is.Not.Null);
        Assert.That(File.Exists(backupFileInfo.FullName), Is.True);
        Assert.That(backupFileInfo.DirectoryName, Is.EqualTo(backupStorage.WorkingDirectory),
            DIFFERENT_PATH_MESSAGE);
    }
}