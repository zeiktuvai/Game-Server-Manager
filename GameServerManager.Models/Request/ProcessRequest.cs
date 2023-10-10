using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Models.Request
{
    public class ProcessRequest
    {
        public string ExecutablePath { get; set; }
        public string WorkingDir { get; set; }
        public string Arguments { get; set; }
        public bool UseShell { get; set; } = false;
        public bool RedirectOutput { get; set; } = true;
        public bool RedirectError { get; set; } = false;            
        public bool RedirectInput { get; set; } = false;
        public ProcessWindowStyle WindowStyle { get; set; } = ProcessWindowStyle.Hidden;
        public int PID { get; set; }

    }
}
