// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.CodeGeneration.Dtos;
using ATCer.CodeGeneration.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.CodeGeneration.Client.Services
{
    /// <summary>
    /// 代码生成
    /// </summary>
    [ScopedService]
    public class CodeGenerationService : ICodeGenerationService
    {
        private static readonly string controller = "code-generation";

        private readonly IApiCaller apiCaller;

        public CodeGenerationService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<EntityCodeGenerationSettingDto> GetEntityCodeGenerationSetting(string entityFullName)
        {
            return await apiCaller.GetAsync<EntityCodeGenerationSettingDto>($"{controller}/entity-code-generation-setting/{entityFullName}");
        }

        public async Task<List<EntityDefinitionDto>> GetEntityDefinitions()
        {
            return await apiCaller.GetAsync<List<EntityDefinitionDto>>($"{controller}/entity-definitions");
        }

        public async Task<bool> AddEntityCodeGenerationSetting(EntityCodeGenerationSettingDto settingDto)
        {
            return await apiCaller.PostAsync<EntityCodeGenerationSettingDto,bool>($"{controller}/entity-code-generation-setting",settingDto);
        }

        public async Task<bool> UpdateEntityCodeGenerationSetting(EntityCodeGenerationSettingDto settingDto)
        {
            return await apiCaller.PutAsync<EntityCodeGenerationSettingDto, bool>($"{controller}/entity-code-generation-setting", settingDto);
        }
    }
}
