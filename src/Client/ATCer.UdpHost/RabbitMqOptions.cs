using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.UdpHost
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "admin";
        public string Password { get; set; } = "admin";
    }
}
