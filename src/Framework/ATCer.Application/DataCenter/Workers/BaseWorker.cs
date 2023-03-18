// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Dtos.MetDatDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ATCer.DataCenter.Workers;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class BaseWorker<TDto,TKey>:ICapSubscribe where TDto: BaseMetDto,new()
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
    public BaseWorker(IServiceBase<TDto, TKey> client,
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
    public virtual async Task AddDataAsync(RawMetData data)
    {
        var mdata = data.ToMetType<TDto>();
        if (mdata == null)
        {
            _logger.LogError(message: $"{DateTimeOffset.Now} 收到的原始数据有误");
            return;
        }

        mdata.Id = Guid.NewGuid().ToString("N");

        var result = await _client.Insert(mdata);
        //insert
        if (result != null)
            _logger.LogInformation($"press已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(mdata)}");
    }
}
