public static class UseNetAssemblies
{
    public static void Run()
    {
        using Lua state = new Lua();

        // var l = new System.Net.WebClient().DownloadString("http://nlua.org").Length; //DevSkim: ignore DS137138
        // Console.WriteLine(l);

        // Using .NET assemblies inside Lua:
        state.LoadCLRPackage();
        state.DoString(@"import ('First', '')
                import ('System.Net.WebClient','System.Net') ");
        // import will load any .NET assembly and they will be available inside the Lua context.

        state.DoString(@"obj = SomeClass() -- you can suppress default values.
                client = WebClient()");

        state.DoString(@"res1 = obj:Func1()
                res2 = obj:AnotherFunc (10, 'hello')
                res3 = client:DownloadString('http://nlua.org'):len()"); //DevSkim: ignore DS137138
        state.DoString(@"res4 = SomeClass.StaticMethod(4)");
        state.DoString(@"res5 = obj.MyProperty");
        state.DoString(@"
                print(""res1="",res1)
                print(""res2="",res2)
                print(""res3="",res3)
                print(""res4="",res4)
                print(""res5="",res5)
        ");
    }
}
