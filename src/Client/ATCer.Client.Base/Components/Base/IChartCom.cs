// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Components
{
    /// <summary>
    /// 图表展示基类接口
    /// </summary>
    public interface IChartCom:IBaseCom
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        Task RefreshData();
        /// <summary>
        /// 网络状态
        /// </summary>
        event NetworkStatusChangedEventHandler NetWorkStatusChanged;
    }
}
