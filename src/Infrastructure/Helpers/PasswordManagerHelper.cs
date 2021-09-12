using System.IO;

namespace Infrastructure.Helpers
{
    public static class PasswordManagerHelper
    {
        public static string GetValueFromTxtFile(string directoryName,string fileName)
        {
            var result = string.Empty;

            var parent = Path.GetDirectoryName(directoryName);
            var filePath = Path.GetDirectoryName(parent) + $"/{fileName}";

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                result = File.ReadAllText(filePath);
            }

            return result;
        }
    }
}
