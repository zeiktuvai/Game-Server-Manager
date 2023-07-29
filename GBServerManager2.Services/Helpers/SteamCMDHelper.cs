using GBServerManager2.Models;
using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using static GBServerManager2.Models.GameServerProcess;

namespace GBServerManager2.Services.Helpers
{
    public class SteamCMDHelper
    {
        public event procOutputUpdateHandler outputUpdated;
        public string output = "";        
        private StringBuilder ProcOut;


        public SteamCMDHelper()
        {
            ProcOut = new StringBuilder();            
        }

        public int DownloadUpdateNewServer(GameServer server)
        {
            if (!string.IsNullOrWhiteSpace(server.ServerPath))
            {
                if (!Directory.Exists(server.ServerPath))
                {
                    Directory.CreateDirectory(server.ServerPath);
                }

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = GlobalConstants.SteamCommandPath + "\\steamcmd.exe",
                        Arguments = $"+force_install_dir '{server.ServerPath}' +login anonymous +app_update {server.SteamAppId} +quit",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                var status = proc.Start();
                //TODO: Why is this not working to the right folder?
                if (status)
                {
                    this.output = "";
                    proc.OutputDataReceived += OutputRecieved;
                    proc.BeginOutputReadLine();                                        
                }

                return proc.Id;
            }

            return 0;
        }

        public static bool CheckSteamClientExists()
        {
            return File.Exists(GlobalConstants.SteamCommandPath + "\\steamcmd.exe");
        }

        public static async Task<bool> DownloadSteamClient()
        {
            try
            {
                if (!Directory.Exists(GlobalConstants.SteamCommandPath))
                {
                    Directory.CreateDirectory(GlobalConstants.SteamCommandPath);
                }

                if (!CheckSteamClientExists())
                {
                    using (var client = new HttpClient())
                    {
                        byte[] fileBytes = await client.GetByteArrayAsync(new Uri("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip"));
                        File.WriteAllBytes(GlobalConstants.SteamCommandPath + "\\steamcmd.zip", fileBytes);
                    }
                    ZipFile.ExtractToDirectory(GlobalConstants.SteamCommandPath + "\\steamcmd.zip", GlobalConstants.SteamCommandPath, true);
                    File.Delete(GlobalConstants.SteamCommandPath + "\\steamcmd.zip");
                }
            }
            catch (System.Exception)
            {
                throw new IOException("Could not download steamCMD file");
            }

            return true;
        }

        private void OutputRecieved(object sendingProc, DataReceivedEventArgs e)
        {
            this.ProcOut.Append(e.Data);
            this.output = ProcOut.ToString();
            outputUpdated?.Invoke();
        }
    }
}
