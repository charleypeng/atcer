// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.FanoutMq
{
#if DEBUG
    public class MqOptions
    {
        public string? Ip { get; set; } = "127.0.0.1";
        public int? Port { get; set; } = 5672;
        public string? Username { get; set; } = "admin";
        public string? Password { get; set; } = "admin";
        public string? BindName { get; set; } = "logs4";
        public string? QueueName { get; set; } = "hahame";
    }
#else
    public class MqOptions
    {
        public string? Ip { get; set; } = "189.161.20.111";
        public int? Port { get; set; } = 5672;
        public string? Username { get; set; } = "admin";
        public string? Password { get; set; } = "1qaz@WSX3edc";
        public string? BindName { get; set; } = "logs4";
        public string? QueueName { get; set; } = "hahame";
    }
#endif

}
