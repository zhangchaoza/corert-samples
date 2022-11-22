dotnet.exe publish BuilderApiCoreRTDemo/BuilderApiCoreRTDemo.csproj -c Release -r win-x64 -f net7.0

.\BuilderApiCoreRTDemo\bin\Release\net7.0\win-x64\publish\BuilderApiCoreRTDemo.exe `
    "subcommand1" `
    "20" `
    "hello world vscode" `
    "-s:haha" `
    "-r:50" `
    "-v" `
    "-a:123"