// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ATCer.Client.Native;
public static class ModuleExtension
{
    private static ClientModuleContext moduleContext;

    public static ClientModuleContext GetModuleContext()
    {
        return moduleContext;
    }

    public static void AddModuleLoader(this IServiceCollection services, IConfiguration configuration)
    {
        IEnumerable<IConfigurationSection> sections = configuration.GetSection("ModuleSettings:Dlls").GetChildren();
        List<string> dlls = new List<string> {
            "ATCer.Client.Base.dll",
            "ATCer.Client.Core.dll"
            };
        foreach (IConfigurationSection conf in sections)
        {
            dlls.Add(conf.Value);
        }
        dlls = dlls.Distinct().ToList();
        List<Assembly> assemblies = new List<Assembly>();
        foreach (string dll in dlls)
        {
            var rdll = GetFromFile(dll);
            if (rdll != null)
                assemblies.Add(rdll);
            System.Console.WriteLine("加载DLL：" + dll);
        }
        moduleContext = new ClientModuleContext
        {
            ModeuleDlls = dlls,
            ModeuleAssemblies = assemblies.ToArray()
        };
        services.AddScoped(typeof(ClientModuleContext), p => moduleContext);
    }

    private static Assembly GetFromFile(string fileName)
    {

        try
        {
            return Assembly.LoadFrom(fileName);
        }
        catch (Exception ex)
        {
            return null;
        }

    }
}
