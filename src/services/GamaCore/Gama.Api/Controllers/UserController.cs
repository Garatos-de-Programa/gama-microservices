using System.Net;
using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Domain.Entities;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
}