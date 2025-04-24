public static class UseCSharpObject
{
    public static void Run()
    {
        using Lua state = new Lua();

        // Passing.NET objects to the state:
        SomeClass obj = new SomeClass("Param");
        state["obj"] = obj; // Create a global value 'obj' of .NET type SomeClass
                            // This could be any .NET object, from BCL or from your assemblies

        // simple type
        state.DoString("print(obj.MyProperty)");
        state.DoString("print(obj:Func1())");

        // Dictionary type
        state.DoString("""
            function Function_1(c)
                print(c.Time)
                print(c.Regs["1"].Name) -- use string key
                print(c.Regs.a.Name)

                -- use GetEnumerator of Dictionary
                local regs = c.Regs
                local enumerator = regs:GetEnumerator()
                while enumerator:MoveNext() do
                    local pair = enumerator.Current
                    print(pair.Key,pair.Value, pair.Value.Name, pair.Value.Age)
                end
            end

            function Function_2(c)
                print(c, c.Time)
                print(c, c.Regs[1].Name) -- use number key
                print(c, c.Regs.a.Name)

                -- Traverse the table of consecutive integers starting from 1
                for i, v in ipairs(c.Regs) do
                    print(i,v, v["Name"], v["Age"])
                end

                -- Traverse the table
                for k, v in pairs(c.Regs) do
                    print(k,v, v["Name"], v["Age"])
                end
            end

        """);
        using var func1 = state.GetFunction("Function_1");
        var fp1 = new FunctionParam1()
        {
            Regs = new Dictionary<string, SubFunctionParamRecord>()
            {
                {"1", new SubFunctionParamRecord("1", 1)},
                {"2", new SubFunctionParamRecord("2", 2)},
                {"3", new SubFunctionParamRecord("3", 3)},
                {"a", new SubFunctionParamRecord("a", 4)},
                {"b", new SubFunctionParamRecord("b", 5)},
                {"c", new SubFunctionParamRecord("c", 6)},
                {"d", new SubFunctionParamRecord("d", 7)},
            }
        };
        func1.Call(fp1);

        // Dictionary type with lua table
        state.NewTable("fc2_tb");
        var table = (LuaTable)state["fc2_tb"];
        table["Time"] = DateTimeOffset.UtcNow;
        state.NewTable($"fc2_tb.Regs");
        ((LuaTable)table["Regs"])[1] = new SubFunctionParamRecord("1", 1);
        ((LuaTable)table["Regs"])[2] = new SubFunctionParamRecord("2", 2);
        ((LuaTable)table["Regs"])[3] = new SubFunctionParamRecord("3", 3);
        table["Regs.a"] = new SubFunctionParamRecord("a", 4);
        table["Regs.b"] = new SubFunctionParamRecord("b", 5);
        table["Regs.c"] = new SubFunctionParamRecord("c", 6);
        table["Regs.d"] = new SubFunctionParamRecord("d", 7);
        using var func2 = state.GetFunction("Function_2");
        func2.Call(table);
    }
}
