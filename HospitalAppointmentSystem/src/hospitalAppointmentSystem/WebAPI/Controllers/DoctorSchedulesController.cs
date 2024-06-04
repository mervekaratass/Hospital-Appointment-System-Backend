using Application.Features.DoctorSchedules.Commands.Create;
using Application.Features.DoctorSchedules.Commands.Delete;
using Application.Features.DoctorSchedules.Commands.Update;
using Application.Features.DoctorSchedules.Queries.GetById;
using Application.Features.DoctorSchedules.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorSchedulesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedDoctorScheduleResponse>> Add([FromBody] CreateDoctorScheduleCommand command)
    {
        CreatedDoctorScheduleResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedDoctorScheduleResponse>> Update([FromBody] UpdateDoctorScheduleCommand command)
    {
        UpdatedDoctorScheduleResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedDoctorScheduleResponse>> Delete([FromRoute] int id)
    {
        DeleteDoctorScheduleCommand command = new() { Id = id };

        DeletedDoctorScheduleResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdDoctorScheduleResponse>> GetById([FromRoute] int id)
    {
        GetByIdDoctorScheduleQuery query = new() { Id = id };

        GetByIdDoctorScheduleResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListDoctorScheduleQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDoctorScheduleQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListDoctorScheduleListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}