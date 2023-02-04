// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyHttpMethod = ATCer.Enums.MyHttpMethod;

namespace ATCer.Base.Domains
{
    /// <summary>
    /// 功能信息
    /// </summary>
    [Description("功能信息")]
    public class Function : ATCerEntityBase<Guid>
    {
        /// <summary>
        /// 分组
        /// </summary>
        [MaxLength(200)]
        [DisplayName("分组")]
        public string? Group { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        [MaxLength(200)]
        [DisplayName("服务")]
        public string? Service { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        [MaxLength(100)]
        [DisplayName("概要")]
        public string? Summary { get; set; }

        /// <summary>
        /// 唯一键
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("唯一键")]
        public string? Key { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500)]
        [DisplayName("描述")]
        public string? Description { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("地址")]
        public string? Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        [DisplayName("请求方法")]
        public MyHttpMethod Method { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        [DisplayName("启用审计")]
        public bool EnableAudit { get; set; }

        /// <summary>
        /// 多对多中间表
        ///</summary>
        public List<ResourceFunction> ResourceFunctions { get; set; }

    }
}
