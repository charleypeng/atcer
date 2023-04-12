// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Enums;
using ATCer.Authentication.Enums;
using ATCer.Common;
using ATCer.Enums;

namespace ATCer.Client.Base.Constants
{
    public class TableFiltersConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<MyHttpMethod>[] FunctionMethodFilters = EnumHelper.EnumToList<MyHttpMethod>().Select(x => { return new TableFilter<MyHttpMethod>() { Text = x.ToString(), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<Gender>[] GenderFilters = EnumHelper.EnumToList<Gender>().Select(x => { return new TableFilter<Gender>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<EntityOperationType>[] OperationTypeFilters = EnumHelper.EnumToList<EntityOperationType>().Select(x => { return new TableFilter<EntityOperationType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<AttachmentBusinessType?>[] AttachmentBusinessTypeFilters = EnumHelper.EnumToList<AttachmentBusinessType>().Select(x => { return new TableFilter<AttachmentBusinessType?>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<AttachmentFileType?>[] AttachmentFileTypeFilters = EnumHelper.EnumToList<AttachmentFileType>().Select(x => { return new TableFilter<AttachmentFileType?>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<LoginClientType>[] LoginClientTypeFilters = EnumHelper.EnumToList<LoginClientType>().Select(x => { return new TableFilter<LoginClientType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();/// <summary>
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<IdentityType>[] IdentityTypeFilters = EnumHelper.EnumToList<IdentityType>().Select(x => { return new TableFilter<IdentityType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
    }
}
