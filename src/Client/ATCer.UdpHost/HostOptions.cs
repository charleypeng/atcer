using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.UdpHost
{
    public class HostOptions
    {
        public int Port { get; set; } = 5566;
        public string Ip { get; set; } = "127.0.0.1";
        public string ExchangeName { get; set; } = "exchange.default";
    }
}
