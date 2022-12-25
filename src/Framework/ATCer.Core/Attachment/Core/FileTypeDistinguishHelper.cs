// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Enums;

namespace ATCer.Attachment.Core
{
    /// <summary>
    /// 识别附件文件类型
    /// </summary>
    public class FileTypeDistinguishHelper
    {
        /// <summary>
        /// 识别附件文件类型
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static AttachmentFileType GetAttachmentFileType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                return AttachmentFileType.Other;
            }
            contentType = contentType.Split("/")[0];
            switch (contentType)
            {
                case "image": return AttachmentFileType.Image;
                case "video": return AttachmentFileType.Video;
                case "audio": return AttachmentFileType.Audio;
                default:return AttachmentFileType.Other;
            }
        }
    }
}
