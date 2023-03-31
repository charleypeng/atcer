// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;

namespace ATCer.DataCenter.Workers.MetData;

/// <summary>
/// 
/// </summary>
public class VisWorker : BaseRadarWorker<VisDto,string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public VisWorker(IVisService client, ILogger<VisWorker> logger):base(client, logger)
    {
 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [CapSubscribe("datacenter.met.raw.vis", Group = "metdata")]
    public override Task AddDataAsync(RawMetData data)
    {
        return base.AddDataAsync(data);
    }
}
