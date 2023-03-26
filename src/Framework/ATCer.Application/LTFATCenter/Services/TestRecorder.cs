// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataRecorder;
using Microsoft.Extensions.Options;
using System.Text;

namespace ATCer.LTFATCenter.Services
{
    public class TestRecorder :  SimpleUdpRecorder,ITestRecorder
    {
        public TestRecorder(IOptions<DataRecorderOptions> options,
                            ILogger<TestRecorder> logger):base(options, logger)
        {
            
        }
    }

    public class TestRecorder2 : MUdpRecorder, ITestRecorder2
    {
        private readonly ICapPublisher _publisher;
        public TestRecorder2(IOptions<DataRecorderOptions> options,
                            ICapPublisher publisher,
                            ILogger<TestRecorder2> logger) : base(options, logger)
        {
           _publisher = publisher;

            this.Action = async(a) =>
            {
                await _publisher.PublishAsync("data.raw.mh4029_3", a.Data);
            };
        }
    }
}
