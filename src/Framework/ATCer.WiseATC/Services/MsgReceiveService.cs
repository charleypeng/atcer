// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using RabbitMQ.EventBus.AspNetCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.WiseATC.Services
{
    public class MsgReceiveService:IEventHandler<MessageBody>, IDisposable
    {
        private readonly ILogger<MsgReceiveService> _logger;

        public MsgReceiveService(ILogger<MsgReceiveService> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _logger.LogInformation($"已释放{nameof(MsgReceiveService)}");
        }

        public Task Handle(EventHandlerArgs<MessageBody> args)
        {
            _logger.LogInformation("你2" + args.Original);
            _logger.LogInformation("你" + args.Redelivered.ToString());
            _logger.LogInformation("你" + args.Exchange);
            _logger.LogInformation("你" + args.RoutingKey);

            _logger.LogInformation("你" + args.Event.Content);
            return Task.CompletedTask;
        }
    }
}
