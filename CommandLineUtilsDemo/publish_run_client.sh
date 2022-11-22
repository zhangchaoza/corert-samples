#!/bin/sh

dotnet publish BuilderApiCoreRTDemo/BuilderApiCoreRTDemo.csproj -c Release -r linux-x64 -f net7.0

./BuilderApiCoreRTDemo/bin/Release/net7.0/linux-x64/publish/BuilderApiCoreRTDemo \
    "subcommand1" \
    "20" \
    "hello world vscode" \
    "-s:haha" \
    "-r:50" \
    "-v" \
    "-a:123"