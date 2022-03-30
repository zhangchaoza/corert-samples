public static class RunLua
{
    public static void Run()
    {
        // Creating Lua state:
        using (Lua state = new Lua())
        {
            // Lua can return multiple values, for this reason DoString return a array of objects
            var res = state.DoString("return 10.0 + 3*(5 + 2)")[0] as double?;
            Console.WriteLine(res);

            // Passing raw values to the state:
            double val = 12.0;
            state["x"] = val; // Create a global value 'x'
            var res2 = (double)state.DoString("return 10 + x*(5 + 2)")[0];
            Console.WriteLine(res2);

            // Retrieving global values:
            state.DoString("y = 10 + x*(5 + 2)");
            double y = (double)state["y"]; // Retrieve the value of y
            Console.WriteLine(y);

            // Retrieving Lua functions:
            state.DoString(@"
	function ScriptFunc (val1, val2)
		if val1 > val2 then
			return val1 + 1
		else
			return val2 - 1
		end
	end
	");
            var scriptFunc = state["ScriptFunc"] as LuaFunction;
            var res3 = (Int64)scriptFunc.Call(3, 5).First();
            Console.WriteLine(res3);
        }

    }
}
