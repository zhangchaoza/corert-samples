﻿.\publish_run.ps1
.\bin\Release\net7.0\win-x64\native\CommandlineApiDemo.exe  *> rt.log
dotnet.exe run -c release -r win-x64 --sc *> run.log

code -d rt.log run.log
