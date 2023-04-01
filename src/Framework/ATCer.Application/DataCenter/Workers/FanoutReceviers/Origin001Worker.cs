// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains.RadarData;
using ATCer.FanoutMq;

namespace ATCer.Application.DataCenter.Workers.FanoutReceviers;

/// <summary>
/// 
/// </summary>
public class Origin001Worker : Fanout
{
    private readonly ICapPublisher _publisher;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="options"></param>
    /// <param name="publisher"></param>
    public Origin001Worker(ILogger<Origin001Worker> logger,
        TestOpt options,
        ICapPublisher publisher) : base(logger: logger, options: options)
    {
        BindName = "data.raw.origin1";
        _publisher = publisher;

        OnMessageCallback = async (a, b) =>
        {
            var model = a.GetModel<RawRadarData>();

            if (model == null)
                return;

            if(model.T == null)
            {
                model.T =  DateTimeOffset.Now.ToUnixTimeSeconds();
            }

            await _publisher.PublishAsync($"data.raw.{model.tp?.ToLower()}", model);
        };
    }
}