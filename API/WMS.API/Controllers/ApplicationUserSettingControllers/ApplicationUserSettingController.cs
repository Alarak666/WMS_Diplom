using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.ApplicationUserSettingControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class PalletController : ControllerBase
{
    private readonly IDocumentRepository<ApplicationUserSetting> _documentService;
    private readonly IMapper _mapper;

    public PalletController(IDocumentRepository<ApplicationUserSetting> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUserSettingDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _documentService.GetAll(cancellationToken);
        var itemsDto = _mapper.Map<IEnumerable<ApplicationUserSettingDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationUserSettingDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<ApplicationUserSettingDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUserSettingDto>> Create(
        [FromBody] ApplicationUserSettingDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationUserSetting>(itemDto);
        var request = await _documentService.Create(item, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<ApplicationUserSettingDto>> Update(
        [FromBody] ApplicationUserSettingDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ApplicationUserSetting>(itemDto);
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
    public async Task<ActionResult<IEnumerable<ApplicationUserSettingDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken,pageRequestDto.PageNo, pageRequestDto.PageSize);
        return Ok(items);
    }
}