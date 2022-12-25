// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using Microsoft.Extensions.Localization;

namespace ATCer.Client.Core.Services
{
    public class ClientLocalizer<T> : IClientLocalizer
    {
        private readonly IStringLocalizer<T> localizer;

        public ClientLocalizer(IStringLocalizer<T> localizer)
        {
            this.localizer = localizer;
        }

        public string this[string name] => localizer[name].Value;

        public string Combination(params string[] names)
        {
            if (names.Length == 0) {
                return string.Empty;
            }
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            for (int i=0;i<names.Length;i++)
            {
                msg.Append(localizer[names[i]].Value);
                msg.Append(' ');
            }
            return msg.ToString().TrimEnd(' ');
        }
        
    }
}
