// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.FanoutMq;
using System.Text;

namespace ATCer.Application.DataCenter.Workers.Fanout
{
    public class Origin101Worker : FanoutWorker
    {
        public Origin101Worker(
            ILogger<Origin101Worker> logger,
            ICapPublisher publisher) : base("logs1", "data.raw.test1", logger, publisher)
        {
            Worker =async (a, b) =>
            {
                await _publisher.PublishAsync("aha", Encoding.UTF8.GetString(a.Body.ToArray()));
            };
        }
    }
}
