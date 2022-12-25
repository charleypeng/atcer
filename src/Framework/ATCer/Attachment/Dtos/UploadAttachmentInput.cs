// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Enums;
using System.ComponentModel.DataAnnotations;

namespace ATCer.Attachment.Dtos
{
    /// <summary>
    /// 上传附件
    /// </summary>
    public class UploadAttachmentInput
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [MaxLength(64,ErrorMessage = "最大长度不能大于{1}")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [Required(ErrorMessage = "附件业务类型不能为空")]
        public AttachmentBusinessType BusinessType { get; set; }
    }
}
