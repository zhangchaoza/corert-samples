namespace CommandlineApiDemo
{
    using System.CommandLine;
    using System.CommandLine.Invocation;

    internal class SimpleResult : IInvocationResult
    {
        public void Apply(InvocationContext context)
        {
            context.Console.Out.WriteLine("\u001b[32mSimpleResult Apply\u001b[0m");
            context.ResultCode = 0;
        }
    }
}
