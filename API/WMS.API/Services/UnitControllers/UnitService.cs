using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.UnitDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Units;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.UnitControllers;

public class UnitService : IDocumentRepository<UnitDto>
{
    private readonly IDocumentNumeratorService<Unit> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public UnitService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Unit> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<UnitDto> Create(UnitDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Unit>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Unit>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<UnitDto>(item);
        return request;
    }
    public async Task<IEnumerable<UnitDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<UnitDto, bool>>? whereClause = null,
        Expression<Func<UnitDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Unit>().AsQueryable();
        var dtos = queryable.ProjectTo<UnitDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<UnitDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<UnitDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Unit>()
            .AsQueryable()
            .ProjectTo<UnitDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<UnitDto> Update(UnitDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Units
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<UnitDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Unit>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Unit>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<UnitDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<UnitDto, bool>>? whereClause = null,
        Expression<Func<UnitDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Unit>().AsQueryable();
        var dtos = queryable.ProjectTo<UnitDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}