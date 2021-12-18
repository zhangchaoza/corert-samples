#!/bin/sh

dotnet publish AttributesCoreRTDemo/AttributesCoreRTDemo.csproj -c Release -r linux-x64 -f net6.0

./AttributesCoreRTDemo/bin/Release/net6.0/linux-x64/publish/AttributesCoreRTDemo