// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using RabbitMQ.EventBus.AspNetCore.Attributes;
using RabbitMQ.EventBus.AspNetCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.WiseATC
{
    [EventBus(Exchange = "cap.default.router", RoutingKey = "DataCenter.Clear.All", Queue ="atcer.listener")]
    public class MessageBody:IEvent
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
