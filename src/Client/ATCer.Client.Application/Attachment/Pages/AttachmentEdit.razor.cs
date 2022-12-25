// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Dtos;
using ATCer.Attachment.Enums;
using ATCer.Client.Base;
using System.ComponentModel.DataAnnotations;

namespace ATCer.Attachment.Client.Pages
{
    public partial class AttachmentEdit : EditOperationDialogBase<AttachmentDto, Guid>
    {

        [Required(ErrorMessage = "业务类型不能为空")]
        private string _currentEditModelBusinessType
        {
            get
            {
                return _editModel.BusinessType.ToString();
            }
            set
            {
                _editModel.BusinessType = (AttachmentBusinessType)Enum.Parse(typeof(AttachmentBusinessType), value);
            }
        }

        [Required(ErrorMessage = "文件类型不能为空")]
        private string _currentEditModelFileType
        {
            get
            {
                return _editModel.FileType.ToString();
            }
            set
            {
                _editModel.FileType = (AttachmentFileType)Enum.Parse(typeof(AttachmentFileType), value);
            }
        }
    }
}
