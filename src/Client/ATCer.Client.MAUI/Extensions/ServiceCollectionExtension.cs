// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer;

namespace ATCer.Client.MAUI
{
    public static class ServiceCollectionExtension
    {
        public static void AddMauiServices(this IServiceCollection services)
        {
            services.AddScoped<IFileReader, FileReader>();
            //services.AddScoped(sp => new HttpClient());
        }
    }
}
