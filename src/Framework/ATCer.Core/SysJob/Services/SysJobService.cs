using ATCer.SysJob.Domains;
using ATCer.SysJob.Dtos;
using Furion.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace ATCer.SysJob.Services;

/// <summary>
/// 系统作业任务服务
/// </summary>
[ApiDescriptionSettings(Order = 188)]
public class SysJobService : ServiceBase<SysJobDetail, SysJobDetailDto,int>, ITransient
{
    private readonly IRepository<SysJobDetail> _sysJobDetailRep;
    private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
    private readonly IRepository<SysJobCluster> _sysJobClusterRep;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IDynamicFilterService _dynamicFilterService;
    /// <summary>
    /// init
    /// </summary>
    /// <param name="sysJobDetailRep"></param>
    /// <param name="sysJobTriggerRep"></param>
    /// <param name="sysJobClusterRep"></param>
    /// <param name="schedulerFactory"></param>
    public SysJobService(IRepository<SysJobDetail> sysJobDetailRep,
        IRepository<SysJobTrigger> sysJobTriggerRep,
        IRepository<SysJobCluster> sysJobClusterRep,
        ISchedulerFactory schedulerFactory):base(sysJobDetailRep)
    {
        _sysJobDetailRep = sysJobDetailRep;
        _sysJobTriggerRep = sysJobTriggerRep;
        _sysJobClusterRep = sysJobClusterRep;
        _schedulerFactory = schedulerFactory;
    }

    /// <summary>
    /// 假删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override Task<bool> FakeDelete(int id)
    {
        return base.FakeDelete(id);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override async Task<MyPagedList<SysJobDetailDto>> Search(MyPageRequest request)
    {
        var jobDetails = await base.Search(request);

        var expression = _dynamicFilterService.GetExpression<SysJobDetail>(request.FilterGroups);

        if (jobDetails != null) 
        {
            foreach (var jobDetail in jobDetails.Items)
            {
                var triggers = await _sysJobTriggerRep.Where(x => x.JobId == jobDetail.JobId).ProjectToType<SysJobTriggerDto>().ToListAsync();
                jobDetail.JobTriggers = triggers;
            }
        }
        return jobDetails;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <returns></returns>
    public override async Task<SysJobDetailDto> Insert(SysJobDetailDto input)
    {
        var isExist = await _sysJobDetailRep.Where(u => u.JobId == input.JobId && u.Id != input.Id).AnyAsync();
        if (isExist)
            throw Oops.Oh(ExceptionCode.INVALID_BUSSINESS_TYPE);

        // 动态创建作业
        NatashaInitializer.Preheating();
        var oop = new AssemblyCSharpBuilder("ATCer.Core");
        oop.Domain = DomainManagement.Random();
        oop.Add(input.ScriptCode);
        var jobType = oop.GetTypeFromShortName(input.JobId);
        _schedulerFactory.AddJob(JobBuilder.Create(jobType).SetIncludeAnnotations(input.IncludeAnnotations));

        await _sysJobDetailRep.InsertAsync(input.Adapt<SysJobDetail>());

        return await base.Insert(input);
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<bool> Update(SysJobDetailDto input)
    {
        var isExist = await _sysJobDetailRep.Where(u => u.JobId == input.JobId && u.Id != input.Id).AnyAsync();
        if (isExist)
            throw Oops.Oh(ExceptionCode.INVALID_BUSSINESS_TYPE);

        return await base.Update(input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<bool> Delete(int id)
    {
        var jobDetail = await base.Get(id);
        if (jobDetail == null)
            return false;

        _schedulerFactory.RemoveJob(jobDetail.JobId);

        await _sysJobDetailRep.Where(u => u.JobId == jobDetail.JobId).ExecuteDeleteAsync();
        await _sysJobTriggerRep.Where(u => u.JobId == jobDetail.JobId).ExecuteDeleteAsync();

        return await base.Delete(id);
    }


    /// <summary>
    /// 获取触发器列表
    /// </summary>
    [HttpGet("/sysJob/triggerList")]
    public async Task<List<SysJobTrigger>> GetJobTriggerList([FromQuery] JobDetailInput input)
    {
        return await _sysJobTriggerRep.AsQueryable()
            .Where(!string.IsNullOrWhiteSpace(input.JobId), u => u.JobId.Contains(input.JobId))
            .ToListAsync();
    }

    /// <summary>
    /// 添加触发器
    /// </summary>
    /// <returns></returns>
    [HttpPost("/sysJob/triggerAdd")]
    public async Task AddJobTrigger(AddJobTriggerInput input)
    {
        var isExist = await _sysJobTriggerRep.Where(u => u.TriggerId == input.TriggerId && u.Id != input.Id).AnyAsync();
        if (isExist)
            throw Oops.Oh(ExceptionCode.INVALID_BUSSINESS_TYPE);

        var jobTrigger = input.Adapt<SysJobTrigger>();
        jobTrigger.Args = "[" + jobTrigger.Args + "]";
        await _sysJobTriggerRep.InsertAsync(jobTrigger);
    }

    /// <summary>
    /// 更新触发器
    /// </summary>
    /// <returns></returns>
    [HttpPost("/sysJob/triggerUpdate")]
    public async Task UpdateJobTrigger(UpdateJobTriggerInput input)
    {
        var isExist = await _sysJobTriggerRep.Where(u => u.TriggerId == input.TriggerId && u.Id != input.Id).AnyAsync();
        if (isExist)
            throw Oops.Oh(ExceptionCode.INVALID_BUSSINESS_TYPE);

        var jobTrigger = input.Adapt<SysJobTrigger>();
        jobTrigger.Args = "[" + jobTrigger.Args + "]";
        await _sysJobTriggerRep.UpdateAsync(jobTrigger);
    }

    /// <summary>
    /// 删除触发器
    /// </summary>
    /// <returns></returns>
    [HttpPost("/sysJob/triggerDelete")]
    public async Task DeleteJobTrigger(DeleteJobTriggerInput input)
    {
        await _sysJobTriggerRep.Where(u => u.TriggerId == input.TriggerId).ExecuteDeleteAsync();
    }

    /// <summary>
    /// 暂停所有作业
    /// </summary>
    /// <returns></returns>
    [HttpPost("/sysJob/pauseAll")]
    public void PauseAllJob()
    {
        _schedulerFactory.PauseAll();
    }

    /// <summary>
    /// 启动所有作业
    /// </summary>
    /// <returns></returns>
    [HttpPost("/sysJob/startAll")]
    public void StartAllJob()
    {
        _schedulerFactory.StartAll();
    }

    /// <summary>
    /// 暂停作业
    /// </summary>
    [HttpPost("/sysJob/pauseJob")]
    public void PauseJob(JobDetailInput input)
    {
        _ = _schedulerFactory.TryGetJob(input.JobId, out var _scheduler);
        _scheduler?.Pause();
    }

    /// <summary>
    /// 启动作业
    /// </summary>
    [HttpPost("/sysJob/startJob")]
    public void StartJob(JobDetailInput input)
    {
        _ = _schedulerFactory.TryGetJob(input.JobId, out var _scheduler);
        _scheduler?.Start();
    }

    /// <summary>
    /// 暂停触发器
    /// </summary>
    [HttpPost("/sysJob/pauseTrigger")]
    public void PauseTrigger(JobTriggerInput input)
    {
        _ = _schedulerFactory.TryGetJob(input.JobId, out var _scheduler);
        _scheduler?.PauseTrigger(input.TriggerId);
    }

    /// <summary>
    /// 启动触发器
    /// </summary>
    [HttpPost("/sysJob/startTrigger")]
    public void StartTrigger(JobTriggerInput input)
    {
        _ = _schedulerFactory.TryGetJob(input.JobId, out var _scheduler);
        _scheduler?.StartTrigger(input.TriggerId);
    }

    /// <summary>
    /// 强制唤醒作业调度器
    /// </summary>
    [HttpPost("/sysJob/cancelSleep")]
    public void CancelSleep()
    {
        _schedulerFactory.CancelSleep();
    }

    /// <summary>
    /// 强制触发所有作业持久化
    /// </summary>
    [HttpPost("/sysJob/persistAll")]
    public void PersistAll()
    {
        _schedulerFactory.PersistAll();
    }

    /// <summary>
    /// 获取集群列表
    /// </summary>
    [HttpGet("/sysJob/clusterList")]
    public async Task<List<SysJobCluster>> GetJobClusterList()
    {
        return await _sysJobClusterRep.AsQueryable(false).ToListAsync();
    }
}