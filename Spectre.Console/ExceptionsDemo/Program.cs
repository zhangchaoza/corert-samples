using Spectre.Console;

// Exceptions
var ex = new NotSupportedException("不支持", new Exception("内部"));
AnsiConsole.WriteException(ex);

AnsiConsole.WriteException(ex,
    ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes |
    ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);

AnsiConsole.WriteException(ex, new ExceptionSettings
{
    Format = ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks,
    Style = new ExceptionStyle
    {
        Exception = new Style().Foreground(Color.Grey),
        Message = new Style().Foreground(Color.White),
        NonEmphasized = new Style().Foreground(Color.Cornsilk1),
        Parenthesis = new Style().Foreground(Color.Cornsilk1),
        Method = new Style().Foreground(Color.Red),
        ParameterName = new Style().Foreground(Color.Cornsilk1),
        ParameterType = new Style().Foreground(Color.Red),
        Path = new Style().Foreground(Color.Red),
        LineNumber = new Style().Foreground(Color.Cornsilk1),
    }
});
