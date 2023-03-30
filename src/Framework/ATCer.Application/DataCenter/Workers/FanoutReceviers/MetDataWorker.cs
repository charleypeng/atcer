// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Application.LTFATCenter.Services;
using ATCer.DataCenter.Domains;
using ATCer.FanoutMq;
using System.Text;

namespace ATCer.Application.DataCenter.Workers.FanoutReceviers;


public class MetDataWorker : Fanout
{
    private readonly ICapPublisher _publisher;
    public MetDataWorker(ILogger<MetDataWorker> logger, 
        TestOpt options, 
        ICapPublisher publisher) : base(logger: logger, options: options)
    {
        _publisher = publisher;
        BindName = "data.raw.awos";
        OnMessageCallback = async(a, b) =>
        {
            await _publisher.PublishAsync("data.raw.mh4029_3", a.GetModel<RawMetData>());
        };
    }
}
