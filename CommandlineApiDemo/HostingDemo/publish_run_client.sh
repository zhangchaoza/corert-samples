#!/bin/bash

dotnet publish -c Release -r linux-x64 -f net6.0 --sc

./bin/release/net6.0/linux-x64/publish/HostingDemo