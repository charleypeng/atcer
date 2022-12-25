// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer
{
    /// <summary>
    /// Text reading
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Read text from path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> ReadAllTextAsync(string path);
        /// <summary>
        /// read as json from given text
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        Task<T> LoadFromJsonAsync<T>(string jsonText);
        /// <summary>
        /// load from file and read as json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<T> ReadAsJsonAsync<T>(string path);
    }
}
