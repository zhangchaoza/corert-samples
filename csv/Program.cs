using Csv;

{
    var columnNames = new[] { "Id", "Name" };
    var rows = new[]
    {
        new [] { "0", "John Doe" },
        new [] { "1", "Jane Doe" }
    };
    var csv = CsvWriter.WriteToText(columnNames, rows, ',');
    File.WriteAllText("data.csv", csv);
}

{
    var options = new CsvOptions // Defaults
    {
        RowsToSkip = 0, // Allows skipping of initial rows without csv data
        SkipRow = (row, idx) => row.Length == 0 || row.Span[0] == '#',
        Separator = '\0', // Autodetects based on first row
        TrimData = false, // Can be used to trim each cell
        Comparer = null, // Can be used for case-insensitive comparison for names
        HeaderMode = HeaderMode.HeaderPresent, // Assumes first row is a header row
        ValidateColumnCount = false, // Checks each row immediately for column count
        ReturnEmptyForMissingColumn = false, // Allows for accessing invalid column names
        Aliases = null, // A collection of alternative column names
        AllowNewLineInEnclosedFieldValues = false, // Respects new line (either \r\n or \n) characters inside field values enclosed in double quotes.
        AllowBackSlashToEscapeQuote = false, // Allows the sequence "\"" to be a valid quoted value (in addition to the standard """")
        AllowSingleQuoteToEncloseFieldValues = false, // Allows the single-quote character to be used to enclose field values
        NewLine = Environment.NewLine // The new line string to use when multiline field values are read (Requires "AllowNewLineInEnclosedFieldValues" to be set to "true" for this to have any effect.)
    };

    var csv = File.ReadAllText("data.csv");
    foreach (var line in CsvReader.ReadFromText(csv, options))
    {
        // Header is handled, each line will contain the actual row data
        // var firstCell = line[0];
        var byName = line["Name"];
        Console.WriteLine("{0} {1}",line["Id"],line["Name"]);
    }
}
