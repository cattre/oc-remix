using OCRemix.Items.Application.Dtos;

namespace OCRemix.Items.Application.Queries;

public interface IItemQueries
{
    Task<IEnumerable<ItemDto>> GetItemsAsync();
}
