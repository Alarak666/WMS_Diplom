﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WMS.Data.Constant;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Helpers;
using WMS.Data.Interface;

namespace WMS.API.Controllers.UserActivityControllers;

[ApiController]
[Route("api/[controller]")]
//[ApiVersion(CoreDefaultValues.Version)]

public class UserActivityController : ControllerBase
{
    private readonly IDocumentRepository<UserActivityDto> _documentService;
    private readonly IMapper _mapper;

    public UserActivityController(IDocumentRepository<UserActivityDto> documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserActivityDto>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] string? searchText = null)
    {
        var items = await _documentService.GetAll(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserActivityDto>> GetById(Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _documentService.Get(id, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<UserActivityDto>> Create(
        [FromBody] UserActivityDto itemDto, CancellationToken cancellationToken)
    {
        var request = await _documentService.Create(itemDto, cancellationToken);
        return Ok(request);
    }

    [HttpPut]
    public async Task<ActionResult<UserActivityDto>> Update(
        [FromBody] UserActivityDto itemDto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<UserActivityDto>>> GetPage(
        [FromBody] BasePagingRequestDto pageRequestDto, CancellationToken cancellationToken)
    {
        var items = await _documentService.GetPage(cancellationToken,
            pageRequestDto.PageNo,
            pageRequestDto.PageSize);
        return Ok(items);
    }
}