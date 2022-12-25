// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ATCer.Client.MAUI.Extensions
{
    public static class ModuleExtension
    {
        private static ClientModuleContext moduleContext;

        public static ClientModuleContext GetModuleContext()
        {
            return moduleContext;
        }

        public static void AddModuleLoader(this MauiAppBuilder builder)
        {
            IEnumerable<IConfigurationSection> sections = builder.Configuration.GetSection("ModuleSettings:Dlls").GetChildren();
            List<string> dlls = new List<string> {
            "ATCer.Client.Base.dll",
            "ATCer.Client.Core.dll"
            };
            foreach (IConfigurationSection configuration in sections)
            {
                dlls.Add(configuration.Value);
            }
            dlls = dlls.Distinct().ToList();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (string dll in dlls)
            {
                var rdll =  GetFromFile(dll);
                if(rdll != null)
                    assemblies.Add(rdll);
                System.Console.WriteLine("加载DLL：" + dll);
            }
            moduleContext = new ClientModuleContext
            {
                ModeuleDlls = dlls,
                ModeuleAssemblies = assemblies.ToArray()
            };
            builder.Services.AddScoped(typeof(ClientModuleContext), p => moduleContext);
        }

        private static Assembly GetFromFile(string fileName)
        {
            var ss = Environment.ProcessPath.Replace("ATCer.Client.MAUI.exe", "");
            
            try
            {
                return Assembly.LoadFile(ss + fileName);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
