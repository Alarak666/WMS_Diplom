using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.CurrencyDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Currencies;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.CurrencyServices;

public class CurrencyService : IDocumentRepository<CurrencyDto>
{
    private readonly IDocumentNumeratorService<Currency> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public CurrencyService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Currency> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<CurrencyDto> Create(CurrencyDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Currency>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Currency>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<CurrencyDto>(item);
        return request;
    }
    public async Task<IEnumerable<CurrencyDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<CurrencyDto, bool>>? whereClause = null,
        Expression<Func<CurrencyDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Currency>().AsQueryable();
        var dtos = queryable.ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<CurrencyDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<CurrencyDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Currency>()
            .AsQueryable()
            .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<CurrencyDto> Update(CurrencyDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Currencies
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<CurrencyDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Currency>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Currency>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<CurrencyDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<CurrencyDto, bool>>? whereClause = null,
        Expression<Func<CurrencyDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Currency>().AsQueryable();
        var dtos = queryable.ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}