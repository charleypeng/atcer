using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.UdpHost
{
    public class UdpHostOptions
    {
        public List<HostOptions>? HostOptions { get; set; }

        public RabbitMqOptions? RabbitMqOptions { get; set; }
    }
}
