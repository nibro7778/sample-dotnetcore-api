using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Services
{
    public class CloudMailService : IMailService
    {
        public void Send(string subject, string message)
        {
            Debug.WriteLine("Cloud Mail service called");
        }
    }
}
