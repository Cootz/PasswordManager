using System.IO;
using System.IO.Compression;
using Nuke.Common.IO;

class ZipHelper
{
    private readonly Build build;

    public ZipHelper(Build build)
    {
        this.build = build;
    }

    /// <summary>
    /// Zip <paramref name="path"/> to <see cref="Build.PublishDirectory"/>
    /// </summary>
    /// <remarks>
    /// If <paramref name="path"/> doesn't exist this method will NOT throw an exception
    /// </remarks>
    /// <param name="path">Path to folder/file to zip</param>
    /// <param name="zipFileName">Filename of created zip file</param>
    /// <param name="expression">Define if <paramref name="path"/> is file or directory. Directory by default. For more info <see cref="AbsolutePathExtensions.Exists"/></param>
    public void ZipToPublish(AbsolutePath path, string zipFileName, string expression = Build.DIRECTORY_INDICATOR)
    {
        if (!path.Exists(expression))
            return;

        path.ZipTo(build.PublishDirectory / zipFileName,
            compressionLevel: CompressionLevel.SmallestSize,
            fileMode: FileMode.CreateNew);
    }
}