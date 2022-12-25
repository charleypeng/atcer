// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum MyHttpMethod
    {
        /// <summary>
        /// GET
        /// </summary>
        [Description("GET")]
        GET=0,
        /// <summary>
        /// POST
        /// </summary>
        [Description("POST")]
        POST=1,
        /// <summary>
        /// PUT
        /// </summary>
        [Description("PUT")]
        PUT=2,
        /// <summary>
        /// DELETE
        /// </summary>
        [Description("DELETE")]
        DELETE=3,
        /// <summary>
        /// PATCH
        /// </summary>
        [Description("PATCH")]
        PATCH=4
    }
}
