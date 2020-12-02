namespace CommandlineApiDemo
{
    using System.CommandLine.Invocation;
    using System.CommandLine.IO;

    internal class SimpleResult : IInvocationResult
    {
        public void Apply(InvocationContext context)
        {
            context.Console.Out.WriteLine("\u001b[32mSimpleResult Apply\u001b[0m");
            context.ResultCode = 0;
        }
    }
}
