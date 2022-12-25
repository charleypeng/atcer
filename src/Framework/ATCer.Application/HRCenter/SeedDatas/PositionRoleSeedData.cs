//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊  
////  CopyRight(C) 2022  版权所有 
//// -----------------------------------------------------------------------------

//using ATCer.HRCenter.Domains;
//using Furion.DatabaseAccessor;
//using System;
//using System.Collections.Generic;

//namespace ATCer.HRCenter.SeedDatas
//{
//    /// <summary>
//    /// 岗位角色种子数据
//    /// </summary>
//    public class PositionRoleSeedData: IEntitySeedData<PositionRole>
//    {
//        /// <summary>
//        /// Seed data
//        /// </summary>
//        /// <param name="dbContext"></param>
//        /// <param name="dbContextLocator"></param>
//        /// <returns></returns>
//        public IEnumerable<PositionRole> HasData(DbContext dbContext, Type dbContextLocator)
//        {
//            return new PositionRole[]
//            {
//                new PositionRole{Id = 1, Code = "InCommand", Name = "指挥席"},
//                new PositionRole{Id = 2, Code = "CoCommand", Name = "协调席"},
//                new PositionRole{Id = 3, Code = "Plan", Name = "计划席"},
//                new PositionRole{Id = 4, Code = "Report", Name = "报告席"},
//                new PositionRole{Id = 5, Code = "FlowControl", Name = "流控席"},
//                new PositionRole{Id = 6, Code = "Supervisor", Name = "带班席"},
//                new PositionRole{Id = 7, Code = "CoSupervisor", Name = "代协"}
//            };
//        }
//    }
//}
