dotnet publish /p:NativeLib=Static /p:SelfContained=true -r win-x64 -c release
gcc caller_lib.c -o .\bin\Release\net7.0\win-x64\publish\caller_lib.exe -L.\bin\Release\net7.0\win-x64\publish\ -l:NativeLibrary.lib
.\bin\Release\net7.0\win-x64\publish\caller_lib.exe