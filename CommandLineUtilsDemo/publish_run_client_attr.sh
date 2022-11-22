#!/bin/sh

dotnet publish AttributesCoreRTDemo/AttributesCoreRTDemo.csproj -c Release -r linux-x64 -f net7.0

./AttributesCoreRTDemo/bin/Release/net7.0/linux-x64/publish/AttributesCoreRTDemo