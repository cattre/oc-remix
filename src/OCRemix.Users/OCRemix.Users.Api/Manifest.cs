using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OCRemix.Users.Api",
    Author = "Richard Catterill",
    Version = "0.0.1",
    Description = "OCRemix Users Api",
    Category = "OCRemix",
    Dependencies = new string[] {
        "OCRemix.Api",
        "OrchardCore.OpenId.Server"
    })]
