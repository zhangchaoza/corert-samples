using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddBranch<AddSettings>("add", add =>
    {
        add.AddCommand<AddPackageCommand>("package");
        add.AddCommand<AddReferenceCommand>("reference");
    });

    config.SetExceptionHandler((ex, tr) =>
    {
        AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        return -99;
    });
});

return app.Run(args);


public class AddSettings : CommandSettings
{
    [CommandArgument(0, "[PROJECT]")]
    public string? Project { get; set; }
}

public class AddPackageSettings : AddSettings
{
    [CommandArgument(0, "<PACKAGE_NAME>")]
    public string? PackageName { get; set; }

    [CommandOption("-v|--version <VERSION>")]
    public string? Version { get; set; }
}

public class AddReferenceSettings : AddSettings
{
    [CommandArgument(0, "<PROJECT_REFERENCE>")]
    public string? ProjectReference { get; set; }
}

public class AddPackageCommand : Command<AddPackageSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] AddPackageSettings settings)
    {
        // Omitted
        return 0;
    }
}

public class AddReferenceCommand : Command<AddReferenceSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] AddReferenceSettings settings)
    {
        // Omitted
        return 0;
    }
}
