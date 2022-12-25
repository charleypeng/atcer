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

namespace ATCer.Base
{
    /// <summary>
    /// Entity基类接口
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// 已删除
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// 已锁定
        /// </summary>
        bool IsLocked { get; set; }
    }
}
