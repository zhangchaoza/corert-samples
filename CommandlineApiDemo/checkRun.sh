#!/bin/bash

sh publish_run_client.sh
bin/Release/net5.0/linux-x64/native/CommandlineApiDemo &> rt.log
dotnet run -c release -r linux-x64 &> run.log