using System;
using CustomServerDemo.CustomFtpServer;
using FubarDev.FtpServer;
using FubarDev.FtpServer.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Setup dependency injection
var services = new ServiceCollection();

// use %TEMP%/TestFtpServer as root folder
services.Configure<CustomServerDemoOptions>(opt => opt.RootPath = @"D:\FTP\软件\数据库\SQLServer");

// Add FTP server services
// DotNetFileSystemProvider = Use the .NET file system functionality
// AnonymousMembershipProvider = allow only anonymous logins
services
    .AddLogging(logging =>
    {
        logging.AddConsole();
    })
    .AddFtpServer(builder => builder
    .UseCustomServerDemo() // Use the .NET file system functionality
    .EnableAnonymousAuthentication()); // allow anonymous logins

// override Retr command
// services.AddSingleton<IFtpCommandHandlerScanner>(_ => new AssemblyFtpCommandHandlerScanner(typeof(RetrCommandHandler).Assembly));
services.AddSingleton<IFtpCommandHandlerScanner, SimpleFtpCommandHandlerScanner>();

// Configure the FTP server
services.Configure<FtpServerOptions>(opt => opt.ServerAddress = "127.0.0.1");

// Build the service provider
using (var serviceProvider = services.BuildServiceProvider())
{
    // Initialize the FTP server
    var ftpServerHost = serviceProvider.GetRequiredService<IFtpServerHost>();

    // Start the FTP server
    await ftpServerHost.StartAsync();

    Console.WriteLine("Press ENTER/RETURN to close the test application.");
    Console.ReadLine();

    // Stop the FTP server
    await ftpServerHost.StopAsync();
}
