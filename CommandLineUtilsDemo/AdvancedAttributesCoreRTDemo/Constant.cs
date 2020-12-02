namespace AdvancedAttributesCoreRTDemo
{
    using McMaster.Extensions.CommandLineUtils;

    public static class Constant
    {
        public static void ConfigurePrimary(CommandLineApplication<Primary> app)
        {
            app.Command<SubCommand1>(name: "config", configuration: subCmd =>
            {
            });
        }
    }
}
