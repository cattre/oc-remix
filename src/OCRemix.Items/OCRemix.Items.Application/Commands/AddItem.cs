using MediatR;
using OCRemix.Items.Domain.Models;
using YesSql;

namespace OCRemix.Items.Application.Commands;

public record AddItemCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsChecked { get; set; }
}

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, string>
{
    private readonly ISession _session;

    public AddItemCommandHandler(ISession session)
    {
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public Task<string> Handle(AddItemCommand command, CancellationToken cancellationToken)
    {
        var entity = Item.New(
            command.Name,
            command.Description,
            command.Date,
            command.IsChecked
        );

        _session.Save(entity);
        return Task.FromResult(entity.Id);
    }
}
