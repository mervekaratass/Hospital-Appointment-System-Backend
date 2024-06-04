using Application.Features.Notifications.Commands.Create;
using Application.Features.Notifications.Commands.Delete;
using Application.Features.Notifications.Commands.Update;
using Application.Features.Notifications.Queries.GetById;
using Application.Features.Notifications.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedNotificationResponse>> Add([FromBody] CreateNotificationCommand command)
    {
        CreatedNotificationResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedNotificationResponse>> Update([FromBody] UpdateNotificationCommand command)
    {
        UpdatedNotificationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedNotificationResponse>> Delete([FromRoute] int id)
    {
        DeleteNotificationCommand command = new() { Id = id };

        DeletedNotificationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdNotificationResponse>> GetById([FromRoute] int id)
    {
        GetByIdNotificationQuery query = new() { Id = id };

        GetByIdNotificationResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListNotificationQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListNotificationQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListNotificationListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}