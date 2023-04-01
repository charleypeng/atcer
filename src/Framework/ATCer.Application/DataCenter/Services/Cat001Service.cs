// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Services.MetData;

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
    [CapSubscribe("data.raw.origin001", Group = "rada.raw")]
    public async Task AddDataAsync(string data)
    {
        //_logger.LogError($"ORRIGIN001：{Encoding.UTF8.GetString(data)}");
    }
}

/// <summary>
/// 
/// </summary>
public class Cat002Worker : ICapSubscribe
{
    private readonly ILogger<Cat002Worker> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public Cat002Worker(ICloudService client, ILogger<Cat002Worker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [CapSubscribe("data.raw.origin002", Group = "rada.raw")]
    public async Task AddDataAsync(string data)
    {
        //_logger.LogError($"ORRIGIN001：{Encoding.UTF8.GetString(data)}");
    }
}

/// <summary>
/// 
/// </summary>
public class Cat003Worker : ICapSubscribe
{
    private readonly ILogger<Cat003Worker> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public Cat003Worker(ICloudService client, ILogger<Cat003Worker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [CapSubscribe("data.raw.origin003", Group = "rada.raw")]
    public async Task AddDataAsync(string data)
    {
        //_logger.LogError($"ORRIGIN001：{Encoding.UTF8.GetString(data)}");
    }
}