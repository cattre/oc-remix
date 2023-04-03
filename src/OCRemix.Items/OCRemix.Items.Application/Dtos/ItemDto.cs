using System.ComponentModel.DataAnnotations;
using OCRemix.Items.Domain.Models;

namespace OCRemix.Items.Application.Dtos;

public record ItemDto
{
    public ItemDto(Item item)
    {
        Id = item.Id;
        Name = item.Name;
        Description = item.Description;
        Date = item.Date;
        IsChecked = item.IsChecked;
    }

    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public bool IsChecked { get; set; }
}
