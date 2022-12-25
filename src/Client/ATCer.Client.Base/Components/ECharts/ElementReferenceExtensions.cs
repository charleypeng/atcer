// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Components;

public static class ElementReferenceExtensions
{
    public static string? GetSelector(this ElementReference? elementReference)
    {
        return elementReference?.GetSelector();
    }

    public static string? GetSelector(this ElementReference elementReference)
    {
        if (elementReference.Context is null)
        {
            return null;
        }

        return $"[_bl_{elementReference.Id}]";
    }
}