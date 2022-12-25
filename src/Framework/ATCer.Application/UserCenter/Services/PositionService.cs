// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.UserCenter.Impl.Domains;
using ATCer.UserCenter.Dtos;
using Microsoft.AspNetCore.Mvc;
using ATCer.UserCenter.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Mapster;

namespace ATCer.UserCenter.Impl.Services
{
    /// <summary>
    /// 岗位管理服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class PositionService : ServiceBase<Position, PositionDto, int>, IPositionService
    {

        private readonly IRepository<Position> _positionRepository;

        /// <summary>
        /// 查询所有可以用的
        /// </summary>
        /// <remarks>
        /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        public override async Task<List<PositionDto>> GetAllUsable()
        {
            return await _repository.AsQueryable(false).Where(x => x.IsLocked == false && x.IsDeleted == false).ProjectToType<PositionDto>().ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionService(IRepository<Position> positionRepository) : base(positionRepository)
        {
            this._positionRepository = positionRepository;
        }

    }
}
