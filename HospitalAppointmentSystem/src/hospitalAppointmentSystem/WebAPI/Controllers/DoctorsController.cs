using Application.Features.Doctors.Commands.Create;
using Application.Features.Doctors.Commands.Delete;
using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries.GetById;
using Application.Features.Doctors.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedDoctorResponse>> Add([FromBody] CreateDoctorCommand command)
    {
        CreatedDoctorResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedDoctorResponse>> Update([FromBody] UpdateDoctorCommand command)
    {
        UpdatedDoctorResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedDoctorResponse>> Delete([FromRoute] Guid id)
    {
        DeleteDoctorCommand command = new() { Id = id };

        DeletedDoctorResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdDoctorResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdDoctorQuery query = new() { Id = id };

        GetByIdDoctorResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListDoctorQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDoctorQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListDoctorListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}