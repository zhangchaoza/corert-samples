# The above command will drop a shared library (Windows .dll, OSX .dylib, Linux .so)
# in ./bin/[configuration]/netstandard2.0/[RID]/publish/ folder and will have the
# same name as the folder in which your source file is present. Building shared
# libraries on Linux is currently non-functional, see #4988.
dotnet publish /p:NativeLib=Shared /p:SelfContained=true -r win-x64 -c release