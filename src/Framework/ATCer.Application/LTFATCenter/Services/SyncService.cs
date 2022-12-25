// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;
using ATCer.LTFATCenter.Domains;
using ATCer.LTFATCenter.Domains.GZFips;
using ATCer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DotNetCore.CAP.Serialization;
using DotNetCore.CAP.Messages;
using ATCer.LTFATCenter.Services;
using System.Text.Json;

namespace ATCer.LTFATCenter.Impl.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ApiDescriptionSettings("LTFATServices")]
    public class FlightPlanSync:DbSyncServiceBase<GZFlightPlan, FlightPlan, FipsDbContextLocator,MasterDbContextLocator> 
    {
        
    }
    [ApiDescriptionSettings("LTFATServices")]
    public class FlightArrInfoSync : DbSyncServiceBase<GZFlightArrInfo, FlightArrInfo, FipsDbContextLocator, MasterDbContextLocator>
    {

    }
    [ApiDescriptionSettings("LTFATServices")]
    public class FlightDepInfoSync : DbSyncServiceBase<GZFlightDepInfo, FlightDepInfo, FipsDbContextLocator, MasterDbContextLocator>
    {

    }

    [ApiDescriptionSettings("LTFATServices")]
    public class MSSync : DbSyncServiceBase<FlightPlan,FlightPlan, MasterDbContextLocator, ATCerSlaveDbContextLocator>
    {
        //private readonly IOptions<MQSettings> _mqSettings;
        //public MSSync(IOptions<MQSettings> mqSettings)
        //{
        //    _mqSettings = mqSettings;
        //}
        //[NonAction]
        ////[CapSubscribe(MQTopic.DataCenter_UpDate_Dashboard,Group = nameof(FlightPlanSync))]
        //public async Task StartToSync(DateTime time)
        //{
        //    await this.Start();
        //    Console.WriteLine($"已经于 {time} 同步完成");
    }

    [ApiDescriptionSettings("LTFATServices")]
    public class HistoryDataSync : DbSyncServiceBase<GZFlightPlanHist, FlightPlan, FipsHistoryDbContextLocator, MasterDbContextLocator>, ICapSubscribe
    {
        private readonly ILogger<HistoryDataSync> _logger;
        private readonly ISerializer _serializer;
        public HistoryDataSync(ILogger<HistoryDataSync> logger,
                               ISerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
        }
        [NonAction]
        [CapSubscribe(Topic.Dashboard.DataCenterUpdateDashboard, Group = nameof(FlightPlanSync))]
        public async Task HelpMeNow(object obj)
        {
            _logger.LogInformation($"收到消息： {System.Text.Json.JsonSerializer.Serialize(obj)}");
        }

        [CapSubscribe(Topic.Dashboard.DataCenterClearAll, Group = nameof(FlightPlanSync))]
        public async Task HelpMeNow2(MQObject mqobj)
        {
            var msg2 = System.Text.Json.JsonSerializer.Serialize(mqobj);
            _logger.LogInformation($"收到消息： {msg2}");
        }

        [CapSubscribe(Topic.Dashboard.DataCenterCheckStatus, Group = nameof(FlightPlanSync))]
        public async Task CallMeBack(object obj)
        {
            var msg2 = System.Text.Json.JsonSerializer.Serialize(obj);
            _logger.LogInformation($"收到消息： {msg2}");
        }
    }

    [ApiDescriptionSettings("LTFATServices")]
    public class HistoryArrDataSync : DbSyncServiceBase<GZFlightArrInfoHist, FlightArrInfo, FipsHistoryDbContextLocator, MasterDbContextLocator>, ICapSubscribe
    {
        //private readonly IOptions<MQSettings> _mqSettings;
        //public MSSync(IOptions<MQSettings> mqSettings)
        //{
        //    _mqSettings = mqSettings;
        //}
    }

    [ApiDescriptionSettings("LTFATServices")]
    public class HistoryDepDataSync : DbSyncServiceBase<GZFlightDepInfoHist, FlightDepInfo, FipsHistoryDbContextLocator, MasterDbContextLocator>, ICapSubscribe
    {
        //private readonly IOptions<MQSettings> _mqSettings;
        //public MSSync(IOptions<MQSettings> mqSettings)
        //{
        //    _mqSettings = mqSettings;
        //}
    }
}
