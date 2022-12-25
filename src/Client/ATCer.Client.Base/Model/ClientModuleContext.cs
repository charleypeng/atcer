// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace ATCer.Client.Base
{
    public class ClientModuleContext
    {
        public List<string> ModeuleDlls { get; set; }

        public Assembly[] ModeuleAssemblies { get; set; }
    }
}
