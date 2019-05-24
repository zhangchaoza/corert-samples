#!/bin/sh

dotnet publish AttributesCoreRTDemo/AttributesCoreRTDemo.csproj -c Release -r linux-x64 -f netcoreapp3.0

./AttributesCoreRTDemo/bin/Release/netcoreapp3.0/linux-x64/publish/AttributesCoreRTDemo