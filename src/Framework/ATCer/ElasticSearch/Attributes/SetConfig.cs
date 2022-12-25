// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.ElasticSearch
{
    /// <summary>
    /// 设置连接字符
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SetConfig : Attribute
    {
        private string _configString;
        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="connectionString">string</param>
        public SetConfig(string connectionString)
        {
            _configString = connectionString;
        }

        /// <summary>
        /// Entity名字
        /// </summary>
        public string ConfigString => _configString;
    }
}
