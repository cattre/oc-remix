using System.Collections.Generic;
using System.Threading.Tasks;
using OCRemix.Users.Application.Dtos;

namespace OCRemix.Users.Application.Queries;

public interface IUserQueries
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
}
