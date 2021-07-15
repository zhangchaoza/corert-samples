#!/bin/bash

dotnet publish -c Release -r linux-x64 -f net5.0

# .\bin\Release\net5.0\win-x64\publish\CommandlineApiDemo.exe > $PSScriptRoot\run.log
bin/Release/net5.0/linux-x64/native/CommandlineApiDemo