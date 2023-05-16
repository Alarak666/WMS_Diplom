﻿using AutoMapper;
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

namespace WMS.API.Services.AcceptanceOfGoodServices;

public class AcceptanceOfGoodService : IDocumentRepository<AcceptanceOfGoodDto>
{
    private readonly IDocumentNumeratorService<AcceptanceOfGood> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public AcceptanceOfGoodService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<AcceptanceOfGood> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<AcceptanceOfGoodDto> Create(AcceptanceOfGoodDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<AcceptanceOfGood>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<AcceptanceOfGood>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<AcceptanceOfGoodDto>(item);
        return request;
    }
    public async Task<IEnumerable<AcceptanceOfGoodDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<AcceptanceOfGoodDto, bool>>? whereClause = null,
        Expression<Func<AcceptanceOfGoodDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<AcceptanceOfGood>().AsQueryable();
        var dtos = queryable.ProjectTo<AcceptanceOfGoodDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<AcceptanceOfGoodDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<AcceptanceOfGoodDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<AcceptanceOfGood>()
            .AsQueryable()
            .ProjectTo<AcceptanceOfGoodDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<AcceptanceOfGoodDto> Update(AcceptanceOfGoodDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.AcceptanceOfGoods
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<AcceptanceOfGoodDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<AcceptanceOfGood>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<AcceptanceOfGood>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<AcceptanceOfGoodDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<AcceptanceOfGoodDto, bool>>? whereClause = null,
        Expression<Func<AcceptanceOfGoodDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<AcceptanceOfGood>().AsQueryable();
        var dtos = queryable.ProjectTo<AcceptanceOfGoodDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}