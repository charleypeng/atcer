// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Dtos;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using ATCer.UserCenter.Impl.Domains;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;

namespace ATCer.HRCenter.Services
{
    /// <summary>
    /// 管制员信息
    /// </summary>
    [ApiDescriptionSettings("HRCenterServices")]
    public class UserATCInfoService:ServiceBase<UserATCInfo, UserATCInfoDto,int>, IUserATCInfoService
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="repository"></param>
        public UserATCInfoService(IRepository<UserATCInfo> repository):base(repository)
        {

        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task ImportData(IEnumerable<UserATCInfoDto> data)
        {
            var datas = data.Adapt<IList<UserATCInfo>>();
            await _repository.Context.BulkInsertAsync(datas);
        }

        /// <summary>
        /// 自动生成带管制员信息的用户
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AutoGenerateUser()
        {
            var userRepo = Db.GetRepository<User>();
            var userService = App.GetService<IUserService>();
            var depRepo = Db.GetRepository<Dept>();
            var atcs = await _repository.AsQueryable().ToListAsync();
            var roleRepo = Db.GetRepository<UserRole>();

            if (atcs == null || atcs.Count == 0)
                return false;

            foreach (var atc in atcs)
            {
                var dep = await depRepo.Where(x => x.Name == ATCer.Common.EnumHelper.GetEnumDescription(atc.Department)).FirstOrDefaultAsync();
                var depId = dep == null ? 2 : dep.Id;
                var user = new UserDto
                {
                    UserName = atc.ATCName,
                    Password = "hnatc@2022",
                    DeptId = depId,
                    NickName = atc.ATCName,
                    ATCInfoId = atc.Id,
                    Gender = Gender.Male,
                    PositionId = 3
                };
                var result = await userService.Insert(user);
            }

            foreach (var atc in atcs)
            {
                var user = await userRepo.Where(x => x.ATCInfoId == atc.Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    await roleRepo.InsertAsync(new UserRole { UserId = user.Id, RoleId = 2 });
                }
            }
            return true;
        }

        /// <summary>
        /// 自动生成角色
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AutoGenerateRole()
        {
            var userRepo = Db.GetRepository<User>();
            var userService = App.GetService<IUserService>();
            var depRepo = Db.GetRepository<Dept>();
            var atcs = await _repository.AsQueryable().ToListAsync();
            var roleRepo = Db.GetRepository<UserRole>();

            if (atcs == null || atcs.Count == 0)
                return false;

            foreach (var atc in atcs)
            {
                var user = await userRepo.Where(x => x.ATCInfoId == atc.Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    await roleRepo.InsertAsync(new UserRole { UserId = user.Id, RoleId = 2 });
                }
            }
            await roleRepo.SaveNowAsync();
            return true;
        }
    }
}
