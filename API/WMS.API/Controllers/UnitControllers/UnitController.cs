using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.UnitDtos;
using WMS.Data.Entity.Units;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.UnitControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class UnitController : ControllerBase
{
    private readonly IDocumentRepository<UnitDto> _documentService;
    private readonly IMapper _mapper;

    public UnitController(IDocumentRepository<UnitDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null)
    {
        var items = await _documentService.GetAll(cancellationToken,
            orderClause: x => x.Name,
            whereClause: string.IsNullOrWhiteSpace(searchText)
                ? null
                : x => x.Name.ToLower().Contains(searchText.ToLower()));
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UnitDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<UnitDto>> Create(
        [FromBody] UnitDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<UnitDto>> Update(
        [FromBody] UnitDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<UnitDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken,
            pageRequestDto.PageNo,
            pageRequestDto.PageSize,
            orderClause: x => x.Name,
            whereClause: string.IsNullOrWhiteSpace(pageRequestDto.SearchText)
                ? null
                : x => x.Name.ToLower().Contains(pageRequestDto.SearchText.ToLower()));
        return Ok(items);
    }
}