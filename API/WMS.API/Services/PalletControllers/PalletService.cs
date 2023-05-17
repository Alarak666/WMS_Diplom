using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Stocks;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.PalletControllers;

public class PalletService : IDocumentRepository<PalletDto>
{
    private readonly IDocumentNumeratorService<Pallet> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public PalletService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Pallet> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<PalletDto> Create(PalletDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Pallet>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Pallet>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<PalletDto>(item);
        return request;
    }
    public async Task<IEnumerable<PalletDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<PalletDto, bool>>? whereClause = null,
        Expression<Func<PalletDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Pallet>().AsQueryable();
        var dtos = queryable.ProjectTo<PalletDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<PalletDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<PalletDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Pallet>()
            .AsQueryable()
            .ProjectTo<PalletDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<PalletDto> Update(PalletDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Pallets
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<PalletDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Pallet>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Pallet>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<PalletDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<PalletDto, bool>>? whereClause = null,
        Expression<Func<PalletDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Pallet>().AsQueryable();
        var dtos = queryable.ProjectTo<PalletDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}