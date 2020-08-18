using System.IO;

namespace CurrencyConverter.Shared.Utilities
{
    public class DirectoryUtility
    {
        public static void EnsureDirectoryExists(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Directory.Exists)
            {
                Directory.CreateDirectory(fi.DirectoryName);
            }
        }
    }
}
