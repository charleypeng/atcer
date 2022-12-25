// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.ElasticSearch.Enums
{
    /// <summary>
    /// Elastic Repsonse Status
    /// </summary>
    public enum StatusCode
    {
        Success,
        Failed,
        Error,
        NotFound,
        /// <summary>
        /// Use when you're sure it is a connection problem
        /// </summary>
        ConnectionFailed,
        /// <summary>
        /// This is used when a very serious error accured
        /// </summary>
        FatalError,
        Null,
        Unauthorized,
        Unknown,
        NoAction
    }
}
