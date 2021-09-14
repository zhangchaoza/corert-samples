namespace CustomServerDemo.CustomFtpServer
{
    using System.IO;
    using FubarDev.FtpServer.FileSystem;

    public class CustomFileEntry : CustomServerDemoEntry, IUnixFileEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFileEntry"/> class.
        /// </summary>
        /// <param name="info">The <see cref="FileInfo"/> to extract the information from.</param>
        public CustomFileEntry(FileInfo info)
            : base(info)
        {
            FileInfo = info;
        }

        /// <summary>
        /// Gets the file information.
        /// </summary>
        public FileInfo FileInfo { get; }

        /// <inheritdoc/>
        public long Size => FileInfo.Length;
    }
}
