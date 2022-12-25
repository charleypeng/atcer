// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using ATCer.CodeGeneration.Dtos;
using ATCer.CodeGeneration.Services;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace ATCer.CodeGeneration.Client.Pages
{
    public partial class CodeGenerration
    {
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        private ICodeGenerationService codeGenerationService { get; set; }

        private int current { get; set; } = 0;

        private List<EntityDefinitionDto> entityDefinitions=new List<EntityDefinitionDto>();
        private IEnumerable<EntityDefinitionDto> selectedEntityDefinitions=new List<EntityDefinitionDto>();

        private bool entityDefinitionsLoading = false;

        private EntityCodeGenerationSettingDto selectEntityCodeGenerationSettingDto=new EntityCodeGenerationSettingDto();


        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            entityDefinitions= await codeGenerationService.GetEntityDefinitions();
        }

        private void OnPreClick()
        {
            current--;
        }

        private async Task OnNextClick()
        {
            current++;
            await StepsOnChange();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private async Task StepsOnChange()
        {
            if (current == 1)
            {
                if (selectedEntityDefinitions.Count() == 0)
                {
                    messageService.Warn("请选择实体");
                    current = 0;
                }
                EntityDefinitionDto entityDefinition = selectedEntityDefinitions.FirstOrDefault();
                //加载配置
                EntityCodeGenerationSettingDto settingDto = await codeGenerationService.GetEntityCodeGenerationSetting(entityDefinition.FullName);

                if (settingDto != null)
                {
                    settingDto.Adapt(selectEntityCodeGenerationSettingDto);
                }
            }
        }
    }
}
