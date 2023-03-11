using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OCRemix.Users.Application.Dtos;
using OCRemix.Users.Application.Queries;
using OrchardCore.Users;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using YesSql;

namespace OCRemix.Users.Infrastructure.Queries;

public class UserQueries : IUserQueries
{
    private readonly UserManager<IUser> _userManager;
    private readonly IUserRoleStore<IUser> _userRoleStore;
    private readonly ISession _session;

    public UserQueries(
        ISession session,
        IUserRoleStore<IUser> userRoleStore,
        UserManager<IUser> userManager)
    {
        _session = session;
        _userManager = userManager;
        _userRoleStore = userRoleStore;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var query = _session.Query<User, UserIndex>();

            var users = await query.ListAsync();

            return users.Select(user => new UserDto(user));
        }
}
