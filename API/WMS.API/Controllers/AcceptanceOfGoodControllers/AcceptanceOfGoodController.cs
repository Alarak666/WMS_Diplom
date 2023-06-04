using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.Stocks;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.AcceptanceOfGoodControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]
public class AcceptanceOfGoodController : ControllerBase
{
    private readonly IDocumentRepository<AcceptanceOfGood> _documentService;
    private readonly IMapper _mapper;

    public AcceptanceOfGoodController(IDocumentRepository<AcceptanceOfGood> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AcceptanceOfGoodDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null)
    {
        var items = await _documentService.GetAll(cancellationToken,
            orderClause: x => x.CreatedDate.ToString(CultureInfo.CurrentCulture),
            whereClause: string.IsNullOrWhiteSpace(searchText)
                ? null
                : x => x.Name.ToLower().Contains(searchText.ToLower()));
        var itemsDto = _mapper.Map<IEnumerable<AcceptanceOfGoodDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AcceptanceOfGoodDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<AcceptanceOfGoodDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<AcceptanceOfGoodDto>> Create(
        [FromBody] AcceptanceOfGoodDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<AcceptanceOfGood>(itemDto);
        var request = await _documentService.Create(item, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<AcceptanceOfGoodDto>> Update(
        [FromBody] AcceptanceOfGoodDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<AcceptanceOfGood>(itemDto);
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
    public async Task<ActionResult<IEnumerable<AcceptanceOfGoodDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken,
            pageRequestDto.PageNo,
            pageRequestDto.PageSize,
            orderClause: x => x.CreatedDate.ToString(CultureInfo.CurrentCulture),
            whereClause: string.IsNullOrWhiteSpace(pageRequestDto.SearchText)
                ? null
                : x => x.Name.ToLower().Contains(pageRequestDto.SearchText.ToLower()));
        return Ok(items);
    }
}