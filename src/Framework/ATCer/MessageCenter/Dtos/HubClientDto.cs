// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System;

namespace ATCer.MessageCenter.Dtos
{
    public class HubClientDto: ATCerBaseDto<int>
    {
        public int UserID { get; set; }
        public string ClientId { get; set; }
        public int Dep { get; set; }
        public string Remark { get; set; }
        public DateTimeOffset LoginTime { get; set; }
        public DateTimeOffset? LogoutTime { get; set; }
    }
}
