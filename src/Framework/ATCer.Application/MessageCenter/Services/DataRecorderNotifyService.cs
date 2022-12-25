// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageQueue.Core;
using Furion.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.MessageCenter.Services
{
    public class DataRecorderNotifyService : IDataRecorderNotifyService, IScoped
    {
        private readonly ILogger<DataRecorderNotifyService> logger;
        private readonly IMQService mqService;
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        /// <param name="mqService"></param>
        public DataRecorderNotifyService(
                             
                                       ILogger<DataRecorderNotifyService> logger,
                                       IMQService mqService)
        {
            this.logger = logger;
            this.mqService = mqService;
        }
        public async Task Send(object message, string topic, string group = null)
        {
            throw new NotImplementedException();
        }
    }
}
