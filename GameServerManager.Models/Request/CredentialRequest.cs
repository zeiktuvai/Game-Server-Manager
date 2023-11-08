using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Models.Request
{
    public class CredentialRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TFA { get; set; }
    }
}
