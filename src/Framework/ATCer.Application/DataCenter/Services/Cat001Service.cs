// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;

namespace ATCer.Application.DataCenter.Services;

/// <summary>
/// 
/// </summary>
public class Cat001Worker : BaseWorker<CloudDto, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public Cat001Worker(ICloudService client, ILogger<Cat001Worker> logger) : base(client, logger)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [CapSubscribe("data.raw.origin001", Group = "rada.raw.cat001")]
    public override Task AddDataAsync(RawMetData data)
    {
        return base.AddDataAsync(data);
    }
}
