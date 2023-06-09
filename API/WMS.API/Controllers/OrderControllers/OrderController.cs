using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.Entity.Orders;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.OrderControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class OrderController : ControllerBase
{
    private readonly IDocumentRepository<OrderDto> _documentService;
    private readonly IMapper _mapper;

    public OrderController(IDocumentRepository<OrderDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll(
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
    public async Task<ActionResult<OrderDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(
        [FromBody] OrderDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<OrderDto>> Update(
        [FromBody] OrderDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetPage(
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