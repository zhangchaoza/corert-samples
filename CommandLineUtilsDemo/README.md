# 1. CommandlineUtils #

- [1. CommandlineUtils](#1-commandlineutils)
  - [1.1. AttributesCoreRTDemo](#11-attributescorertdemo)
    - [1.1.1. windows](#111-windows)
    - [1.2.2. linux](#122-linux)
  - [1.2. BuilderApiCoreRTDemo](#12-builderapicorertdemo)
    - [1.2.1. windows](#121-windows)
    - [1.2.2. linux](#122-linux-1)

## 1.1. AttributesCoreRTDemo ##

未实现，原因：

- RD.xml现不支持方法重载，[ValueParserProvider](https://github.com/natemcmaster/CommandLineUtils/blob/master/src/CommandLineUtils/Abstractions/ValueParserProvider.cs)存在`GetParser`方法重载

相关issues跟踪

- [run-time exception with McMaster.Extensions.CommandLineUtils](https://github.com/dotnet/corert/issues/6245#issuecomment-465021958)

现有解决方案

代码预先执行泛型方法

```C#
    var app = new CommandLineApplication<Primary>();
    _ = app.ValueParsers.GetParser<int>();
    _ = app.ValueParsers.GetParser<bool>();
    _ = app.ValueParsers.GetParser<int?>();
    _ = app.ValueParsers.GetParser<bool?>();
    _ = app.ValueParsers.GetParser<string>();
    _ = app.ValueParsers.GetParser<EnumOption>();
    _ = app.ValueParsers.GetParser<EnumOption?>();

```

### 1.1.1. windows ###

执行`publish_run_client_attr.ps1`

### 1.2.2. linux ###

执行`publish_run_client_attr.ps1`

## 1.2. BuilderApiCoreRTDemo ##

使用BuilderApi时无需配置RD.xml文件

### 1.2.1. windows ###

执行`publish_run_client.ps1`

### 1.2.2. linux ###

    sudo apt-get install clang-3.9
    sudo apt-get install libcurl4-openssl-dev zlib1g-dev libkrb5-dev

执行`publish_run_client.sh`