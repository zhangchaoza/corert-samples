# The above command will drop a static library (Windows .lib, OSX/Linux .a)
# in ./bin/[configuration]/netstandard2.0/[RID]/publish/ folder and will
# have the same name as the folder in which your source file is present.
dotnet publish /p:NativeLib=Static /p:SelfContained=true -r win-x64 -c release