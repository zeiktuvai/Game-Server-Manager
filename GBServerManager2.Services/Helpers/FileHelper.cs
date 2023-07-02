using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Services.Helpers
{
    public static class FileHelper
    {
        public static bool CheckDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
