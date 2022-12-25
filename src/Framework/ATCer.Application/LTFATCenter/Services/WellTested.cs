// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using DotNetCore.CAP.Messages;
using DotNetCore.CAP.Serialization;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Services
{
    public class MQObject
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class WellTested : IDynamicApiController, ICapSubscribe
    {
        private readonly ICapPublisher _publisher;
        private readonly ILogger<WellTested> _logger;
        private readonly ISerializer _serializer;
        public WellTested(ICapPublisher publisher,
                          ILogger<WellTested> logger,
                          ISerializer serializer)
        {
            _publisher = publisher;
            _logger = logger;
            _serializer = serializer;
        }
        [HttpPost("~/send")]
        public async Task<IActionResult> SendMessage(MQObject obj)
        {
            var msg = await _serializer.SerializeAsync(new Message { Value = obj });
            _publisher.Publish(Topic.Dashboard.DataCenterUpdateDashboard, msg);
            return new OkResult();
        }

        [HttpPost("~/send2")]
        public async Task<IActionResult> Send2Message(MQObject obj)
        {
            _publisher.Publish(Topic.Dashboard.DataCenterClearAll, obj, Topic.Dashboard.DataCenterUpdateDashboard);
            return new OkResult();
        }

        [CapSubscribe("samples.time.show")]
        public void ShowTime(DateTime time)
        {
            _logger.LogError("测试达成 -->" + time);
        }

        private object result;
        [CapSubscribe("testmq.result.get")]
        public async Task<object> MQTest(string msg)
        {
            await _publisher.PublishAsync<string>("testmq", msg, "wtf");
            _logger.LogError("mqtest:" + msg);
            return result;
        }

        [NonAction]
        [CapSubscribe("testmq")]
        public async Task<object> RMQTest(string msg, [FromCap]CapHeader header)
        {
            _logger.LogError("test"+msg+header.ToString());
            result = new { wwr = DateTime.Now, msg = msg };
            await _publisher.PublishAsync("testmq.result", result,"testmq.result.get");
            return result;
        }

        [NonAction]
        [CapSubscribe("testmq", isPartial: true)]
        public void RMQTest2(string msg)
        {
            _logger.LogError("test2:"+msg);
        }

        [CapSubscribe("wtf")]
        public string WTF(string msg)
        {
            _logger.LogError("wtf:");
            return "wtf!!!:" + msg;
        }
    }
}
