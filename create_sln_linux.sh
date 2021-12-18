#!/bin/pwsh

using namespace System.IO

tree -fi -P '*.csproj' -I 'bin|obj|debug|release|.vscode|.vs|.ionide|CacheDemoWPF|Webview2Demo' | grep 'csproj' > paths

$paths = [File]::ReadAllLines("paths")

if ($paths.Count -gt 0) {
    dotnet new sln --force
}

dotnet sln add $paths

# for ($i = 0; $i -lt $paths.Count; $i++) {
#     $cmd = "dotnet sln add " + $paths[$i];
#     Invoke-Expression $cmd
# }

Remove-Item paths
