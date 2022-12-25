// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Base.Authorization
{
    public class ClientResource
    {
        public string[] Keys { get; set; }

        /// <summary>
        /// 并且关系
        /// 默认 true 是 and关系,想使用 or 置为 false
        /// </summary>
        public bool AndCondition { get; set; } = true;
    }
}
