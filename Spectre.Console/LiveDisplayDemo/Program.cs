using Spectre.Console;

{
    var table = new Table().Centered();

    AnsiConsole.Live(table)
        .Start(ctx =>
        {
            table.AddColumn("Foo");
            ctx.Refresh();
            Thread.Sleep(1000);

            table.AddColumn("Bar");
            ctx.Refresh();
            Thread.Sleep(1000);
        });
}

{

    var table = new Table().Centered();

    await AnsiConsole.Live(table)
        .StartAsync(async ctx =>
        {
            table.AddColumn("Foo");
            ctx.Refresh();
            await Task.Delay(1000);

            table.AddColumn("Bar");
            ctx.Refresh();
            await Task.Delay(1000);
        });
}

{
    var table = new Table().Centered();

    await AnsiConsole.Live(table)
        .AutoClear(false)   // Do not remove when done
        .Overflow(VerticalOverflow.Ellipsis) // Show ellipsis when overflowing
        .Cropping(VerticalOverflowCropping.Top) // Crop overflow at top
        .StartAsync(async ctx =>
        {
            // Omitted
            table.AddColumn("Foo");
            ctx.Refresh();
            await Task.Delay(1000);

            table.AddColumn("Bar");
            ctx.Refresh();
            await Task.Delay(1000);
        });
}

{
    // Synchronous
    AnsiConsole.Progress()
        .Start(ctx =>
        {
            // Define tasks
            var task1 = ctx.AddTask("[green]Reticulating splines[/]");
            var task2 = ctx.AddTask("[green]Folding space[/]");

            while (!ctx.IsFinished)
            {
                Thread.Sleep(250);

                task1.Increment(5);
                task2.Increment(4.5);
            }
        });
}


{
    // Asynchronous
    await AnsiConsole.Progress()
        .StartAsync(async ctx =>
        {
            // Define tasks
            var task1 = ctx.AddTask("[green]Reticulating splines[/]");
            var task2 = ctx.AddTask("[green]Folding space[/]");

            while (!ctx.IsFinished)
            {
                // Simulate some work
                await Task.Delay(250);

                // Increment
                task1.Increment(5);
                task2.Increment(4.5);
            }
        });
}

{
    await AnsiConsole.Progress()
        .AutoRefresh(true) // Turn off auto refresh
        .AutoClear(true)   // Do not remove the task list when done
        .HideCompleted(true)   // Hide tasks as they are completed
        .Columns(new ProgressColumn[]
        {
            new TaskDescriptionColumn(),    // Task description
            new ProgressBarColumn(),        // Progress bar
            new PercentageColumn(),         // Percentage
            new RemainingTimeColumn(),      // Remaining time
            new SpinnerColumn(),            // Spinner
        })
        .StartAsync(async ctx =>
        {
            // Omitted
            var task1 = ctx.AddTask("[green]Reticulating splines[/]");
            var task2 = ctx.AddTask("[green]Folding space[/]");

            while (!ctx.IsFinished)
            {
                // Simulate some work
                await Task.Delay(250);

                // Increment
                task1.Increment(5);
                task2.Increment(4.5);
            }
        });
}

{
    // Synchronous
    AnsiConsole.Status()
        .Start("Thinking...", ctx =>
        {
            // Simulate some work
            AnsiConsole.MarkupLine("Doing some work...");
            Thread.Sleep(1000);

            // Update the status and spinner
            ctx.Status("Thinking some more");
            ctx.Spinner(Spinner.Known.Star);
            ctx.SpinnerStyle(Style.Parse("green"));

            // Simulate some work
            AnsiConsole.MarkupLine("Doing some more work...");
            Thread.Sleep(2000);
        });
}

{
    // Asynchronous
    await AnsiConsole.Status()
        .StartAsync("Thinking...", async ctx =>
        {
            // Simulate some work
            AnsiConsole.MarkupLine("Doing some work...");
            await Task.Delay(1000);

            // Update the status and spinner
            ctx.Status("Thinking some more");
            ctx.Spinner(Spinner.Known.Star);
            ctx.SpinnerStyle(Style.Parse("green"));

            // Simulate some work
            AnsiConsole.MarkupLine("Doing some more work...");
            await Task.Delay(2000);
        });
}

{
    await AnsiConsole.Status()
        .AutoRefresh(true)
        .Spinner(Spinner.Known.Star)
        .SpinnerStyle(Style.Parse("green bold"))
        .StartAsync("Thinking...", async ctx =>
        {
            // Simulate some work
            AnsiConsole.MarkupLine("Doing some work...");
            await Task.Delay(1000);

            // Update the status and spinner
            ctx.Status("Thinking some more");
            ctx.Spinner(Spinner.Known.Star);
            ctx.SpinnerStyle(Style.Parse("green"));

            // Simulate some work
            AnsiConsole.MarkupLine("Doing some more work...");
            await Task.Delay(2000);
        });
}
