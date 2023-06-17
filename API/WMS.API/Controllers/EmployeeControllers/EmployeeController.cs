using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WMS.Data.Constant;
using WMS.Data.DTO.EmployeeDtos;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.EmployeeControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class EmployeeController : ControllerBase
{
    private readonly IDocumentRepository<EmployeeDto> _documentService;
    private readonly IMapper _mapper;

    public EmployeeController(IDocumentRepository<EmployeeDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    private Expression<Func<EmployeeDto, bool>> GetWhereClause(string? searchText, string? searchOption)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return null;
        }

        if (searchOption == "F")
        {
            return x => x.FirstName.ToLower().Contains(searchText.ToLower());
        }
        if (searchOption == "M")
        {
            return x => x.MiddleName.ToLower().Contains(searchText.ToLower());
        }
        if (searchOption == "L")
        {
            return x => x.LastName.ToLower().Contains(searchText.ToLower());
        }

        if (searchOption == "P")
        {
            return x => x.PositionName == searchText;
        }
        return null;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null,
        [FromQuery] string? searchOption = null)
    {
        var items = await _documentService.GetAll(cancellationToken,
            orderClause: x => x.FirstName,
            whereClause: GetWhereClause(searchText, searchOption));
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create(
        [FromBody] EmployeeDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<EmployeeDto>> Update(
        [FromBody] EmployeeDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetPage(
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