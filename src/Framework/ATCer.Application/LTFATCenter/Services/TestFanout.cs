﻿//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊  
////  CopyRight(C) 2023  版权所有 
//// -----------------------------------------------------------------------------

//using ATCer.FanoutMq;
//using System.Text;

//namespace ATCer.Application.LTFATCenter.Services
//{
//    public class TestFanout:Fanout
//    {
//        private readonly TestOpt _options;
//        private readonly ICapPublisher _publisher;
//        public TestFanout(ILogger<TestFanout> logger, TestOpt options, ICapPublisher publisher):base(logger:logger, options:options)
//        {
//            _options = options;
//            _publisher = publisher;
//            OnMessageCallback = (a, b) =>
//            {
//                _logger.LogWarning($"收到：{Encoding.UTF8.GetString(a.Body.ToArray())}");
//                return Task.CompletedTask;
//            };
//        }
//    }
//}
