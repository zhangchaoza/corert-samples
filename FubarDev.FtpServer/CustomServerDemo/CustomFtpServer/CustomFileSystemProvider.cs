namespace CustomServerDemo.CustomFtpServer
{
    using System.IO;
    using System.Threading.Tasks;
    using FubarDev.FtpServer;
    using FubarDev.FtpServer.FileSystem;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class CustomServerDemoProvider : IFileSystemClassFactory
    {
        private readonly IAccountDirectoryQuery _accountDirectoryQuery;
        private readonly ILogger<CustomServerDemoProvider>? _logger;
        private readonly string _rootPath;
        private readonly int _streamBufferSize;
        private readonly bool _allowNonEmptyDirectoryDelete;
        private readonly bool _flushAfterWrite;

        public CustomServerDemoProvider(
            IOptions<CustomServerDemoOptions> options,
            IAccountDirectoryQuery accountDirectoryQuery,
            ILogger<CustomServerDemoProvider>? logger = null
        )
        {
            _accountDirectoryQuery = accountDirectoryQuery;
            _logger = logger;
            _rootPath = string.IsNullOrEmpty(options.Value.RootPath) ? Path.GetTempPath() : options.Value.RootPath!;
            _streamBufferSize = options.Value.StreamBufferSize ?? CustomServerDemo.DefaultStreamBufferSize;
            _allowNonEmptyDirectoryDelete = options.Value.AllowNonEmptyDirectoryDelete;
            _flushAfterWrite = options.Value.FlushAfterWrite;
        }

        public Task<IUnixFileSystem> Create(IAccountInformation accountInformation)
        {
            var path = _rootPath;
            var directories = _accountDirectoryQuery.GetDirectories(accountInformation);
            if (!string.IsNullOrEmpty(directories.RootPath))
            {
                path = Path.Combine(path, directories.RootPath);
            }

            _logger?.LogDebug("The root directory for {userName} is {rootPath}", accountInformation.FtpUser.Identity?.Name ?? "unknown", path);

            return Task.FromResult<IUnixFileSystem>(new CustomServerDemo(path, _allowNonEmptyDirectoryDelete, _streamBufferSize, _flushAfterWrite));
        }
    }
}
