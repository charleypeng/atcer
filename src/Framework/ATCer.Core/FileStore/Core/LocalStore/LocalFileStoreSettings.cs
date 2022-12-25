// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace ATCer.FileStore.Core.LocalStore
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalFileStoreSettings : IConfigurableOptions
    {
        /// <summary>
        /// 存储的基础目录,在wwwroot下
        /// 为空时，默认是upload 路径
        /// </summary>
        public string BaseDirectory { get; set; }
    }
}
