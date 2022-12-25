using AntDesign;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace ATCer.Client.Services
{
    public class ThemeService : IThemeService
    {
        [Inject]
        IOptions<ProSettings> proSettings { get; set; }
        [Inject]
        IJSRuntime Js { get; set; }

        private string _url;

        protected async Task JsInvokeAsync(string code, params object[] args)
        {
            try
            {
                await Js.InvokeVoidAsync(code, args);
            }
            catch (Exception ex)
            {
                Exception e = ex;
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateTheme(string theme, ElementReference element)
        {
            await JsInvokeAsync(JSInteropConstants.AddElementToBody, element);
        }
    }
}
