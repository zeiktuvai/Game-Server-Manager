using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Models.Options
{
    public class ApplicationSettings
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public List<AppSetting> Settings { get; set; }
    }
}
