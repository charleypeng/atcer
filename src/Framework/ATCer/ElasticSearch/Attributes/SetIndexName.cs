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
    /// 设置Index名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SetIndexName:Attribute
    {
        private string _name;

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name">string</param>
        public SetIndexName(string name)
        {
            _name = name;
        }
        
        /// <summary>
        /// Entity名字
        /// </summary>
        public string Name 
        {
            get 
            {
                return _name.ToLower();
            }
        }
    }
}
