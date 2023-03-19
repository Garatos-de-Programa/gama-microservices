using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserAuthenticationService _userAuthenticationService;

    public AuthController(IUserAuthenticationService userAuthenticationService)
    {
        _userAuthenticationService = userAuthenticationService;
    }

    [HttpPost("/token")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] AuthenticateCommand authenticateCommand)
    {
        var validationResult = authenticateCommand.IsValid();

        if (validationResult.IsFaulted)
        {
            return validationResult.ToBadRequest();
        }

        var token = await _userAuthenticationService.Authenticate(authenticateCommand);

        return token.ToOk();
    }
}