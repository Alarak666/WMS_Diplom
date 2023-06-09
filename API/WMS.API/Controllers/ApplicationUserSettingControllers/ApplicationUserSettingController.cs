using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.ApplicationUserSettingControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class ApplicationUserSettingController : ControllerBase
{
    private readonly IDocumentRepository<ApplicationUserSettingDto> _documentService;
    private readonly IMapper _mapper;

    public ApplicationUserSettingController(IDocumentRepository<ApplicationUserSettingDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUserSettingDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _documentService.GetAll(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationUserSettingDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUserSettingDto>> Create(
        [FromBody] ApplicationUserSettingDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<ApplicationUserSettingDto>> Update(
        [FromBody] ApplicationUserSettingDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<ApplicationUserSettingDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken, pageRequestDto.PageNo, pageRequestDto.PageSize);
        return Ok(items);
    }
}