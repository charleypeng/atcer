// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace ATCer.NotificationSystem.Options
{
    /// <summary>
    /// 系统通知配置
    /// </summary>
    public class SystemNotificationHubOptions: IConfigurableOptions
    {
        /// <summary>
        /// 域
        /// </summary>
        public string[] Origins { get; set; }
    }
}
