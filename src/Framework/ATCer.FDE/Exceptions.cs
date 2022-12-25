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

namespace ATCer.FDE
{
    /// <summary>
    /// 非标准的报文
    /// </summary>
    public class NotValidFDEException:Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public NotValidFDEException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FDEString">error source</param>
        public NotValidFDEException(string FDEString)
        {
            this.Source = FDEString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FDEString">error source</param>
        /// <param name="errorMsg">error message</param>
        public NotValidFDEException(string FDEString, string errorMsg):base(errorMsg)
        {
            this.Source = FDEString;
        }
    }
}
