#!/bin/bash

sh publish_run_client.sh
bin/Release/net6.0/linux-x64/publish/FSharpDemo &> rt.log
dotnet run -c release -r linux-x64 &> run.log