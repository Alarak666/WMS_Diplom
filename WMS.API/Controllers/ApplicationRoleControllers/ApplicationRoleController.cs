using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.ApplicationRoleControllers;

[ApiController]
[Route("api/[controller]")] 
//[ApiVersion(CoreDefaultValues.Version)]
public class PalletController : ControllerBase
{
    private readonly IDocumentRepository<ApplicationRole> _documentService;
    private readonly IMapper _mapper;

    public PalletController(IDocumentRepository<ApplicationRole> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationRoleDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _documentService.GetAll(cancellationToken);
        var itemsDto = _mapper.Map<IEnumerable<ApplicationRoleDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationRoleDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<ApplicationRoleDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationRoleDto>> Create(
        [FromBody] ApplicationRoleDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationRole>(itemDto);
        var request = await _documentService.Create(item, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<ApplicationRoleDto>> Update(
        [FromBody] ApplicationRoleDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationRole>(itemDto);
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
    public async Task<ActionResult<IEnumerable<ApplicationRoleDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken,
            pageRequestDto.PageNo,
            pageRequestDto.PageSize);
        return Ok(items);
    }
}