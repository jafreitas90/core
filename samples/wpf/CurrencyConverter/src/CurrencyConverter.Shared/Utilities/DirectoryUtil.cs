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
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }
        }
    }
}
