// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.Attachment.Dtos;
using ATCer.Attachment.Services;
using ATCer.FileStore;
using ATCer.UserCenter.Impl.Domains;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Hosting;
using gBase = ATCer.Base;

namespace ATCer.HRCenter.Services;

/// <summary>
/// 执勤小时服务
/// </summary>
[ApiDescriptionSettings("HRCenterServices")]
public class TimeItemService : ServiceBase<TimeItem, TimeItemDto, long>, ITimeItemService, IScoped
{
    #region fields
    private readonly IRepository<UserATCInfo> _userATCInfoRepo;
    private readonly IRepository<Sector> _sectorRepo;
    private readonly IFileStoreService _fileStoreService;
    private readonly IAttachmentService _attachmentService;
    private readonly IRepository<WorkTimeConf> _workTimeConfRepo;
    private readonly IRepository<TimeItem> _timeItemRepo;
    private readonly IRepository<User> _user;
    private readonly IImporter _importer;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ILogger<TimeItemService> _logger;
    private WorkTimeConf? _workTimeConf;
    /// <summary>
    /// Init
    /// </summary>
    public TimeItemService(IRepository<TimeItem> repository,
                           IRepository<UserATCInfo> userATCInfoRepo,
                           IRepository<Sector> sectorRepo,
                           IFileStoreService fileStoreService,
                           IRepository<WorkTimeConf> workTimeConfRepo,
                           IRepository<TimeItem> timeItemRepo,
                           IRepository<User> user,
                           IWebHostEnvironment hostingEnvironment,
                           IAttachmentService attachmentService,
                           ILogger<TimeItemService> logger) : base(repository)
    {
        _userATCInfoRepo = userATCInfoRepo;
        _sectorRepo = sectorRepo;
        _fileStoreService = fileStoreService;
        _attachmentService = attachmentService;
        _workTimeConfRepo = workTimeConfRepo;
        _timeItemRepo = timeItemRepo;
        _user = user;
        _importer = new ExcelImporter();
        _hostingEnvironment = hostingEnvironment;
        _logger = logger;
        _workTimeConf = null!;
    }
    #endregion

    #region controller
    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override async Task<MyPagedList<TimeItemDto>> Search(MyPageRequest request)
    {
        //get the 
        //var x = await _repository.Where(x => x.ControllerRole == Enums.ControllerRole.Caoch).ToListAsync();

        IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();

        var timeItem = _repository.AsDefaultQuaryable(false);
        var sector = _sectorRepo.AsQueryable(false);
        var atcInfo = _userATCInfoRepo.AsQueryable(false);

        Expression<Func<TimeItemDto, bool>> expression = filterService.GetExpression<TimeItemDto>(request.FilterGroups);
        var timeItems = from a in timeItem
                        join b in atcInfo
                        on a.UserATCInfoId equals b.Id
                        join c in sector
                        on a.SectorId equals c.Id
                        select new TimeItemDto
                        {
                            Id = a.Id,
                            
                            IsDeleted = a.IsDeleted,
                            BeginTime = a.BeginTime,
                            EndTime = a.EndTime,
                            TypeOfLogin = a.TypeOfLogin,
                            TypeOfLogout = a.TypeOfLogout,
                            Confirmed = a.Confirmed,
                            ControllerRole = a.ControllerRole,
                            CreatedTime = a.CreatedTime,
                            IsLocked = a.IsLocked,
                            SectorId = a.SectorId,
                            UpdatedTime = a.UpdatedTime,
                            UserATCInfoId = a.UserATCInfoId,
                            WorkTimeConfId = a.WorkTimeConfId,
                        };
        var pageList = await timeItems.Where(expression).ToPageAsync(request.PageIndex, request.PageSize);
        return pageList;
    }

    /// <summary>
    /// 获取导入数据
    /// </summary>
    /// <returns></returns>
    public async Task<gBase.MyPagedList<TimeItemDto>> GetImported(int pageIndex = 1, int pageSize = 10)
    {
        var query = from a in _repository.AsQueryable(false).Where(x => x.Confirmed == false)
                    join b in _sectorRepo.AsQueryable(false)
                    on a.SectorId equals b.Id
                    join c in _userATCInfoRepo.AsQueryable(false)
                    on a.UserATCInfoId equals c.Id
                    select new TimeItemDto
                    {
                        Id = a.Id,
                        BeginTime = a.BeginTime,
                        EndTime = a.EndTime,

                        IsLocked = a.IsLocked,

                        Confirmed = a.Confirmed,
                        ControllerRole = a.ControllerRole,
                        CreatedTime = a.CreatedTime,
                        IsDeleted = a.IsDeleted,
                        SectorId = a.SectorId,
                        TypeOfLogin = a.TypeOfLogin,
                        TypeOfLogout = a.TypeOfLogout,
                        UpdatedTime = a.UpdatedTime,
                        UserATCInfoId = a.UserATCInfoId,
                        WorkTimeConfId = a.WorkTimeConfId
                    };
        var data = await query.ToPageAsync(pageIndex, pageSize);
        return data;
    }

    /// <summary>
    /// 导入Excel数据
    /// </summary>
    /// <param name="input"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<ImportFromExcelOutput> ImportViaFile([FromForm] UploadAttachmentInput input, IFormFile file)
    {
        if (file == null)
            throw Oops.Oh(ExceptionCode.NO_INCLUD_FILE);
        if (input.BusinessType != ATCer.Attachment.Enums.AttachmentBusinessType.TimeItem)
            throw Oops.Oh(ExceptionCode.INVALID_BUSSINESS_TYPE);

        var ext = Path.GetExtension(file.FileName);
        // only xlsx is supported
        if (ext.ToLower() != ".xlsx")
            throw Oops.Oh(ExceptionCode.INVALID_BUSSINESS_TYPE);

        var attachment = input.Adapt<AttachmentDto>();
        //upload excel file
        await _attachmentService.Upload(input, file);
        //begin process timeitem
        var stream = file.OpenReadStream();

        return await importTimeItems(stream);
    }

    /// <summary>
    /// 导入Excel数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ImportFromExcelOutput> ImportViaId([FromBody] Guid id)
    {
        var attachment = await _attachmentService.Get(id);
        if (attachment == null)
            throw Oops.Oh("小时数据不存在");

        var result = await _repository.Where(x => x.Confirmed == false).BatchDeleteAsync();

        _logger.LogInformation($"{result}条未导入的小时数据已删除");

        var stream = _fileStoreService.Get(attachment.Path + attachment.Name);

        return await importTimeItems(stream);
    }
    /// <summary>
    /// 批量导入小时数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> BulkInsert(IEnumerable<TimeItemDto> input)
    {
        if (input == null || input.Count() == 0)
            throw Oops.Oh(ExceptionCode.VALUE_CANNOT_BE_NULL);

        await _repository.Context.BulkInsertAsync(input.Adapt<IList<TimeItem>>());
        return true;
    }

    /// <summary>
    /// 立即导入
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<bool> ImportNow()
    {
        //get where all confirmed is false and then set as true
        //to import all the data
        var result = await _repository
                        .Where(x => x.Confirmed == false)
                        .ExecuteUpdateAsync(x => x.SetProperty(x => x.Confirmed, x => true));

        _logger.LogInformation($"{result}条小时数据已更新");
        return true;
    }

    /// <summary>
    /// 删除先前导入项
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<bool> DeleteRecentImported()
    {
        //batch fake delete all the items where import confirmed is false
        var result = await _repository.Where(x => x.Confirmed == false)
                                      .ExecuteDeleteAsync();

        _logger.LogInformation($"{result}条小时数据已删除");
        return true;
    }
    #endregion

    string[] source = new string[10] { "TWRPRE", "ZTZX08", "ZTZXO9", "ZTZX10", "地面监控席", "西塔监控席", "地面管制席", "GND", "TWRC,TWRE", "TWR,TWRF" };

    #region time item importer
    private async Task<ImportFromExcelOutput> importTimeItems(Stream stream)
    {
        //get work time config
        _workTimeConf = await _workTimeConfRepo.AsDefaultQuaryable().SingleOrDefaultAsync();
        if (_workTimeConf == null)
            throw Oops.Oh("时间配置没有");
        //set input bussiness id
        var excel = await _importer.Import<ImportTimeItemDto>(stream);

        if (excel == null)
            throw Oops.Oh("导入的执勤小时数据不能为空");

        if (excel.HasError)
            throw Oops.Oh(new { info = $"导入的执勤小时数据有错请检查:{excel.Exception.Message}", errors = excel.RowErrors });

        var rowItems = excel.Data;

        var sectors = await _sectorRepo.AsDefaultQuaryable().ToListAsync();
        var users = await _userATCInfoRepo.AsDefaultQuaryable().ToListAsync();
        var lst = new List<TimeItemDto>();

        foreach (var item in rowItems)
        {
            //to ensure begin and end time  is correct
            var timeSpan = item.EndTime - item.BeginTime;

            if (timeSpan.TotalSeconds < 0)
                throw Oops.Oh($"时间错误：{item.ControllerName}:{item.BeginTime}:{item.EndTime}");

            //set the import time item format to make sure it can attach to a signle sector
            if (item.SectorCode.Equals("AS") && (item.PositionType.Equals("流控席") || item.PositionType.Equals("流控见习")))
            {
                item.SectorCode = "APPFLM";
            }
            if (item.PositionType.Contains("见习"))
            {
                item.ControllerRole = ControllerRole.Student;
            }

            if (source.Any((string x) => x.Contains(item.PysicalPosition)))
            {
                item.PysicalPosition = "ZTZX09";
                //order can not change
                //item.SectorCode = "TWR,TWRF,GND,TWRW,TWRC,TWRE";
            }
            if (item.PositionType.Equals("放行席见习"))
            {
                item.PositionType = "放行见习";
            }
            //get user 
            var user = users.Where(x => x.ATCName == item.ControllerName &&
                                   x.Department == item.PysicalPosition.ToDepartment()).Single();
            if (user == null)
                throw Oops.Oh($"管制员 {item.ControllerName} 不存在");

            //get sector
            Sector? sector;
            try
            {
                //tower user must use different sector
                if(user.Department == ATCDepartment.TWR)
                {
                    sector = sectors.Where((Sector c) => c.Department == user.Department &&
                    c.PositionName.Contains(item.PositionType)).Single();
                }
                else
                {
                    sector = sectors.Where((Sector c) => c.Department == user.Department &&
                    c.PositionName.Contains(item.PositionType) &&
                    c.Code.ReverseContain(item.SectorCode)).Single();
                }
                
            }
            catch (Exception ex)
            {
                sector = null;
                _logger.LogError(ex.Message);
            }

            if (sector == null)
                throw Oops.Oh($"扇区 {item.SectorCode}:{item.PositionType} 不存在或不止一个");

            //form time item
            var timeItem = new TimeItemDto
            {
                BeginTime = item.BeginTime,
                EndTime = item.EndTime,
                UserATCInfoId = user.Id,
                SectorId = sector.Id,
                Confirmed = false,
                WorkTimeConfId = _workTimeConf.Id,
                ControllerRole = item.ControllerRole!.Value,
                CreatedTime = DateTime.Now
            };
            lst.Add(timeItem);
        }

        if (rowItems.Count != lst.Count)
        {
            var q = from a in rowItems
                    join b in lst
                    on a.BeginTime equals b.BeginTime
                    select a;

            return new ImportFromExcelOutput
            {
                OriginTotalCount = rowItems.Count,
                ProcessedTotalCount = lst.Count,
                Succeed = false,
                Errors = q
            };
        }

        var added = await this.BulkInsert(lst);

        if (added)
        {
            var result = new ImportFromExcelOutput
            {
                OriginTotalCount = rowItems.Count,
                ProcessedTotalCount = lst.Count,
                Succeed = true,
                Errors = excel.RowErrors
            };
            return result;

        }
        else
        {
            throw Oops.Oh("在最后导入阶段出现错误");
        }
    }
    #endregion

    /// <summary>
    /// 获取用户工作小时
    /// </summary>
    /// <returns></returns>
    //public async Task<object> GetWorkerStats(DateTime? beginTime, DateTime? endTime)
    //{
    //    _logger.LogInformation("start processing...");

    //    if (beginTime == null || endTime == null)
    //        throw Oops.Oh("开始和结束时间不能为空");

    //    var span = endTime - beginTime;

    //    if (span > TimeSpan.FromDays(32))
    //        throw Oops.Oh("查询时间应该小于一个月");
        
    //    //get also deleted and locked sectors
    //    var qSector = _sectorRepo.AsQueryable(false);
    //    var qTimeItem = _timeItemRepo.AsDefaultQuaryable();
    //    var qUserInfo = _userATCInfoRepo.AsDefaultQuaryable();
       
    //    //get can cat3 people timeitem
    //    var qCat3ATC =  from a in qTimeItem
    //                    where a.Confirmed == true
    //                    join b in qUserInfo
    //                    on a.UserATCInfoId equals b.Id
    //                    where b.CanCat3 == false || b.IsCat3 == true
    //                    join c in qSector
    //                    on a.SectorId equals c.Id
    //                    where c.Cat3Sector == true
    //                    select new
    //                    {
    //                        SectorCode = c.Code,
    //                        TimeItem = new TimeItemDto
    //                        {
    //                            Id = a.Id,
    //                            BeginTime = a.BeginTime,
    //                            EndTime = a.EndTime,
    //                            UserName = b.ATCName,
    //                            IsLocked = a.IsLocked,
    //                            SectorName = c.Name,
    //                            Confirmed = a.Confirmed,
    //                            ControllerRole = a.ControllerRole,
    //                            CreatedTime = a.CreatedTime,
    //                            IsDeleted = a.IsDeleted,
    //                            SectorId = a.SectorId,
    //                            TypeOfLogin = a.TypeOfLogin,
    //                            TypeOfLogout = a.TypeOfLogout,
    //                            UpdatedTime = a.UpdatedTime,
    //                            UserATCInfoId = a.UserATCInfoId,
    //                            WorkTimeConfId = a.WorkTimeConfId   
    //                        },
    //                        SectorInfo  = c,
    //                        UserInfo = b
    //                    };

    //    //get the expected cat3 timeitem query
    //    //grouped by date
    //    //then grouped by sector
    //    var qGroupedByDate = qCat3ATC.GroupBy(x => x.TimeItem.BeginTime.Date)
    //        .Select(x => new
    //        {
    //            Date = x.Key,
    //            DataByDate = x.GroupBy(x => x.SectorCode)
    //                .Select(x => new { Sector = x.Key, Data = x.ToList()})
    //        });

    //    List<Cat3ATCStats>? finalQuery = new List<Cat3ATCStats>();
    //    List<List<long>> comparedList = new List<List<long>>();

    //    //date list
    //    foreach (var dateData in qGroupedByDate)
    //    {
    //        //sector list
    //        foreach (var sectorData in dateData.DataByDate)
    //        {
    //            var dtList = sectorData.Data.ToList();

    //            for (int i = 0; i < sectorData.Data.Count(); i++)
    //            {
    //                var dt1 = dtList[i];

    //                //compare
    //                //we only compare cat3 people
    //                if (dt1.UserInfo.IsCat3)
    //                {
    //                    //
    //                    var lst = dtList
    //                        //get where the sector and user is not the same
    //                        .Where(x => x.UserInfo.Id != dt1.UserInfo.Id)
    //                        //time item comparer
    //                        .Where(x => !(x.TimeItem.BeginTime > dt1.TimeItem.EndTime || x.TimeItem.EndTime < dt1.TimeItem.BeginTime));

    //                    //cotinue if not null
    //                    if (!lst.IsNullOrEmpty())
    //                    {
    //                        Cat3ATCStats? final = null;

    //                        //to make sure wheather they are doing the handling job
    //                        var isHandling = lst.Any(x => x.SectorInfo.Id == dt1.SectorInfo.Id &&
    //                            x.UserInfo.Id != dt1.UserInfo.Id);
    //                        //if true
    //                        if(isHandling)
    //                        {
    //                            //to make sure the sector have a valid non cat3 or cancat3=false atc
    //                            var isValidHandling = lst.Any(x => x.SectorInfo.Id != dt1.SectorInfo.Id &&
    //                                x.UserInfo.CanCat3 == true);

    //                            if(isValidHandling)
    //                            {
    //                                final = new Cat3ATCStats
    //                                {
    //                                    Date = dateData.Date,
    //                                    Sector = sectorData.Sector,
    //                                    Violator = lst.Select(x=>x.TimeItem.Adapt<TimeItemDto>()),
    //                                    Comparer = dt1.TimeItem.Adapt<TimeItemDto>()
    //                                };
    //                            }
    //                        }
    //                        else
    //                        {
    //                            final = new Cat3ATCStats
    //                            {
    //                                Date = dateData.Date,
    //                                Sector = sectorData.Sector,
    //                                Violator = lst.Select(x => x.TimeItem.Adapt<TimeItemDto>()),
    //                                Comparer = dt1.TimeItem.Adapt<TimeItemDto>()
    //                            };
    //                        }

    //                        //make sure the comparer is not exist before;

    //                        if(final != null)
    //                        {
    //                            var isComparerExist = finalQuery?.Any(x => x.Comparer?.Id == final.Comparer?.Id);
    //                            var fList = finalQuery?.Select(x => x.Violator).ToList();

    //                            if (isComparerExist == false)
    //                            {
    //                                final.UID = Guid.NewGuid();
    //                                finalQuery?.AddNonNullObject(final);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    var violatorList = new List<TimeItemDto>();
    //    //remove redundant items
    //    foreach (var item in finalQuery)
    //    {
    //        if(item?.Violator != null)
    //        {
    //            foreach (var item2 in item?.Violator!)
    //            {
    //                item2.GroupId = item.UID;
    //                violatorList.Add(item2);
    //            }
    //        }
    //    }

    //    var strList = new List<Guid?>();

    //    foreach (var item in finalQuery)
    //    {
    //        var items = violatorList.Where(x => x.Id == item?.Comparer?.Id);
    //        if (items != null && items.Count() > 1)
    //        {
    //            var items2 = items.FirstOrDefault();
    //            strList.AddUnique(items2?.GroupId);
    //        }
    //        else
    //        {
    //            strList.AddUnique(item.UID);
    //        }
    //    }

    //    var selector = from a in finalQuery
    //                   join b in strList
    //                   on a.UID equals b
    //                   select a;
    //    //var dt = await qGroupedByDate.ToListAsync();
    //    //var query = from a in _sectorRepo.AsQueryable(false)
    //    //            join b in _timeItemRepo.AsDefaultQuaryable(false)
    //    //            on a.Id equals b.SectorId
    //    //            join c in _userATCInfoRepo.AsQueryable(false)
    //    //            on b.UserId equals c.Id
    //    //            where b.BeginTime >= beginTime && b.EndTime <= endTime
    //    //            where a.Cat3Sector == true && c.IsCat3 == true
    //    //            select new Tuple<Sector, TimeItem, UserATCInfo>(a, b, c);

    //    //var query2 = from a in _sectorRepo.AsQueryable(false)
    //    //             join b in _timeItemRepo.AsDefaultQuaryable(false)
    //    //             on a.Id equals b.SectorId
    //    //             join c in _userATCInfoRepo.AsQueryable(false)
    //    //             on b.UserId equals c.Id
    //    //             where b.BeginTime >= beginTime && b.EndTime <= endTime
    //    //             where c.CanCat3 == false
    //    //             select new Tuple<Sector, TimeItem, UserATCInfo>(a, b, c);

    //    //var rawData1 = await query.ToListAsync();
    //    //var stats = rawData1.GroupBy(x => x.Item1.Code).Select(x => new { Code = x.Key, Data = x });

    //    //List<IEnumerable<Tuple<Sector, TimeItem, UserATCInfo>>> dd = new List<IEnumerable<Tuple<Sector, TimeItem, UserATCInfo>>>();
    //    //foreach (var stat in stats)
    //    //{
    //    //    //group by date
    //    //    var statData1 = stat.Data.GroupBy(x => x.Item2.BeginTime.Date)
    //    //        .Select(x => new StatData { Date = x.Key, Data = x.OrderBy(x => x.Item2.BeginTime) })
    //    //        .ToList();

    //    //    foreach (var stat1 in statData1)
    //    //    {
    //    //        var statData2 = stat1.Data.ToList();
    //    //        //search for data item time
    //    //        for (int i = 0; i < statData2.Count; i++)
    //    //        {
    //    //            var compare1 = statData2[i];
    //    //            var data1 = statData2.Where(x => x.Item1.Position != compare1.Item1.Position).Where(x => !(x.Item2.BeginTime > compare1.Item2.EndTime
    //    //                || x.Item2.EndTime < compare1.Item2.BeginTime))
    //    //                .Where(x => x.Item3.Id != compare1.Item3.Id).OrderBy(x => x.Item2.BeginTime);

    //    //            dd.AddNonNullListObject(data1);
    //    //        }
    //    //    }
    //    //}
    //    return selector!;//.GroupBy(x=>x.GroupBy(x=>x.Item1.Code)).Select(x=>new {code = x.Key, data = x});
    //}

    /// <summary>
    /// 获取用户工作小时
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetWorkerStatsV2(DateTime? beginTime, DateTime? endTime)
    {
        _logger.LogInformation("start processing...");
        //check area
        if (beginTime == null || endTime == null)
            throw Oops.Oh("开始和结束时间不能为空");

        var span = endTime - beginTime;

        if(span?.TotalSeconds < 0)
            throw Oops.Oh("查询时间输入有误");

        if (span > TimeSpan.FromDays(32))
            throw Oops.Oh("查询时间应该小于一个月");

        //get the cat3 atc query
        var cat3ATC = _timeItemRepo
            .AsDefaultQuaryable()
            .Include(x => x.UserATCInfo)
            .Include(x => x.Sector)
            .Where(x=>x.Sector!.Cat3Sector == true)
            .Where(x=>x.UserATCInfo != null && x.Sector != null)
            .Where(x => x.BeginTime >= beginTime && x.EndTime <= endTime);
        //get the grouped query
        var groupedList = cat3ATC.GroupBy(x => x.BeginTime.Date)
            .Select(x => new
            {
                Date = x.Key,
                DataByDate = x.GroupBy(x => x.Sector!.Code)
                .Select(x => new
                {
                    SectorCode = x.Key,
                    Data = x.ToList()
                })
            });

        //List<long> ids = new List<long> { 123, 12345, 122521, 123556 };

        //List<long> ids2 = new List<long> { 122521, 12345, 123, 123556 };

        //List<long> ids3 = new List<long> { 122521, 12345, 123, 123556, 112 };
        //var se = new List<List<long>>
        //{
        //    ids, ids2
        //};

        //var ssw = se.NoOrderContains(ids);
        //var ss = ids.SameAs(ids3);

        var comparedList = new List<List<long>>();
        List<Cat3ATCStats>? finalList = new List<Cat3ATCStats>();
        //group by date
        foreach (var dateItem in groupedList)
        {
            //group by sector
            foreach (var sectorItem in dateItem.DataByDate)
            {
                var cpList = new List<long>();
                for (int i = 0; i < sectorItem.Data.Count; i++)
                {
                    var comparer = sectorItem.Data[i];

                    //will not check non cat3 people
                    if (comparer.UserATCInfo!.IsCat3 == false) continue;

                    var violators = sectorItem.Data
                        .Where(x => x.SectorId != comparer.SectorId)
                        .Where(x => x.UserATCInfoId != comparer.UserATCInfoId)
                        .Where(x => x.Sector!.Cat3Sector == true)
                        .Where(x => x.UserATCInfo!.IsCat3 == true || x.UserATCInfo.CanCat3 == false)
                        .Where(x => !(x.BeginTime > comparer.EndTime || x.EndTime < comparer.BeginTime));

                    if (violators == null || violators.Count() == 0) continue;

                    cpList.Add(comparer.Id);
                    cpList.AddRange(violators.Select(x => x.Id));

                    if(!comparedList.NoOrderContains(cpList))
                    {
                        comparedList.Add(cpList);
                        finalList.Add(new Cat3ATCStats
                        {
                            Comparer = comparer.Adapt<TimeItemDto>(),
                            Date = dateItem.Date,
                            Sector = sectorItem.SectorCode,
                            Violator = violators.Adapt<IEnumerable<TimeItemDto>>(),
                        });
                    }
                };
            }
        }
        return finalList;
    }
}

public class StatData
{
    public DateTime Date { get; set; }
    public IEnumerable<Tuple<Sector, TimeItem, UserATCInfo>> Data { get; set; }
}