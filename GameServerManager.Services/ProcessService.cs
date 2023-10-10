using GameServerManager.Models;
using GameServerManager.Models.Request;
using GameServerManager.Models.Servers;
using System.Diagnostics;

namespace GameServerManager.Services
{
    public class ProcessService
    {
        public Process StartProcess(ProcessRequest request)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = request.FilePath,
                    Arguments = request.Arguments,
                    UseShellExecute = request.UseShell,
                    RedirectStandardOutput = request.RedirectOutput,
                    WindowStyle = request.WindowStyle
                }
            };

            return proc;
        }

    }
}
