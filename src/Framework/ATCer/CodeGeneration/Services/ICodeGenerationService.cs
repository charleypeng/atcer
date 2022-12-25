// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.CodeGeneration.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.CodeGeneration.Services
{
    /// <summary>
    /// 代码生成服务
    /// </summary>
    public interface ICodeGenerationService
    {
        /// <summary>
        /// 获取所有实体定义
        /// </summary>
        /// <returns></returns>
        Task<List<EntityDefinitionDto>> GetEntityDefinitions();

        /// <summary>
        /// 获取实体的代码生成配置
        /// </summary>
        /// <param name="entityFullName">实体完整名称</param>
        /// <returns></returns>
        Task<EntityCodeGenerationSettingDto> GetEntityCodeGenerationSetting(string entityFullName);
        /// <summary>
        /// 添加实体的代码生成配置
        /// </summary>
        /// <param name="settingDto"></param>
        /// <returns></returns>
        Task<bool> AddEntityCodeGenerationSetting(EntityCodeGenerationSettingDto settingDto);
        /// <summary>
        /// 更新实体的代码生成配置
        /// </summary>
        /// <param name="settingDto"></param>
        /// <returns></returns>
        Task<bool> UpdateEntityCodeGenerationSetting(EntityCodeGenerationSettingDto settingDto);
    }
}
