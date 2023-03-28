// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.FanoutMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Application.DataCenter.Workers.Fanout
{
    public class Origin102Worker : FanoutWorker
    {
        public Origin102Worker(
            ILogger<Origin102Worker> logger,
            ICapPublisher publisher) : base("logs2", "data.raw.test2", logger, publisher)
        {
            
        }
    }
}
