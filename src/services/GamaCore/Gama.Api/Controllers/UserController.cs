using Gama.Application.Contracts.Repositories;
using Gama.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("/")]
    [AllowAnonymous]
    public async Task<IActionResult> Create()
    {
        var user = new Cop(
            "Dedao",
            "Jodao",
            "dedaocop",
            "dedaozinho_cop@gmail.com",
            "deds",
            "21233"
        );

        await _userRepository.InsertAsync(user);
        await _userRepository.CommitAsync();

        return Ok(user);
    }
}