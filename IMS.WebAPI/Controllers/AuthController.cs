using IMS.Application.UseCases.Auth.Commands;
using IMS.Application.UseCases.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers;

[Route("api/auth")]
public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPost("renew_access_token")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RenewToken(RenewTokenDto dto)
    {
        // Get username from the token
        var username = User.Identity?.Name;
        // If the username is null, the user is not authenticated
        if (username == null) return Unauthorized();

        var command = new RenewTokenCommand(username, dto.RefreshToken);

        return Ok(new { accessToken = await mediator.Send(command) });
    }
}