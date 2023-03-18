// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.ElasticSearch;
using ATCer.ElasticSearch.Services;

namespace ATCer.DataCenter.Services.MetData;

/// <summary>
/// 
/// </summary>
public class PressService: BaseElasticService<PRESS,PressDto,string>,ICapSubscribe,ITransient, IPressService
{
    /// <summary>
    /// 
    /// </summary>
    public PressService(ILogger<PressService> logger,
                        IATCerEsClient client,
                        ICache cache
                        ):base(client,cache,logger, IndexNames.MetData_PRESS)
    {

    }
}