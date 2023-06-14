using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WMS.API.Services.ApplicationUserServices;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.ApplicationUserControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class ApplicationUserController : ControllerBase
{
    private readonly IDocumentRepository<ApplicationUserDto> _documentService;
    private readonly IApplicationUserService _applicationUserService;

    private readonly IMapper _mapper;

    public ApplicationUserController(IDocumentRepository<ApplicationUserDto> documentService, IMapper mapper, IApplicationUserService applicationUserService)
    {
        _documentService = documentService;
        _mapper = mapper;
        _applicationUserService = applicationUserService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _documentService.GetAll(cancellationToken);
        return Ok(items);
    }
    [HttpGet("login")]
    public async Task<ActionResult<bool>> Login([FromQuery] string Name, [FromQuery] string password, CancellationToken cancellation)
    {
        bool result = await _applicationUserService.Login(Name, password, cancellation);

        if (result)
        {
            return true;
        }
        return false;
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationUserDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUserDto>> Create(
        [FromBody] ApplicationUserDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<ApplicationUserDto>> Update(
        [FromBody] ApplicationUserDto itemDto, CancellationToken cancellationToken)
    {
        await _documentService.Update(itemDto, cancellationToken);
        return Ok(itemDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _documentService.Delete(id, cancellationToken);
        return Ok();
    }

    [HttpPost("Page")]
    public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken, pageRequestDto.PageNo, pageRequestDto.PageSize);
        return Ok(items);
    }
}