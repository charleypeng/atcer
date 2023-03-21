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
        var cloudBuilder = App.GetService<CloudBuilder>();
        var humitempBuilder = App.GetService<HumiTempBuilder>();
        var pressBuilder = App.GetService<VisBuilder>();
        var pvBuilder = App.GetService<PvBuilder>();
        var pwBuilder = App.GetService<PwBuilder>();
        var rainBuilder = App.GetService<RainBuilder>();
        var rosaBuilder = App.GetService<RosaBuilder>();
        var rwyLightsBuilder = App.GetService<RwyLightsBuilder>();
        var visBuilder = App.GetService<VisBuilder>();
        var windBuilder = App.GetService<WindBuilder>();

        var rawBuilder = App.GetService<RawMetDataBuilder>();

        cloudBuilder.InitIndex();
        humitempBuilder.InitIndex();
        pressBuilder.InitIndex();
        pvBuilder.InitIndex();
        pwBuilder.InitIndex();
        rainBuilder.InitIndex();
        rosaBuilder.InitIndex();
        rwyLightsBuilder.InitIndex();
        visBuilder.InitIndex();
        windBuilder.InitIndex();

        rawBuilder.InitIndex();
    }
}