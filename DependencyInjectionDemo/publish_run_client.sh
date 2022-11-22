#!/bin/bash

dotnet publish -c Release -r win-x64 -f net7.0 --sc

./bin/Release/net7.0/win-x64/publish/DIDemo