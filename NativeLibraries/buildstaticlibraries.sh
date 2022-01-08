#!/bin/sh

dotnet publish /p:NativeLib=Static /p:SelfContained=true -r linux-x64 -c release