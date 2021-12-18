#!/bin/bash

sh publish_run_client.sh
bin/Release/net6.0/linux-x64/native/HostingDemo &> rt.log
dotnet run -c release -r linux-x64 &> run.log

code -d rt.log run.log