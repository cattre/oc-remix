using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCRemix.Users.Application.Dtos;
using OCRemix.Users.Application.Queries;

namespace OCRemix.Users.Api.Controllers;

[ApiController,
 Route("api/users"),
 Authorize(AuthenticationSchemes = "Api"),
 ApiExplorerSettings(GroupName = "OCRemix"),
 IgnoreAntiforgeryToken]
public class UsersController : ControllerBase
{
    private readonly IUserQueries _userQueries;

    public UsersController(IUserQueries userQueries)
    {
        _userQueries = userQueries;
    }

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> ListUsers()
    {
        return Ok(await _userQueries.GetUsersAsync());
    }

    [HttpPost("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> AddUser()
    {
        return Ok(await _userQueries.GetUsersAsync());
    }
}
