// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.Attachment.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadAttachmentOutput
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
    }
}
