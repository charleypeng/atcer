// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.Application.DataCenter.Builders.MetData;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class DataCenterServiceExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static void AddDataCenterService(this IServiceCollection services)
    {
        var pressBuilder = App.GetService<PRESSBuilder>();
        var cloudBuilder = App.GetService<CLOUDBuilder>();
        var rawBuilder = App.GetService<RawMetDataBuilder>();
        var rwyLightsBuilder = App.GetService<RWYLIGHTSBuilder>();
        pressBuilder.InitIndex();
        cloudBuilder.InitIndex();
        rawBuilder.InitIndex();
        rwyLightsBuilder.InitIndex();
    }
}