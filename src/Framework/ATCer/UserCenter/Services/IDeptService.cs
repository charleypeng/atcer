// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.UserCenter.Dtos;
using ATCer.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDeptService : IServiceBase<DeptDto, int>
    {
        /// <summary>
        /// 查询所有部门 按树形结构返回
        /// </summary>
        /// <returns></returns>
        Task<List<DeptDto>> GetTree(bool includeLocked = false);

        /// <summary>
        /// 获取资源的种子数据
        /// </summary>
        /// <returns></returns>
        Task<string> GetSeedData();
    }
}
