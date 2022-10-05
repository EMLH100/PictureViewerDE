using System.IO;

namespace PictureViewerDE.Utilities
{
    public class PathUtilities
    {
        public static readonly string EXEC_PATH = Path.GetFullPath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        public static readonly string SOURCE_PATH = Path.GetFullPath(EXEC_PATH + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "..");
    }
}