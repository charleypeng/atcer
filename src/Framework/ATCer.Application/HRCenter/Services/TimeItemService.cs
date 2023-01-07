// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Dtos;
using ATCer.Attachment.Dtos;
using ATCer.Attachment.Services;
using ATCer.FileStore;
using ATCer.UserCenter.Impl.Domains;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Hosting;
using gBase = ATCer.Base;

namespace ATCer.HRCenter.Services
{
    /// <summary>
    /// 执勤小时服务
    /// </summary>
    [ApiDescriptionSettings("HRCenterServices")]
    public class TimeItemService : ATCer.ServiceBase<TimeItem, TimeItemDto, long>, ITimeItemService, IScoped
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
        }
        #endregion

        #region controller
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<ATCer.Base.MyPagedList<TimeItemDto>> Search(MyPageRequest request)
        {
            var x = await _repository.Where(x => x.ControllerRole == Enums.ControllerRole.Caoch).ToListAsync();

            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();

            var timeItem = _repository.AsDefaultQuaryable(false);
            var sector = _sectorRepo.AsQueryable(false);
            var atcInfo = _userATCInfoRepo.AsQueryable(false);

            Expression<Func<TimeItemDto, bool>> expression = filterService.GetExpression<TimeItemDto>(request.FilterGroups);
            var timeItems = from a in timeItem
                            join b in atcInfo
                            on a.UserId equals b.Id
                            join c in sector
                            on a.SectorId equals c.Id
                            select new TimeItemDto
                            {
                                Id = a.Id,
                                UserName = b.ATCName,
                                SectorName = c.Name,
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
                                UserId = a.UserId,
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
                        on a.UserId equals c.Id
                        select new TimeItemDto
                        {
                            Id = a.Id,
                            BeginTime = a.BeginTime,
                            EndTime = a.EndTime,
                            UserName = c.ATCName,
                            IsLocked = a.IsLocked,
                            SectorName = b.Name,
                            Confirmed = a.Confirmed,
                            ControllerRole = a.ControllerRole,
                            CreatedTime = a.CreatedTime,
                            IsDeleted = a.IsDeleted,
                            SectorId = a.SectorId,
                            TypeOfLogin = a.TypeOfLogin,
                            TypeOfLogout = a.TypeOfLogout,
                            UpdatedTime = a.UpdatedTime,
                            UserId = a.UserId,
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

        #region time item importer
        private async Task<ImportFromExcelOutput> importTimeItems(Stream stream)
        {
            //get work time config
            var workConf = await _workTimeConfRepo.Where(x => x.IsDeleted == false && x.IsLocked == false).SingleOrDefaultAsync();
            if (workConf == null)
                throw Oops.Oh("时间配置没有");
            //set input bussiness id
            var excel = await _importer.Import<ImportTimeItemDto>(stream);

            var errObj = new { info = "导入的执勤小时数据有错请检查", errors = excel.RowErrors };

            if (excel.HasError)
                throw Oops.Oh("导入的执勤小时数据有错请检查", errObj);

            var rowItems = excel.Data;

            var sectors = await _sectorRepo.AsDefaultQuaryable(false).ToListAsync();
            var users = await _userATCInfoRepo.AsDefaultQuaryable(false).ToListAsync();
            var lst = new List<TimeItemDto>();

            foreach (var item in rowItems)
            {
                if (item.SectorCode.Contains(",") && item.SectorCode.Contains("TWR"))
                {
                    item.SectorCode = "TWR";
                    item.PysicalPosition = "ZTZX09";
                    goto wo1;
                }
                if (item.SectorCode.Contains("TWR"))
                {
                    item.SectorCode = "TWR";
                    item.PysicalPosition = "ZTZX09";
                    goto wo1;
                }
                if (item.PositionType.Contains("塔台") || item.PositionType.Contains("地面"))
                {
                    item.SectorCode = "TWR";
                    item.PysicalPosition = "ZTZX09";
                    goto wo1;
                }
            wo1:

                if (item.PositionType.Equals("放行席见习"))
                {
                    item.PositionType = "放行见习";
                }
                //error fix for approach controller
                if (item.SectorCode.Equals("AS"))
                {
                    if (item.PositionType.Equals("流控席") || item.PositionType.Equals("流控见习"))
                    {
                        item.SectorCode = "APPFLM";
                    }
                }
                //get user
                var user = users.Where(x => x.ATCName == item.ControllerName &&
                                       x.Department == item.PysicalPosition.ToDepartment()).Single();
                if (user == null)
                    throw Oops.Oh($"管制员 {item.ControllerName} 不存在");
                //get sector
                var sector = sectors.Where(x => x.Code == item.SectorCode &&
                                           x.PositionName.Contains(item.PositionType) &&
                                           x.Department == user.Department).Single();

                if (sector == null)
                    throw Oops.Oh($"扇区 {item.SectorCode}:{item.PositionType} 不存在或不止一个");

                var timeItem = new TimeItemDto 
                {
                    BeginTime = item.BeginTime,
                    EndTime = item.EndTime,
                    UserId = user.Id,
                    SectorId = sector.Id,
                    Confirmed = false,
                    WorkTimeConfId = workConf.Id,
                    UserName = user.ATCName,
                    ControllerRole = item.ControllerRole,
                    CreatedTime = DateTime.Now
                };
                lst.Add(timeItem);
            }

            if (rowItems.Count != lst.Count)
            {
                var q = from a in rowItems
                        join b in lst
                        on a.ControllerName equals b.UserName
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
    }
}
