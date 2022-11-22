#!/bin/bash

dotnet publish -c Release -r linux-x64 -f net7.0 --sc

./bin/release/net7.0/linux-x64/publish/HostingDemo