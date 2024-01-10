using IMS.Application.UseCases.Users.Commands;
using IMS.Application.UseCases.Users.Queries;
using IMS.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers;

[Route("api/users"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class UserController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] PaginationParams pagination)
    {
        var result = await mediator.Send(new GetUsersQuery(pagination));
        return new OkObjectResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id));
        return new OkObjectResult(result);
    }

    [HttpGet("roles")]
    public async Task<ActionResult> GetRoles([FromQuery] PaginationParams pagination)
    {
        var result = await mediator.Send(new GetRolesQuery(pagination));
        return new OkObjectResult(result);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateUserCommand command)
    {
        var result = await mediator.Send(command);
        return new OkObjectResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest(new { message = "Id not match" });

        await mediator.Send(command);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpPut("{id:int}/restore")]
    public async Task<ActionResult> Restore(int id)
    {
        await mediator.Send(new RestoreUserCommand(id));
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteUserCommand(id));
        return Ok(new { message = "User marked as inactive" });
    }
}