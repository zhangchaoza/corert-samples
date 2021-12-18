// Create a table
using SixLabors.ImageSharp.Processing;
using Spectre.Console;

{
    var table = new Table();

    // Add some columns
    table.AddColumn("Foo");
    table.AddColumn(new TableColumn("Bar").Centered());

    // Add some rows
    table.AddRow("Baz", "[green]Qux[/]");
    table.AddRow(new Markup("[blue]Corgi[/]"), new Panel("Waldo"));

    // Render the table to the console
    AnsiConsole.Write(table);
}

{
    // Create the tree
    var root = new Tree("Root");

    // Add some nodes
    var foo = root.AddNode("[yellow]Foo[/]");
    var table = foo.AddNode(new Table()
        .RoundedBorder()
        .AddColumn("First")
        .AddColumn("Second")
        .AddRow("1", "2")
        .AddRow("3", "4")
        .AddRow("5", "6"));

    table.AddNode("[blue]Baz[/]");
    foo.AddNode("Qux");

    var bar = root.AddNode("[yellow]Bar[/]");
    bar.AddNode(new Calendar(2020, 12)
        .AddCalendarEvent(2020, 12, 12)
        .HideHeader());

    // Render the tree
    AnsiConsole.Write(root);
}

{
    AnsiConsole.Write(new BarChart()
        .Width(60)
        .Label("[green bold underline]Number of fruits[/]")
        .CenterLabel()
        .AddItem("Apple", 12, Color.Yellow)
        .AddItem("Orange", 54, Color.Green)
        .AddItem("Banana", 33, Color.Red));
}

{
    var rule = new Rule();
    AnsiConsole.Write(rule);
}

{
    var rule = new Rule("[red]Hello[/]");
    AnsiConsole.Write(rule);
}

{
    var rule = new Rule("[red]Hello[/]");
    rule.Alignment = Justify.Left;
    AnsiConsole.Write(rule);
}

{
    var rule = new Rule("[red]Hello[/]");
    rule.LeftAligned();
    AnsiConsole.Write(rule);
}

{
    var rule = new Rule("[red]Hello[/]");
    rule.Style = Style.Parse("red dim");
    AnsiConsole.Write(rule);
}

{
    var rule = new Rule("[red]Hello[/]");
    rule.RuleStyle("red dim");
    AnsiConsole.Write(rule);
}

{
    var calendar = new Calendar(2020, 10);
    AnsiConsole.Write(calendar);
}

{
    var calendar = new Calendar(2020, 10);
    calendar.Culture("zh-CN");
    AnsiConsole.Write(calendar);
}

{
    var calendar = new Calendar(2020, 10);
    calendar.HideHeader();
    AnsiConsole.Write(calendar);
}

{
    var calendar = new Calendar(2020, 10);
    calendar.HeaderStyle(Style.Parse("blue bold"));
    AnsiConsole.Write(calendar);
}

{
    var calendar = new Calendar(2020, 10);
    calendar.AddCalendarEvent(2020, 10, 11);
    AnsiConsole.Write(calendar);
}

{
    var calendar = new Calendar(2020, 10);
    calendar.AddCalendarEvent(2020, 10, 11);
    calendar.HighlightStyle(Style.Parse("yellow bold"));
    AnsiConsole.Write(calendar);
}

{
    AnsiConsole.Write(
        new FigletText("Hello")
            .LeftAligned()
            .Color(Color.Red));
}

{
    // Create a canvas
    var canvas = new Canvas(16, 16);

    // Draw some shapes
    for (var i = 0; i < canvas.Width; i++)
    {
        // Cross
        canvas.SetPixel(i, i, Color.White);
        canvas.SetPixel(canvas.Width - i - 1, i, Color.White);

        // Border
        canvas.SetPixel(i, 0, Color.Red);
        canvas.SetPixel(0, i, Color.Green);
        canvas.SetPixel(i, canvas.Height - 1, Color.Blue);
        canvas.SetPixel(canvas.Width - 1, i, Color.Yellow);
    }

    // Render the canvas
    AnsiConsole.Write(canvas);
}

{
    // Load an image
    var image = new CanvasImage("cake.png");

    // Set the max width of the image.
    // If no max width is set, the image will take
    // up as much space as there is available.
    image.MaxWidth(16);

    // Render the image to the console
    AnsiConsole.Write(image);
}

{
    // Load an image
    var image = new CanvasImage("cake.png");
    image.MaxWidth(32);

    // Set a sampler that will be used when scaling the image.
    image.BilinearResampler();

    // Mutate the image using ImageSharp
    image.Mutate(ctx => ctx.Grayscale().Rotate(-45).EntropyCrop());

    // Render the image to the console
    AnsiConsole.Write(image);
}
