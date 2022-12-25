// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client
{
    public struct ResourceKey
    {
        public struct HRCenter
        {
            public const string Base = "hr_center";
            public const string Admin = $"{Base}_admin";
            /// <summary>
            /// 只有查看权限
            /// </summary>
            public const string View = $"{Base}_view";
            public const string Debug = $"{Base}_debug";
        }

        public struct LTFATCenter
        {
            public const string Base = "ltfat_center";
            public const string Admin = $"{Base}_admin";
            /// <summary>
            /// 只有查看权限
            /// </summary>
            public const string View = $"{Base}_view";
            public const string Debug = $"{Base}_debug";
        }
    }
}
