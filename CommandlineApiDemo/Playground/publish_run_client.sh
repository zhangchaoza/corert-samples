#!/bin/bash

dotnet publish -c Release -r linux-x64 -f net6.0 --sc

# .\bin\Release\net6.0\win-x64\publish\CommandlineApiDemo.exe > $PSScriptRoot\run.log
bin/Release/net6.0/linux-x64/native/CommandlineApiDemo
