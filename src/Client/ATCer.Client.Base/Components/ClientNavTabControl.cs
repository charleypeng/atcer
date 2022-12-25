// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;

namespace ATCer.Client.Base
{
    /// <summary>
    /// Client导航标签控制器
    /// </summary>
    public class ClientNavTabControl
    {
        private static ReuseTabs _reuseTabs;

        public static void SetReuseTabs(ReuseTabs reuseTabs) 
        {
            _reuseTabs = reuseTabs;
            
        }

        /// <summary>
        /// 移除多标签导航
        /// </summary>
        public static void RemoveNavTabPageWithRegex(string pattern)
        {
            if (_reuseTabs != null)
            {
                _reuseTabs.RouteView.RemovePageWithRegex(pattern);
            }
        }

        /// <summary>
        /// 移除多标签导航
        /// </summary>
        public static void RemoveAllNavTabPage()
        {
            if (_reuseTabs != null)
            {
                _reuseTabs.RouteView.RemovePageWithRegex(".*");
            }
        }
    }
}
