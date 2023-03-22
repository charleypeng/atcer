// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataRecorder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static RecorderBuilder AddDataRecorder(this IServiceCollection services, Action<DataRecorderOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            //Options and extension service
            var options = new DataRecorderOptions();
            setupAction(options);

            services.AddSingleton(_ => services);

            foreach (var serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            services.Configure(setupAction);

            return new RecorderBuilder(services);
        }
    }
}
