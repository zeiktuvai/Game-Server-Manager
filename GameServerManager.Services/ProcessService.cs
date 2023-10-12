using GameServerManager.Models;
using GameServerManager.Models.Enums;
using GameServerManager.Models.Request;
using GameServerManager.Models.Servers;
using System.Diagnostics;

namespace GameServerManager.Services
{
    public class ProcessService
    {
        public List<RunningProcess> RunningProcesses { get; set; }

        public ProcessService()
        {
            RunningProcesses = new();
        }

        public List<RunningProcess> GetRunningProcesses()
        {
            return RunningProcesses;
        }

        public RunningProcess StartProcess(ProcessRequest request)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = request.ExecutablePath,
                    Arguments = request.Arguments,
                    UseShellExecute = request.UseShell,
                    RedirectStandardOutput = request.RedirectOutput,
                    WindowStyle = request.WindowStyle
                }
            };

            var result = proc.Start();

            if (result)
            {
                var rp = new RunningProcess(proc);
                rp.ProcessExited += ProcExited;
                RunningProcesses.Add(rp);
                return rp;
            }

            return null;
        }

        public RunningProcess StartServerProcess(ProcessRequest request)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = request.ExecutablePath,
                    WorkingDirectory = request.WorkingDir,
                    Arguments = request.Arguments,
                    UseShellExecute = request.UseShell,
                    RedirectStandardOutput = request.RedirectOutput,
                    WindowStyle = request.WindowStyle
                }

            };

            var result = proc.Start();
            if (result)
            {
                var rp = new RunningProcess(proc);
                rp.ProcessExited += ProcExited;
                RunningProcesses.Add(rp);
                return rp;
            }

            return null;
        }

        public bool StopServerProcess(ProcessRequest request)
        {
            var procEntry = RunningProcesses.FirstOrDefault(p => p.proc?.Id == request.PID);

            if (procEntry != null)
            {
                procEntry.proc.Kill();
            }

            if (procEntry.proc.HasExited)
            {
                RunningProcesses.Remove(procEntry);
                return true;
            }
            return false;
        }

        public bool CheckProcessRunning(ProcessRequest request)
        {
            var procEntry = RunningProcesses.FirstOrDefault(p => p?.proc?.StartInfo.FileName == request.ExecutablePath);
            var PIDMatches = procEntry?.proc?.Id == request.PID;

            if (PIDMatches)
            {
                return Process.GetProcessById(request.PID).HasExited;
            }
            return false;
        }


        private void ProcExited(object? sender, EventArgs e)
        {
            var procEntry = RunningProcesses.FirstOrDefault(p => p.proc.Id == (sender as Process).Id);

            if (procEntry != null)
            {
                RunningProcesses.Remove(procEntry);
            }
        }
    }
}
