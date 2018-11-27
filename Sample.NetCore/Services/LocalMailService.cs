using Sample.NetCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = Startup.configuration["mailSetting:mailToAddress"];
        private string _mailFrom = Startup.configuration["mailSetting:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Local Mail sent to {_mailTo}");
        }
    }
}
