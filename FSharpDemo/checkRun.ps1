.\publish_run_client.ps1
.\bin\Release\net7.0\win-x64\publish\FSharpDemo.exe  *> rt.log
dotnet.exe run -c release -r win-x64 *> run.log