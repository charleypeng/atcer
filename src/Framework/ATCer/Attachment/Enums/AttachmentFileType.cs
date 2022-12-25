// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Attachment.Enums
{
    /// <summary>
    /// 上传文件类型
    /// </summary>
    public enum AttachmentFileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Image,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video,
        /// <summary>
        /// 音频
        /// </summary>
        [Description("音频")]
        Audio,
        /// <summary>
        /// Other
        /// </summary>
        [Description("其他")]
        Other
    }
}
