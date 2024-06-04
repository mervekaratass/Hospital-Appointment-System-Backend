using Application.Features.Patients.Commands.Create;
using Application.Features.Patients.Commands.Delete;
using Application.Features.Patients.Commands.Update;
using Application.Features.Patients.Queries.GetById;
using Application.Features.Patients.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedPatientResponse>> Add([FromBody] CreatePatientCommand command)
    {
        CreatedPatientResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedPatientResponse>> Update([FromBody] UpdatePatientCommand command)
    {
        UpdatedPatientResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedPatientResponse>> Delete([FromRoute] Guid id)
    {
        DeletePatientCommand command = new() { Id = id };

        DeletedPatientResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdPatientResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdPatientQuery query = new() { Id = id };

        GetByIdPatientResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListPatientQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPatientQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListPatientListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}