// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataRecorder;
using Microsoft.Extensions.Options;

namespace ATCer.LTFATCenter.Services
{
    public class TestRecorder :  SimpleUdpRecorder,ITestRecorder
    {
       
        private readonly ILogger<TestRecorder> _logger;

        public TestRecorder(IOptions<DataRecorderOptions> options,
                            ILogger<TestRecorder> logger):base(options, logger)
        {
            
        }
    }

    public class TestRecorder2 : SimpleUdpRecorder, ITestRecorder2
    {

        private readonly ILogger<TestRecorder> _logger;

        public TestRecorder2(IOptions<DataRecorderOptions> options,
                            ILogger<TestRecorder> logger) : base(options, logger)
        {

        }
    }
}
