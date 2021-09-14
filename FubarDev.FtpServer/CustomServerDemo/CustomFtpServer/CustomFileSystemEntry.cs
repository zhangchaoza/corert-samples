namespace CustomServerDemo.CustomFtpServer
{
    using System;
    using System.IO;
    using FubarDev.FtpServer.FileSystem;
    using FubarDev.FtpServer.FileSystem.Generic;

    public class CustomServerDemoEntry : IUnixFileSystemEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomServerDemoEntry"/> class.
        /// </summary>
        /// <param name="fsInfo">The <see cref="FileSystemInfo"/> to extract the information from.</param>
        protected CustomServerDemoEntry(FileSystemInfo fsInfo)
        {
            Info = fsInfo;
            LastWriteTime = new DateTimeOffset(Info.LastWriteTime);
            CreatedTime = new DateTimeOffset(Info.CreationTimeUtc);
            var accessMode = new GenericAccessMode(true, true, true);
            Permissions = new GenericUnixPermissions(accessMode, accessMode, accessMode);
        }

        /// <summary>
        /// Gets the underlying <see cref="DirectoryInfo"/>.
        /// </summary>
        public FileSystemInfo Info { get; }

        public string Name => Info.Name;

        public IUnixPermissions Permissions { get; }

        public DateTimeOffset? LastWriteTime { get; }

        public DateTimeOffset? CreatedTime { get; }

        public long NumberOfLinks => 1;

        public string Owner => "owner";

        public string Group => "group";
    }
}
