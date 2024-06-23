using Application.Features.Feedbacks.Commands.Create;
using Application.Features.Feedbacks.Commands.Delete;
using Application.Features.Feedbacks.Commands.Update;
using Application.Features.Feedbacks.Queries.GetById;
using Application.Features.Feedbacks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Appointments.Queries.GetByPatientId;
using Application.Features.Feedbacks.Queries.GetListByUser;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedbacksController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedFeedbackResponse>> Add([FromBody] CreateFeedbackCommand command)
    {
        CreatedFeedbackResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedFeedbackResponse>> Update([FromBody] UpdateFeedbackCommand command)
    {
        UpdatedFeedbackResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedFeedbackResponse>> Delete([FromRoute] int id)
    {
        DeleteFeedbackCommand command = new() { Id = id };

        DeletedFeedbackResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdFeedbackResponse>> GetById([FromRoute] int id)
    {
        GetByIdFeedbackQuery query = new() { Id = id };

        GetByIdFeedbackResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListFeedbackQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFeedbackQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListFeedbackListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }


    [HttpGet("getByUserId")]
    public async Task<ActionResult<GetListResponse<GetListByUserDto>>> GetListByUserId([FromQuery] PageRequest pageRequest, [FromQuery] Guid userId)
    {
        GetListByUserQuery query = new GetListByUserQuery { PageRequest = pageRequest, UserId = userId };
        GetListResponse<GetListByUserDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}