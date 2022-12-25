// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.LTFATCenter.Client.Services
{
    [ScopedService]
    public class Test1
    {
        public string Testme()
        {
            return Guid.NewGuid().ToString("d");
        }
    }
}
