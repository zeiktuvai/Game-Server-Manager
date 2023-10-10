namespace GameServerManager.Services.Helpers
{
    public static class FileHelper
    {
        public static bool CheckDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public static bool CheckFileExists(string file)
        {
            return File.Exists(file);
        }
    }
}
