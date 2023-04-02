// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Dtos;
using EFCore.BulkExtensions;

namespace ATCer.HRCenter.Services;

/// <summary>
/// 扇区服务
/// </summary>
[ApiDescriptionSettings("HRCenterServices")]
public class SectorService : ATCer.ServiceBase<Sector, SectorDto, int>, ISectorService
{
    /// <summary>
    /// Init
    /// </summary>
    /// <param name="repository"></param>
    public SectorService(IRepository<Sector> repository) : base(repository)
    {

    }

    /// <summary>
    /// 测试1
    /// </summary>
    /// <returns></returns>
    public Task<Dictionary<int, string>> GetSectorsDict()
    {
        return Task.FromResult(StaticDict.SectorNameDict());
    }

    /// <summary>
    /// 更新扇区数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<bool> Update(SectorDto input)
    {
        var query = _repository.AsQueryable(false);

        if (input == null)
            throw Oops.Oh(ExceptionCode.VALUE_CANNOT_BE_NULL);

        var data = await query.Where(x => x.Id == input.Id).SingleOrDefaultAsync();

        if (data == null)
            return false;

        // must delete the old one and insert a new one
        data.IsDeleted = true;
        var isDeleted = await this.FakeDelete(data.Id);

        if (isDeleted)
        {
            var newInput = input.Adapt<SectorDto>();
            newInput.Id = 0;

            var result = await base.Insert(newInput);
            //send message
            return result == null ? false : true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 添加扇区
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<SectorDto> Insert(SectorDto input)
    {
        if (input == null)
            throw Oops.Oh(ExceptionCode.VALUE_CANNOT_BE_NULL);
        // the input must not exist
        var isExist = await _repository.AsDefaultQuaryable()
                                       .Where(x => x.Code == input.Code || x.Name == input.Name)
                                       .AnyAsync();
        if (isExist)
        {
            throw Oops.Oh(ExceptionCode.ALREADY_EXIST);
        }

        return await base.Insert(input);
    }
    /// <summary>
    /// 添加新区数据
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task ImportSector([FromBody] IList<ImportSectorDto> sector)
    {
        var sectors = sector.Adapt<IList<Sector>>();
        await _repository.Context.BulkInsertAsync(sectors);
    }
}
