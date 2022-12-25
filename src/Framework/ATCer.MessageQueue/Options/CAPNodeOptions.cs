// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.MessageQueue.Options
{
    /// <summary>
    /// CAP节点设置
    /// </summary>
    public class CAPNodeOptions : IConfigurableOptions
    {
        public string DiscoveryServerHostName { get; set; }

        public int DiscoveryServerPort { get; set; }

        public string CurrentNodeHostName { get; set; }

        public int CurrentNodePort { get; set; }

        public string NodeId { get; set; }

        public string NodeName { get; set; }

        public string MatchPath { get; set; } = "/cap";

        public string Scheme { get; set; } = "http";

        public string[] CustomTags { get; set; }
    }
}
