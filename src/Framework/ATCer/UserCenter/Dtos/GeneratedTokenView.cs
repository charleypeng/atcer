// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneratedTokenView
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; } = "使用方法:将Token写入header中:atcer {token},请妥善保管好";
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}
