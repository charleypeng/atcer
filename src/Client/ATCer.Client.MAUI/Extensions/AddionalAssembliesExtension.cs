using System;
using System.Reflection;

namespace ATCer.Client.MAUI.Extensions;

public static class AddionalAssemblies
{
    public static IEnumerable<Assembly> GetAssemblies()
    {
        var lst = new List<Assembly>
        {
            typeof(App).Assembly,
            typeof(ATCer.Client.Application.Home.Index).Assembly,
            typeof(ATCer.Client.Base.ApiSettings).Assembly,
            typeof(ATCer.Client.Core.ApiCaller).Assembly
        };

        return lst;
    }
}

