#!/bin/sh

dotnet publish AttributesCoreRTDemo/AttributesCoreRTDemo.csproj -c Release -r linux-x64 -f net5.0

./AttributesCoreRTDemo/bin/Release/net5.0/linux-x64/publish/AttributesCoreRTDemo