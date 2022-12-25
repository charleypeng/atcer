// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Enums;
using ATCer.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace ATCer.Attachment.Domains
{
    /// <summary>
    /// 附件
    /// </summary>
    [Description("附件信息")]
    public class Attachment : GardenerEntityBase<Guid>
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [MaxLength(64)]
        [DisplayName("业务编号")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [DisplayName("业务类型")]
        public AttachmentBusinessType BusinessType { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        [DisplayName("类型")]
        public AttachmentFileType FileType { get; set; }
        /// <summary>
        /// 原始类型
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("原始类型")]
        public string ContentType { get; set; }
        /// <summary>
        /// 文件大小 byte
        /// </summary>
        [DisplayName("大小")]
        public long Size { get; set; }
        /// <summary>
        /// 存储地址 无name
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("路径")]
        public string Path { get; set; }
        /// <summary>
        /// 文件名称 随机生成
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        [MaxLength(100)]
        [DisplayName("原始名称")]
        public string OriginalName { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("访问地址")]
        public string Url { get; set; }
        /// <summary>
        /// 后缀
        /// .jpg
        /// </summary>
        [Required, MaxLength(20)]
        [DisplayName("后缀")]
        public string Suffix { get; set; }
    }
}
