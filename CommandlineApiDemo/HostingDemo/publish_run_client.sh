#!/bin/bash

dotnet publish -c Release -r linux-x64 -f net5.0

./bin/release/net5.0/linux-x64/publish/HostingDemo