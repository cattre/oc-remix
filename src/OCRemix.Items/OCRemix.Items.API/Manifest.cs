using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OCRemix.Items.Api",
    Author = "Richard Catterill",
    Version = "0.0.1",
    Description = "OCRemix Items Api",
    Category = "OCRemix",
    Dependencies = new string[]
    {
        "OCRemix.Items.Application"
    }
)]
