public static class UseCSharpObject
{
    public static void Run()
    {
        using Lua state = new Lua();

        // Passing.NET objects to the state:
        SomeClass obj = new SomeClass("Param");
        state["obj"] = obj; // Create a global value 'obj' of .NET type SomeClass
        // This could be any .NET object, from BCL or from your assemblies

        state.DoString("print(obj.MyProperty)");
        state.DoString("print(obj:Func1())");
    }
}
