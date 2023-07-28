using Gama.Application.Contracts.Mappers;
using Gama.Application.Seedworks.Pagination;
using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Application.UseCases.UserAgg.Queries;
using Gama.Application.UseCases.UserAgg.Responses;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.ValueTypes;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gama.Api.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEntityMapper _entityMapper;

    public UserController(
        IUserService userService,
        IEntityMapper entityMapper
        )
    {
        _userService = userService;
        _entityMapper = entityMapper;
    }

    [Authorize]
    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(GetUserResonse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.GetAsync(userId);

        return user.ToOk((user) => _entityMapper.Map<GetUserResonse, User>(user));
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

        var userToCreate = _entityMapper.Map<User, CreateUserCommand>(command);

        userToCreate.AddRole(RolesName.Citizen);

        var user = await _userService.CreateAsync(userToCreate);

        return user.ToOk((user) => _entityMapper.Map<UserCreatedResponse, User>(user));
    }

    [HttpPost("admin")]
    [Authorize(Roles = RolesName.Admin)]
    [ProducesResponseType(typeof(UserCreatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToCreate = _entityMapper.Map<User, CreateUserCommand>(command);

        userToCreate.AddRole(RolesName.Admin);

        var user = await _userService.CreateAsync(userToCreate);

        return user.ToOk((user) => _entityMapper.Map<UserCreatedResponse, User>(user));
    }

    [HttpPost("cop")]
    [Authorize(Roles = RolesName.Admin)]
    [ProducesResponseType(typeof(UserCreatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCop([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToCreate = _entityMapper.Map<User, CreateUserCommand>(command);

        userToCreate.AddRole(RolesName.Cop);

        var user = await _userService.CreateAsync(userToCreate);

        return user.ToOk((user) => _entityMapper.Map<UserCreatedResponse, User>(user));
    }

    [HttpPut("{userId:int}")]
    [Authorize]
    [ProducesResponseType(typeof(GetUserResonse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UpdateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToUpdate = _entityMapper.Map<User, UpdateUserCommand>(command);

        var user = await _userService.UpdateAsync(userId, command);

        return user.ToOk((user) => _entityMapper.Map<GetUserResonse, User>(user));
    }

    [HttpDelete("{userId:int}")]
    [Authorize(Roles = RolesName.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.DeleteAsync(userId);

        return user.ToNoContent();
    }

    [HttpPut("{login}/password")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword(string login, UpdatePasswordCommand updatePasswordCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        updatePasswordCommand.Login = login;

        var result = await _userService.UpdatePasswordAsync(updatePasswordCommand);

        return result.ToNoContent();
    }

    [HttpGet]
    [Authorize(Roles = RolesName.Admin)]
    [ProducesResponseType(typeof(OffsetPageResponse<GetUsersResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers([FromQuery] SearchUserQuery searchUserQuery)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.GetAsync(searchUserQuery);

        return result.ToOk((trafficViolation) => _entityMapper.Map<OffsetPageResponse<GetUsersResponse>, OffsetPage<User>>(trafficViolation));
    }
}