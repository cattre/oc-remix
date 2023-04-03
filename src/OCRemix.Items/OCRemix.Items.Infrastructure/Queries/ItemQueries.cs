using OCRemix.Items.Application.Dtos;
using OCRemix.Items.Application.Queries;
using OCRemix.Items.Domain.Models;
using YesSql;

namespace OCRemix.Items.Infrastructure.Queries;

public class ItemQueries : IItemQueries
{
    private readonly ISession _session;

    public ItemQueries(ISession session)
    {
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = await _session.Query<Item>().ListAsync();

        return items.Select(item => new ItemDto(item));
    }

}
