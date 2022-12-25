// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Base
{
    public interface IJsTool
    {
        IDocument Document { get; set; }
        IWebStorage LocalStorage { get; init; }
        IWebStorage SessionStorage { get; init; }
    }
}