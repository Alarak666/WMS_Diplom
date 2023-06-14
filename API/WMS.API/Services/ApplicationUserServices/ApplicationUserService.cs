using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.ApplicationUserServices;

public class ApplicationUserService : IDocumentRepository<ApplicationUserDto>, IApplicationUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public ApplicationUserService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<ApplicationUser> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<ApplicationUserDto> Create(ApplicationUserDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationUser>(itemDto);
        _context.Set<ApplicationUser>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<ApplicationUserDto>(item);
        return request;
    }
    public async Task<IEnumerable<ApplicationUserDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<ApplicationUserDto, bool>>? whereClause = null,
        Expression<Func<ApplicationUserDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<ApplicationUser>().AsQueryable();
        var dtos = queryable.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<ApplicationUserDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<ApplicationUserDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<ApplicationUser>()
            .AsQueryable()
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<ApplicationUserDto> Update(ApplicationUserDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.ApplicationUsers
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<ApplicationUserDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ApplicationUser>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
            _context.Set<ApplicationUser>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<ApplicationUserDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<ApplicationUserDto, bool>>? whereClause = null,
        Expression<Func<ApplicationUserDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<ApplicationUser>().AsQueryable();
        var dtos = queryable.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }

    public async Task<bool> Login(string name, string password, CancellationToken cancellation)
    {
       var user = await _context.ApplicationUsers.Where(x => x.UserName == name).FirstOrDefaultAsync(cancellation);

       if (user == null)
       {
           return false;
       }

       var passwordHasher = new PasswordHasher<ApplicationUser>();
       var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

       return passwordVerificationResult == PasswordVerificationResult.Success;
    }
}