// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Services.MetData;
using ATCer.FanoutMq;
using System.Text;

namespace ATCer.Application.DataCenter.Services;

/// <summary>
/// 
/// </summary>
public class Cat001Worker : ICapSubscribe
{
    private readonly ILogger<Cat001Worker> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public Cat001Worker(ICloudService client, ILogger<Cat001Worker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [CapSubscribe("data.raw.origin001", Group = "rada.raw.cat001")]
    public async Task AddDataAsync(byte[] data)
    {
        //_logger.LogError($"ORRIGIN001：{Encoding.UTF8.GetString(data)}");
    }
}
