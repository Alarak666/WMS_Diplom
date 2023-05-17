using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.CountryDtos;
using WMS.Data.Entity.Countries;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.CountryControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class PalletController : ControllerBase
{
    private readonly IDocumentRepository<Country> _documentService;
    private readonly IMapper _mapper;

    public PalletController(IDocumentRepository<Country> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null)
    {
        var items = await _documentService.GetAll(cancellationToken,
            orderClause: x => x.CreatedDate.ToString(CultureInfo.CurrentCulture),
            whereClause: string.IsNullOrWhiteSpace(searchText)
                ? null
                : x => x.Name.ToLower().Contains(searchText.ToLower()));
        var itemsDto = _mapper.Map<IEnumerable<CountryDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CountryDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<CountryDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<CountryDto>> Create(
        [FromBody] CountryDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Country>(itemDto);
        var request = await _documentService.Create(item, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<CountryDto>> Update(
        [FromBody] CountryDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Country>(itemDto);
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
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetPage(
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