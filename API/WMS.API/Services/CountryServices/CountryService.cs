using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.CountryDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Countries;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.CountryServices;

public class CountryService : IDocumentRepository<CountryDto>
{
    private readonly IDocumentNumeratorService<Country> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public CountryService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Country> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<CountryDto> Create(CountryDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Country>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Country>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<CountryDto>(item);
        return request;
    }
    public async Task<IEnumerable<CountryDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<CountryDto, bool>>? whereClause = null,
        Expression<Func<CountryDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Country>().AsQueryable();
        var dtos = queryable.ProjectTo<CountryDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<CountryDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<CountryDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Country>()
            .AsQueryable()
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<CountryDto> Update(CountryDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Countries
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<CountryDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Country>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Country>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<CountryDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<CountryDto, bool>>? whereClause = null,
        Expression<Func<CountryDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Country>().AsQueryable();
        var dtos = queryable.ProjectTo<CountryDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}