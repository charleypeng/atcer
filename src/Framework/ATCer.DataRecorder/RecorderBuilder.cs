// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace ATCer.DataRecorder
{
    public sealed class RecorderBuilder
    {
        public IServiceCollection Services { get; }
        public RecorderBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public RecorderBuilder AddRecorder<T>(Action<RecorderOptions> setupAction) where T : class, IRecorder
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            var options = new RecorderOptions();
            setupAction(options);
            Services.Configure(setupAction);
            Services.AddScoped<IRecorder, T>();
            return this;
        }
    }
}
