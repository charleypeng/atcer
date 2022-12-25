// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Base
{
    /// <summary>
    /// SignalRClient 提供者
    /// </summary>
    public interface ISignalRClientProvider
    {
        /// <summary>
        /// 创建client
        /// </summary>
        /// <returns></returns>
        ISignalRClient GetSignalRClient();
    }
}
