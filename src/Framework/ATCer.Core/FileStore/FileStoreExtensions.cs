// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.FileStore;
using ATCer.FileStore.Core.LocalStore;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 文件存储扩展
    /// </summary>
    public static class FileStoreExtensions
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileLocalStore(this IServiceCollection services)
        {
            services.AddScoped<IFileStoreService, LocalFileStoreService>();
            services
               .AddConfigurableOptions<LocalFileStoreSettings>();

            return services;
        }
    }
}
