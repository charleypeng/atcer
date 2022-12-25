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
    /// Set the main key
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class MainKey:Attribute
    {
        /// <summary>
        /// The main key's name
        /// </summary>
        public string IdentityName { get; }

        /// <summary>
        /// Init key
        /// </summary>
        /// <param name="identityName"></param>
        public MainKey(string identityName)
        {
            IdentityName = IdentityName;
        }
    }
}
