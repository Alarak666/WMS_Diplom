using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.PositionDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Positions;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.PositionControllers;

public class PositionService : IDocumentRepository<PositionDto>
{
    private readonly IDocumentNumeratorService<Position> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public PositionService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Position> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<PositionDto> Create(PositionDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Position>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Position>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<PositionDto>(item);
        return request;
    }
    public async Task<IEnumerable<PositionDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<PositionDto, bool>>? whereClause = null,
        Expression<Func<PositionDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Position>().Include(x=> x.Division).AsQueryable();
        var dtos = queryable.ProjectTo<PositionDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<PositionDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<PositionDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Position>()
            .AsQueryable()
            .ProjectTo<PositionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<PositionDto> Update(PositionDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Positions
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<PositionDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Position>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Position>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<PositionDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<PositionDto, bool>>? whereClause = null,
        Expression<Func<PositionDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Position>().AsQueryable();
        var dtos = queryable.ProjectTo<PositionDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}