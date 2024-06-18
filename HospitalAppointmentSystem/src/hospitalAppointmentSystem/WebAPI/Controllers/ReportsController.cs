using Application.Features.Reports.Commands.Create;
using Application.Features.Reports.Commands.Delete;
using Application.Features.Reports.Commands.Update;
using Application.Features.Reports.Queries.GetById;
using Application.Features.Reports.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Appointments.Queries.GetByPatientId;
using Application.Features.Reports.Queries.GetListByDoctor;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedReportResponse>> Add([FromBody] CreateReportCommand command)
    {
        CreatedReportResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedReportResponse>> Update([FromBody] UpdateReportCommand command)
    {
        UpdatedReportResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedReportResponse>> Delete([FromRoute] int id)
    {
        DeleteReportCommand command = new() { Id = id };

        DeletedReportResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdReportResponse>> GetById([FromRoute] int id)
    {
        GetByIdReportQuery query = new() { Id = id };

        GetByIdReportResponse response = await Mediator.Send(query);

        return Ok(response);
    }


    [HttpGet("getByDoctorId")]
    public async Task<IActionResult> GetListByIdDoctor([FromQuery] PageRequest pageRequest, [FromQuery] Guid doctorId)
    {
        GetListByDoctorQuery query = new() { PageRequest = pageRequest, DoctorId = doctorId };
        GetListResponse<GetListByDoctorDto> response = await Mediator.Send(query);
        return Ok(response);
    }


    [HttpGet]
    public async Task<ActionResult<GetListReportQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReportQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListReportListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}