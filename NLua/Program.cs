global using NLua;
using System.Text;

using (Lua lua = new Lua())
{
    lua.State.Encoding = Encoding.UTF8;
    lua.DoString("res = 'Файл'");// res全局变量
    lua.DoString("local res2 = 'Файл'");// res2 局部变量
    string res = (string)lua["res"];
    string res2 = (string)lua["res2"];

    Console.WriteLine("global var:{0}", res);
    Console.WriteLine("local var:{0}", res2);
}

// RunLua.Run();
UseCSharpObject.Run();
// UseNetAssemblies.Run();
