using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCRemix.Items.Application.Commands;
using OCRemix.Items.Application.Dtos;
using OCRemix.Items.Application.Queries;

namespace OCRemix.Items.Api.Controllers;

[ApiController,
 Route("api/items"),
 Authorize(AuthenticationSchemes = "Api"),
 ApiExplorerSettings(GroupName = "OCRemix"),
 IgnoreAntiforgeryToken]
public class ItemsController : ControllerBase
{
    private readonly IItemQueries _itemQueries;
    private readonly IMediator _mediator;

    public ItemsController(IItemQueries itemQueries, IMediator mediator)
    {
        _itemQueries = itemQueries;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("items")]
    public async Task<ActionResult<IEnumerable<ItemDto>>> ListItems()
    {
        return Ok(await _itemQueries.GetItemsAsync());
    }

    [HttpPost("item")]
    public async Task<ActionResult<string>> AddUser(AddItemCommand command)
    {
        var entityId = await _mediator.Send(command);

        return Ok(entityId);
    }
}
