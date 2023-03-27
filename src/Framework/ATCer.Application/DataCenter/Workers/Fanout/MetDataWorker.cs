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
    public class MetDataWorker:FanoutWorker
    {
        public MetDataWorker( 
            ILogger<MetDataWorker> logger, 
            ICapPublisher publisher):base("logs4", "data.raw.mh4029_3", logger,publisher)
        {
            Worker = null;
        }
    }
}
