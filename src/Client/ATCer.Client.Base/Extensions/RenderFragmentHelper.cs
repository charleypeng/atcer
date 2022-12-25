// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ATCer.Client.Base
{
    public static class RenderFragmentHelper
    {
        public static RenderFragment ToMarkupRenderFragment(this string value)
        {
            return delegate (RenderTreeBuilder builder)
            {
                builder.AddMarkupContent(1, value);
            };
        }
    }
}
