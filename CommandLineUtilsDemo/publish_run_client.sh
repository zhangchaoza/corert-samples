#!/bin/sh

dotnet publish BuilderApiCoreRTDemo/BuilderApiCoreRTDemo.csproj -c Release -r linux-x64 -f net6.0

./BuilderApiCoreRTDemo/bin/Release/net6.0/linux-x64/publish/BuilderApiCoreRTDemo \
    "subcommand1" \
    "20" \
    "hello world vscode" \
    "-s:haha" \
    "-r:50" \
    "-v" \
    "-a:123"