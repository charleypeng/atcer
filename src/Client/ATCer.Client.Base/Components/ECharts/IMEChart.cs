// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer
{
    public interface IMEChart
    {
        /// <summary>
        /// 配置Echarts选项
        /// </summary>
        /// <param name="options">option: ECOption</param>
        /// <param name="notMerge">notMerge 可选。是否不跟之前设置的 option 进行合并。默认为 false。即表示合并。合并的规则，详见 组件合并模式。如果为 true，表示所有组件都会被删除，然后根据新 option 创建所有新组件。</param>
        /// <param name="lazyMode">可选。在设置完 option 后是否不立即更新图表，默认为 false，即同步立即更新。如果为 true，则会在下一个 animation frame 中，才更新图表。</param>
        /// <returns></returns>
        Task SetOption(object options, bool notMerge=false, bool lazyUpdate=false);
    }
}
