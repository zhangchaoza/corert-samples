public class SomeClass
{
    public string MyProperty { get; private set; }

    public SomeClass(string param1 = "defaultValue")
    {
        MyProperty = param1;
    }

    public int Func1()
    {
        return 32;
    }

    public string AnotherFunc(int val1, string val2)
    {
        return "Some String";
    }

    public static string StaticMethod(int param)
    {
        return "Return of Static Method";
    }
}
