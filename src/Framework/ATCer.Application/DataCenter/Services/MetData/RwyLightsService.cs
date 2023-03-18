// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Domains.MH4016_9;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;
using ATCer.ElasticSearch;
using ATCer.ElasticSearch.Services;

namespace ATCer.Application.DataCenter.Services.MetData
{
    public class RwyLightsService: BaseElasticService<RWYLIGHTS, RwyLightsDto, string>, IRwyLightsService, ITransient
    {
        public RwyLightsService(ILogger<PressService> logger,
                        IATCerEsClient client,
                        ICache cache) : base(client, cache, logger, IndexNames.MetData_RwyLights)
        {

        }
    }
}
