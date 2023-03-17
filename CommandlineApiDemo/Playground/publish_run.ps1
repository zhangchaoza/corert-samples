dotnet.exe publish -c Release -r win-x64 -f net7.0 --sc

# .\bin\Release\net7.0\win-x64\publish\CommandlineApiDemo.exe > $PSScriptRoot\run.log
.\bin\Release\net7.0\win-x64\native\CommandlineApiDemo.exe
