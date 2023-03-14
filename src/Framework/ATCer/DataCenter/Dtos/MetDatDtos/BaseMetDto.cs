// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.DataCenter.Dtos.MetDatDtos
{
    public abstract class BaseMetDto<TKey>:BaseDto<TKey>
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [DisplayName("主键")]
        public virtual TKey Id { get; set; }
        /// <summary>
        /// 假删除
        /// </summary>
        [DisplayName("假删除")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 数据原ID
        /// </summary>
        public string? SourceId { get; set; }

        /// <summary>
        /// 传感器位置
        /// </summary>
        public string? Location { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseMetDto : BaseMetDto<string>
    {
    }
}
