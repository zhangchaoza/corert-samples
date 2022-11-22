dotnet publish /p:NativeLib=Shared /p:SelfContained=true -r win-x64 -c release
gcc caller_dll.c -o .\bin\Release\net7.0\win-x64\publish\caller_dll.exe
.\bin\Release\net7.0\win-x64\publish\caller_dll.exe