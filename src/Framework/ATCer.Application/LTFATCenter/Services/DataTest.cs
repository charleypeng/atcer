// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageQueue.Core;
using ATCer.MessageQueue.Dtos;
using Furion.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Schema;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ATCer.LTFATCenter.Services
{
    public class DataTest:ISingleton
    {
        public IMQService mQService;
        private System.Timers.Timer timer;
        private ILogger<DataTest> logger;
        public DataTest(IMQService mQService,
                        ILogger<DataTest> logger)
        {
            this.mQService = mQService;
            this.logger = logger;
            timer = new System.Timers.Timer();
        }

        public void Start()
        {
            timer.Interval = 0.002;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await TransferNow();
        }

        public async Task TransferNow()
        {
            var msg = new FlightArrInfo { ADEP = "ZGHA", EOBT = DateTime.Now, Callsign = Guid.NewGuid().ToString("d"), TCHT = DateTime.Now };

            MQRecord data = new MQRecord { Data = msg.ToJson(), MQTopic = "TESTDATA" };
            await mQService.SendToAllWithMQ<MQRecord>("farr",data);
            logger.LogError($"msg:{msg.Callsign} is send in {data.IssueTime}");
        }
    }
}
