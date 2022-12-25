// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace ATCer.FileStore
{
    /// <summary>
    /// 文件存储服务
    /// </summary>
    public interface IFileStoreService
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        void Delete(string path);
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Stream Get(string path);
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns>文件访问路径</returns>
        Task<string> Save(Stream file, string path);

    }
}
