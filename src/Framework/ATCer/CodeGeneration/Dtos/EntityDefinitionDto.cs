// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATCer.CodeGeneration.Dtos
{
    /// <summary>
    /// 实体信息
    /// </summary>
    [Description("实体信息")]
    public class EntityDefinitionDto
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public String Name { get; set; }
        /// <summary>
        /// 实体完整名称
        /// </summary>
        [DisplayName("实体完整名称")]
        public String FullName { get; set; }
        /// <summary>
        /// 实体描述
        /// </summary>
        [DisplayName("实体描述")]
        public string Description { get; set; }
        /// <summary>
        /// 实体属性信息
        /// </summary>
        [DisplayName("实体属性信息")]
        public List<EntityPropertyDto> Properties { get; set; }

    }
}
