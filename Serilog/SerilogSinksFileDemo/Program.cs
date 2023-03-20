using Serilog;
using Serilog.Formatting.Display;

var log = new LoggerConfiguration()
    .WriteTo.File(
        path: "log..log",
        formatter: new MessageTemplateTextFormatter("{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"),
        fileSizeLimitBytes: 128,// 文件大小限制
        rollOnFileSizeLimit: true,// 使用文件大小滚动
        retainedFileCountLimit: 8,// 文件数量限制
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(2),
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

for (int i = 0; i < 128; i++)
{
    log.Information("012345678");
}
