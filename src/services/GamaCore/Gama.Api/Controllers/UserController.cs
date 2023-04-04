using System.Net;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(UserCreatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        var user = await _userService.GetAsync(userId);

        return user.ToOk(m => new UserCreatedResponse(m));
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserCreatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        var user = await _userService.CreateAsync(command);

        return user.ToOk(m => new UserCreatedResponse(m));
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserCreatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UpdateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        var user = await _userService.UpdateAsync(userId, command);

        return user.ToOk(m => new UserUpdatedResponse(m));
    }
    
    [Authorize]
    [HttpDelete("{userId:int}")]
    [ProducesResponseType(typeof(UserCreatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromQuery] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        var user = await _userService.DeleteAsync(userId);

        return user.ToNoContent();
    }
}