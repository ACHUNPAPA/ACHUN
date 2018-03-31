using System.IO;

namespace Achun.File
{
    public class FileTools
    {
        public static DirectoryInfo GetDirectoryInfo(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
                return null;
            return new DirectoryInfo(dirPath);
        }
    }
}