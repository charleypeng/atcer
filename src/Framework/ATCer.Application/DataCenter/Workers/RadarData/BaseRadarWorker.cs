// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains.RadarData;
using ATCer.DataCenter.Dtos.RadarDataDtos;
using Microsoft.AspNetCore.Mvc;

namespace ATCer.DataCenter.Workers.RadarData;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class BaseMetWorker<TDto, TKey> : ICapSubscribe where TDto : BaseRadarDto<string>, new()
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly ILogger _logger;
    /// <summary>
    /// service client
    /// </summary>
    protected readonly IServiceBase<TDto, TKey> _client;

    /// <summary>
    /// Init
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public BaseMetWorker(IServiceBase<TDto, TKey> client,
                      ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <remarks>请重写该方法并添加CAP标签</remarks>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task AddDataAsync(RawRadarData data)
    {
        if (data == null)
            return;

        var mdata = data?.D?.Adapt<List<TDto>>();
        if (mdata == null)
        {
            _logger.LogError(message: $"{DateTimeOffset.Now} 收到的原始数据有误");
            return;
        }

        string? sourceId = null;

        if(!string.IsNullOrWhiteSpace(data?.O))
        {
            sourceId = Guid.NewGuid().ToString("N");
        }

        //var time = DateTimeOffset.Parse();
        foreach (var item in mdata) 
        {
            item.Id = Guid.NewGuid().ToString("N");
            item.SourceId = sourceId;
            //todo: change to T
            item.CreatedTime = DateTime.Now;
            await _client.Insert(item);
        }
        //todo add sourceItem to local
    }
}
