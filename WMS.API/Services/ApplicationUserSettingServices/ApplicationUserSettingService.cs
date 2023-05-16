using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.ApplicationUserSettingServices;

public class ApplicationUserSettingService : IDocumentRepository<ApplicationUserSettingDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public ApplicationUserSettingService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<ApplicationUserSetting> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<ApplicationUserSettingDto> Create(ApplicationUserSettingDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationUserSetting>(itemDto);
        _context.Set<ApplicationUserSetting>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<ApplicationUserSettingDto>(item);
        return request;
    }
    public async Task<IEnumerable<ApplicationUserSettingDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<ApplicationUserSettingDto, bool>>? whereClause = null,
        Expression<Func<ApplicationUserSettingDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<ApplicationUserSetting>().AsQueryable();
        var dtos = queryable.ProjectTo<ApplicationUserSettingDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<ApplicationUserSettingDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<ApplicationUserSettingDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<ApplicationUserSetting>()
            .AsQueryable()
            .ProjectTo<ApplicationUserSettingDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<ApplicationUserSettingDto> Update(ApplicationUserSettingDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.ApplicationUserSettings
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<ApplicationUserSettingDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ApplicationUserSetting>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
            _context.Set<ApplicationUserSetting>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<ApplicationUserSettingDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<ApplicationUserSettingDto, bool>>? whereClause = null,
        Expression<Func<ApplicationUserSettingDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<ApplicationUserSetting>().AsQueryable();
        var dtos = queryable.ProjectTo<ApplicationUserSettingDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}