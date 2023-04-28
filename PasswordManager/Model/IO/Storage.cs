namespace PasswordManager.Model.IO;

/// <summary>
/// Defines storage
/// </summary>
public abstract class Storage
{
    /// <summary>
    /// Current working directory of the storage
    /// </summary>
    public string WorkingDirectory { get; private set; }

    public Storage(string path)
    {
        Directory.CreateDirectory(path);

        WorkingDirectory = path;
    }

    /// <summary>
    /// Get storage for subdirectory. Creates the directory if not exists
    /// </summary>
    /// <param name="directory">Subdirectory for the storage</param>
    /// <returns>New storage attached to <paramref name="directory"/></returns>
    /// <remarks>
    /// <paramref name="directory"/> is a subdirectory of <see cref="WorkingDirectory"/>. It cannot be a path or filename
    /// </remarks>
    public abstract Storage GetStorageForDirectory(string directory);
}