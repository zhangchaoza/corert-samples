using Spectre.Console;

AnsiConsole.Write(new Markup("[bold yellow]Hello[/] [red]World![/]"));
AnsiConsole.WriteLine();

var table = new Table();
table.AddColumn(new TableColumn(new Markup("[yellow]Foo[/]")));
table.AddColumn(new TableColumn("[blue]Bar[/]"));
AnsiConsole.Write(table);
AnsiConsole.WriteLine();

// Convenience methods
AnsiConsole.Markup("[underline green]Hello[/] ");
AnsiConsole.MarkupLine("[bold]World[/]");

// Escaping format characters
AnsiConsole.Markup("[[Hello]] "); // [Hello]
AnsiConsole.Markup("[red][[World]][/]"); // [World]
AnsiConsole.WriteLine();

// You can also use the EscapeMarkup extension method.
AnsiConsole.Markup("[red]{0}[/]", "Hello [World]".EscapeMarkup());
AnsiConsole.WriteLine();

// You can also use the Markup.Escape method.
AnsiConsole.Markup("[red]{0}[/]", Markup.Escape("Hello [World]"));
AnsiConsole.WriteLine();


// Setting background color
AnsiConsole.Markup("[bold yellow on blue]Hello[/]");
AnsiConsole.Markup("[default on blue]World[/]");
AnsiConsole.WriteLine();

// Rendering emojis
AnsiConsole.Markup("Hello :globe_showing_europe_africa:!");
AnsiConsole.WriteLine();

// Colors
AnsiConsole.Markup("[red]Foo[/] ");
AnsiConsole.Markup("[#ff0000]Bar[/] ");
AnsiConsole.Markup("[rgb(255,0,0)]Baz[/] ");
AnsiConsole.WriteLine();
