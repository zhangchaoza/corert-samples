#!/bin/sh

dotnet publish -c Release -r linux-x64

./bin/Release/net8.0/linux-x64/publish/SpectreFirstDemo
