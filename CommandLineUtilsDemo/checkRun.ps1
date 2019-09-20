.\publish_run_client_attr.ps1 *> attr_rt.log
dotnet.exe run -p .\AttributesCoreRTDemo\AttributesCoreRTDemo.csproj -c release -r win-x64 *> attr.log

.\publish_run_client_attr_adv.ps1 *> adv_attr_rt.log
dotnet.exe run -p .\AdvancedAttributesCoreRTDemo\AdvancedAttributesCoreRTDemo.csproj -c release -r win-x64 *> adv_attr.log

.\publish_run_client.ps1 *> builder_rt.log
dotnet.exe run -p .\BuilderApiCoreRTDemo\BuilderApiCoreRTDemo.csproj -c release -r win-x64 -- `
"subcommand1" `
    "20" `
    "hello world vscode" `
    "-s:haha" `
    "-r:50" `
    "-v" `
    "-a:123" *> builder.log
