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
    public interface IFanoutMq
    {
        /// <summary>
        /// Start listening
        /// </summary>
        //void Listening(TimeSpan timeout, CancellationToken cancellationToken);

        /// <summary>
        /// Manual submit message offset when the message consumption is complete
        /// </summary>
        //void Commit(object? sender);

        /// <summary>
        /// Reject message and resumption
        /// </summary>
        //void Reject(object? sender);

        public Func<TransportMsg, object?, Task>? OnMessageCallback { get; set; }

        //public Action<LogMessageEventArgs>? OnLogCallback { get; set; }

        public Task StartAsync(CancellationToken cancellationToken);
        public Task StopAsync(CancellationToken cancellationToken);
    }
}
