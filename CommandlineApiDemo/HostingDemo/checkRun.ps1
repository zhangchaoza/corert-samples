.\publish_run_client.ps1
.\bin\Release\net5.0\win-x64\native\HostingDemo.exe  *> rt.log
dotnet.exe run -c release -r win-x64 *> run.log

code.exe -d rt.log run.log