// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.CodeGeneration.Dtos
{
    /// <summary>
    /// 实体属性信息
    /// </summary>
    [Description("实体属性信息")]
    public class EntityPropertyDto
    {

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("字段名称")]
        public string FieldName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string DataTypeName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string DataTypeFullName { get; set; }

        /// <summary>
        /// 是否是可为NULL类型
        /// </summary>
        [DisplayName("是否是可为NULL类型")]
        public bool IsNullableType { get; set; }

    }
}
