namespace CustomServerDemo.CustomFtpServer
{
    using FubarDev.FtpServer;
    using FubarDev.FtpServer.FileSystem;
    using Microsoft.Extensions.DependencyInjection;

    public static class CustomFtpServerBuilderExtensions
    {
        public static IFtpServerBuilder UseCustomServerDemo(this IFtpServerBuilder builder)
        {
            builder.Services.AddSingleton<IFileSystemClassFactory, CustomServerDemoProvider>();
            return builder;
        }
    }
}
