using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.PersonDtos;
using WMS.Data.Entity.Persons;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.PersonControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class PersonController : ControllerBase
{
    private readonly IDocumentRepository<PersonDto> _documentService;
    private readonly IMapper _mapper;

    public PersonController(IDocumentRepository<PersonDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null)
    {
        var items = await _documentService.GetAll(cancellationToken,
            orderClause: x => x.CreatedDate.ToString(CultureInfo.CurrentCulture),
            whereClause: string.IsNullOrWhiteSpace(searchText)
                ? null
                : x => x.Name.ToLower().Contains(searchText.ToLower()));
        var itemsDto = _mapper.Map<IEnumerable<PersonDto>>(items);
        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        var itemDto = _mapper.Map<PersonDto>(item);
        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> Create(
        [FromBody] PersonDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<PersonDto>> Update(
        [FromBody] PersonDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken,
            pageRequestDto.PageNo,
            pageRequestDto.PageSize,
            orderClause: x => x.CreatedDate.ToString(CultureInfo.InvariantCulture),
            whereClause: string.IsNullOrWhiteSpace(pageRequestDto.SearchText)
                ? null
                : x => x.Name.ToLower().Contains(pageRequestDto.SearchText.ToLower()));
        return Ok(items);
    }
}