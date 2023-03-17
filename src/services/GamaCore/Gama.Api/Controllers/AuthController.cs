using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private ITokenService _tokenService;
    
    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("/token")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] TokenCreationCommand tokenCreationCommand)
    {
        if (!tokenCreationCommand.IsValid())
        {
            return BadRequest();
        }

        var token = await _tokenService.Generate(tokenCreationCommand);

        return Ok(token);
    }
}