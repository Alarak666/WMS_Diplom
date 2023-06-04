using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.ApplicationUserControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class ApplicationUserController : ControllerBase
{
    private readonly IDocumentRepository<ApplicationUser> _documentService;
    private readonly IMapper _mapper;

    public ApplicationUserController(IDocumentRepository<ApplicationUser> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _documentService.GetAll(cancellationToken);
        var itemsDto = _mapper.Map<IEnumerable<ApplicationUserDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationUserDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<ApplicationUserDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUserDto>> Create(
        [FromBody] ApplicationUserDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationUser>(itemDto);
        var request = await _documentService.Create(item, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<ApplicationUserDto>> Update(
        [FromBody] ApplicationUserDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationUser>(itemDto);
        await _documentService.Update(item, cancellationToken);
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