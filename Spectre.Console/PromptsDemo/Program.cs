using Spectre.Console;

// Text prompt
{

    if (!AnsiConsole.Confirm("Run example?"))
    {
        return;
    }

    // Ask for the user's name
    string name = AnsiConsole.Ask<string>("What's your [green]name[/]?");

    // Ask for the user's age
    int age = AnsiConsole.Ask<int>("What's your [green]age[/]?");

    // Choices
    var fruit = AnsiConsole.Prompt(
        new TextPrompt<string>("What's your [green]favorite fruit[/]?")
            .InvalidChoiceMessage("[red]That's not a valid fruit[/]")
            .DefaultValue("Orange")
            .AddChoice("Apple")
            .AddChoice("Banana")
            .AddChoice("Orange"));

    // Validation
    var age2 = AnsiConsole.Prompt(
        new TextPrompt<int>("What's the secret number?")
            .Validate(age =>
            {
                return age switch
                {
                    < 99 => ValidationResult.Error("[red]Too low[/]"),
                    > 99 => ValidationResult.Error("[red]Too high[/]"),
                    _ => ValidationResult.Success(),
                };
            }));

    // Secrets
    var password = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter [green]password[/]")
            .PromptStyle("red")
            .Secret());

    // Optional
    var color = AnsiConsole.Prompt(
        new TextPrompt<string>("[grey][[Optional]][/] [green]Favorite color[/]?")
            .AllowEmpty());
}

// Selection
{
    // Ask for the user's favorite fruit
    var fruit = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("What's your [green]favorite fruit[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
            .AddChoices(new[] {
            "Apple", "Apricot", "Avocado",
            "Banana", "Blackcurrant", "Blueberry",
            "Cherry", "Cloudberry", "Cocunut",
            }));

    // Echo the fruit back to the terminal
    AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
}

// Multi Selection
{
    // Ask for the user's favorite fruits
    var fruits = AnsiConsole.Prompt(
        new MultiSelectionPrompt<string>()
            .Title("What are your [green]favorite fruits[/]?")
            .NotRequired() // Not required to have a favorite fruit
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle a fruit, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(new[] {
            "Apple", "Apricot", "Avocado",
            "Banana", "Blackcurrant", "Blueberry",
            "Cherry", "Cloudberry", "Cocunut",
            }));

    // Write the selected fruits to the terminal
    foreach (string fruit in fruits)
    {
        AnsiConsole.WriteLine(fruit);
    }
}
