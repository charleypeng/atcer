// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using ATCer.Client.Base;
using ATCer.Client.Base.Services;

namespace ATCer.Client.Core.Services
{
    /// <summary>
    /// 系统配置-暂时写死吧
    /// </summary>
    [ScopedService]
    public class SystemConfigService : ISystemConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCopyright()
        {
            return GetSystemConfig().Copyright;
        }
        /// <summary>
        /// 获取系统配置
        /// 可放置到数据库中
        /// </summary>
        /// <returns></returns>
        public SystemConfig GetSystemConfig()
        {

            return new SystemConfig {
            
                Copyright= DateTime.Now.Year+ " 彭磊",
                SystemName= "ATCer",
                SystemDescription= "全平台运行监控系统"

            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSystemName()
        {
            return GetSystemConfig().SystemName;
        }
    }
}
