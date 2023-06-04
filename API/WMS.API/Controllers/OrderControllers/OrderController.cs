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
    private readonly IDocumentRepository<Order> _documentService;
    private readonly IMapper _mapper;

    public OrderController(IDocumentRepository<Order> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null)
    {
        var items = await _documentService.GetAll(cancellationToken,
            orderClause: x => x.CreatedDate.ToString(CultureInfo.CurrentCulture),
            whereClause: string.IsNullOrWhiteSpace(searchText)
                ? null
                : x => x.Name.ToLower().Contains(searchText.ToLower()));
        var itemsDto = _mapper.Map<IEnumerable<OrderDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<OrderDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(
        [FromBody] OrderDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Order>(itemDto);
        var request = await _documentService.Create(item, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<OrderDto>> Update(
        [FromBody] OrderDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Order>(itemDto);
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
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetPage(
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