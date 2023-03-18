// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ATCer.Application.DataCenter.Workers.MetData;

/// <summary>
/// 
/// </summary>
public class PressWorker : ICapSubscribe
{
    private readonly IPressService _pressService;
    private readonly ILogger<CloudWorker> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_pressService"></param>
    /// <param name="logger"></param>
    public PressWorker(IPressService pressService, ILogger<CloudWorker> logger)
    {
        _pressService = pressService;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    [CapSubscribe("datacenter.met.raw.press", Group = "metdata")]
    [NonAction]
    public async Task AddPress(RawMetData data)
    {
        var mdata = data.ToMetType<PressDto>();
        if (mdata == null)
        {
            _logger.LogError(message: $"{DateTimeOffset.FromUnixTimeSeconds(data.TIME.Value)} 收到的原始数据有误");
        }

        mdata.Id = Guid.NewGuid().ToString("N");

        var result = await _pressService.Insert(mdata);
        if (result != null)
            _logger.LogInformation($"press已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(mdata)}");
    }
}