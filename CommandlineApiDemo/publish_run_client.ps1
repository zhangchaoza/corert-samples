dotnet publish -c Release -r win-x64 -f netcoreapp3.0

.\bin\Release\netcoreapp3.0\win-x64\publish\CommandlineApiDemo.exe > $PSScriptRoot\run.log 
.\bin\Release\netcoreapp3.0\win-x64\native\CommandlineApiDemo.exe > $PSScriptRoot\run_rt.log 