using Application.Features.Managers.Commands.Create;
using Application.Features.Managers.Commands.Delete;
using Application.Features.Managers.Commands.Update;
using Application.Features.Managers.Queries.GetById;
using Application.Features.Managers.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagersController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedManagerResponse>> Add([FromBody] CreateManagerCommand command)
    {
        CreatedManagerResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedManagerResponse>> Update([FromBody] UpdateManagerCommand command)
    {
        UpdatedManagerResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedManagerResponse>> Delete([FromRoute] Guid id)
    {
        DeleteManagerCommand command = new() { Id = id };

        DeletedManagerResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdManagerResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdManagerQuery query = new() { Id = id };

        GetByIdManagerResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListManagerQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListManagerQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListManagerListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}