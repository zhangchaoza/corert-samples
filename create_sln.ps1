using namespace System.IO

$a = Get-Command bash.exe
if ($null -eq $a) {
    Write-Error "Install wsl first."
    exit
}

bash.exe -c "tree -fi -P '*.csproj' -I 'bin|obj|debug|release|.vscode|.vs|.ionide' | grep 'csproj' > paths"
# bash.exe -c "tree -dfi --noreport -I 'bin|obj|debug|release|.vscode|.vs|.ionide' > paths"

if (-not (Test-Path paths) ) {
    Write-Error "Install tree in wsl first."
    exit
}

$paths = [File]::ReadAllLines("paths")

if ($paths.Count -gt 0) {
    dotnet new sln --force
}

dotnet.exe sln add $paths

# for ($i = 0; $i -lt $paths.Count; $i++) {
#     $cmd = "dotnet sln add " + $paths[$i];
#     Invoke-Expression $cmd
# }

Remove-Item paths