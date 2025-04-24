public class FunctionParam1
{
    public FunctionParam1()
    {
        Time = DateTimeOffset.UtcNow;
    }

    public DateTimeOffset Time { get; init; }

    public Dictionary<string, SubFunctionParamRecord>? Regs { get; init; }
}
