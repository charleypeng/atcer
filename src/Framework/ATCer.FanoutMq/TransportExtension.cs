﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System.Text;
using System.Text.Json;

namespace ATCer.FanoutMq
{
    public static class TransportExtension
    {
        public static T? GetModel<T>(this TransportMsg msg)
        {
            if(msg.Body.Length == 0)
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(msg.Body.ToArray());
            }
            catch (Exception)
            {
                
                throw new Exception("无法序列化TransporMsg");
            }
        }
    }
}
