// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.Client.Base
{
    /// <summary>
    /// api的配置
    /// 如何装配需要看<see cref="ApiSettingExtension"/>
    /// </summary>
    public class ApiSettings
    {

        public String BaseAddres { get { return this.Host + ":" + this.Port+"/"+this.BasePath+"/"; } }
        public String Host { get; set; }
        public String Port { get; set; }
        public String BasePath { get; set; }
        public String UploadPath { get; set; }

    }
}
