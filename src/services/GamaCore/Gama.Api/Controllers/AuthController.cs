using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    
    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("/token")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] TokenCreationCommand tokenCreationCommand)
    {
        var validationResult = tokenCreationCommand.IsValid();
        
        if (validationResult.IsFaulted)
        {
            return validationResult.ToBadRequest();
        }

        var token = await _tokenService.Generate(tokenCreationCommand);

        return Ok(token);
    }
}