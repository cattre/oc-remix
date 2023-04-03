using System.Text.Json.Serialization;

namespace OCRemix.Items.Domain.Models
{
    public class Item
    {
        [JsonConstructor]
        public Item(string id, string name, string description, DateTime date, bool isChecked)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            IsChecked = isChecked;
        }

        public static Item New(string name, string description, DateTime date, bool isChecked)
        {
            var entity = new Item(
                Guid.NewGuid().ToString(),
                name,
                description,
                date,
                isChecked
                );

            return entity;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsChecked { get; private set; }
    }
}
