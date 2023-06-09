using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WMS.Data.Constant;
using WMS.Data.DTO.StockDtos;
using WMS.Data.DTO.VendorCustomerDtos;
using WMS.Data.Entity.Stocks;
using WMS.Data.Entity.VendorCustomers;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.VendorCustomerControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class VendorCustomerController : ControllerBase
{
    private readonly IDocumentRepository<VendorCustomerDto> _documentService;
    private readonly IMapper _mapper;

    public VendorCustomerController(IDocumentRepository<VendorCustomerDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendorCustomerDto>>> GetAll(
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
    public async Task<ActionResult<VendorCustomerDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<VendorCustomerDto>> Create(
        [FromBody] VendorCustomerDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<VendorCustomerDto>> Update(
        [FromBody] VendorCustomerDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<VendorCustomerDto>>> GetPage(
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