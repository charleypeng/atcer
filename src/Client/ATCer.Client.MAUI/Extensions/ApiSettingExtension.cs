// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;

namespace ATCer.Client.MAUI.Extensions
{
    public static class ApiSettingExtension
    {
        public static void AddApiSetting(this MauiAppBuilder builder)
        {
            builder.Services.AddOptions<ApiSettings>().Bind(builder.Configuration.GetSection("ApiSettings"));
            builder.Services.PostConfigure<ApiSettings>(setting =>
            {
                ConfigureApiSettings(builder, setting);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void ConfigureApiSettings(MauiAppBuilder builder, ApiSettings apiSettings)
        {
            string host = apiSettings.Host;
            string port = apiSettings.Port;
            string uploadUrl = apiSettings.UploadPath;
            string basePath = apiSettings.BasePath;
            Uri baseUri = new Uri("https://localhost:5001");
            if (string.IsNullOrEmpty(host))
            {
                host = baseUri.Host;
            }
            if (string.IsNullOrEmpty(port))
            {
                port = baseUri.Port.ToString();
            }
            if (host.IndexOf("http://") < 0 && host.IndexOf("https://") < 0)
            {
                host = baseUri.Scheme + "://" + host;
            }
            apiSettings.Host = host;
            apiSettings.Port = port;
            apiSettings.BasePath = basePath;
            apiSettings.UploadPath = uploadUrl;
        }
    }
}
