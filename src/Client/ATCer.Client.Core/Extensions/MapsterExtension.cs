// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using ATCer.Base;
using ATCer.Enums;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ATCer.Client
{
    public static class MapsterExtension
    {

        public static IServiceCollection AddTypeAdapterConfigs(this IServiceCollection services)
        {
            TypeAdapterConfig<ITableSortModel, ListSortDirection>
                    .NewConfig()
                    .Map(s => s.FieldName, d => d.FieldName)
                    .Map(s => s.SortType, d => d.Sort== "descend" ? ListSortType.Desc : ListSortType.Asc);

            return services;
        }

    }
}
