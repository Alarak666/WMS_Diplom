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

namespace WMS.API.Services.ApplicationRoleServices;

public class ApplicationRoleService : IDocumentRepository<ApplicationRoleDto>
{
    private readonly IDocumentNumeratorService<ApplicationRole> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public ApplicationRoleService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<ApplicationRole> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<ApplicationRoleDto> Create(ApplicationRoleDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationRole>(itemDto);
        _context.Set<ApplicationRole>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<ApplicationRoleDto>(item);
        return request;
    }
    public async Task<IEnumerable<ApplicationRoleDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<ApplicationRoleDto, bool>>? whereClause = null,
        Expression<Func<ApplicationRoleDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<ApplicationRole>().AsQueryable();
        var dtos = queryable.ProjectTo<ApplicationRoleDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<ApplicationRoleDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<ApplicationRoleDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<ApplicationRole>()
            .AsQueryable()
            .ProjectTo<ApplicationRoleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<ApplicationRoleDto> Update(ApplicationRoleDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.ApplicationRoles
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<ApplicationRoleDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ApplicationRole>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
            _context.Set<ApplicationRole>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<ApplicationRoleDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<ApplicationRoleDto, bool>>? whereClause = null,
        Expression<Func<ApplicationRoleDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<ApplicationRole>().AsQueryable();
        var dtos = queryable.ProjectTo<ApplicationRoleDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}