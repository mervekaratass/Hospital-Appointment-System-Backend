using Application.Features.Branches.Commands.Create;
using Application.Features.Branches.Commands.Delete;
using Application.Features.Branches.Commands.Update;
using Application.Features.Branches.Queries.GetById;
using Application.Features.Branches.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Branches.Queries.GetByName;
using Nest;
using Application.Features.Doctors.Queries.GetList;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedBranchResponse>> Add([FromBody] CreateBranchCommand command)
    {
        GetByNameBranchWithoutControlQuery getByNameQuery = new() { Name = command.Name };

        GetByNameBranchResponse getByNameResponse = await Mediator.Send(getByNameQuery);

        if (getByNameResponse != null && getByNameResponse.Name == command.Name)
        {
            return BadRequest("Bu isimde branþ zaten mevcut");
        }


        CreatedBranchResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
        
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedBranchResponse>> Update([FromBody] UpdateBranchCommand command)
    {
        GetByNameBranchWithoutControlQuery getByNameQuery = new() { Name = command.Name };

        GetByNameBranchResponse getByNameResponse = await Mediator.Send(getByNameQuery);

        if (getByNameResponse != null && getByNameResponse.Name == command.Name)
        {
            return BadRequest("Bu isimde branþ zaten mevcut");
        }

        UpdatedBranchResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedBranchResponse>> Delete([FromRoute] int id , [FromQuery] PageRequest  pageRequest)
    {
        GetListDoctorQuery DoctorIdQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListDoctorListItemDto> DoctorIdResponse = await Mediator.Send(DoctorIdQuery);

        foreach (var doctor in DoctorIdResponse.Items)
        {

            if (doctor.BranchID == id)
            {
                return BadRequest("Bu branþý silemezsiniz");
            }
        }



        DeleteBranchCommand command = new() { Id = id };

        DeletedBranchResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdBranchResponse>> GetById([FromRoute] int id)
    {
        GetByIdBranchQuery query = new() { Id = id };

        GetByIdBranchResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListBranchQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBranchQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListBranchListItemDto> response = await Mediator.Send(query);


        return Ok(response);
    }


    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<GetByNameBranchResponse>> GetByName([FromRoute] string name)
    {
        GetByNameBranchQuery query = new() { Name = name };

        GetByNameBranchResponse response = await Mediator.Send(query);

        return Ok(response);
    }
}