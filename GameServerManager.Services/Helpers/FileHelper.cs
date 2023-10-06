namespace GameServerManager.Services.Helpers
{
    public static class FileHelper
    {
        public static bool CheckDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
