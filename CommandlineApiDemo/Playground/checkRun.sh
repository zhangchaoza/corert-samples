#!/bin/bash

sh publish_run.sh
bin/Release/net7.0/linux-x64/native/CommandlineApiDemo &>rt.log
dotnet run -c release -r linux-x64 --sc &>run.log

code -d rt.log run.log
