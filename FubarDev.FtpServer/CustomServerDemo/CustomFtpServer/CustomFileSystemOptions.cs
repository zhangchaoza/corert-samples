namespace CustomServerDemo.CustomFtpServer
{
    public class CustomServerDemoOptions
    {
        /// <summary>
        /// Gets or sets the root path for all users.
        /// </summary>
        public string? RootPath { get; set; }

        /// <summary>
        /// Gets or sets the buffer size to be used in async IO methods.
        /// </summary>
        public int? StreamBufferSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether deletion of non-empty directories is allowed.
        /// </summary>
        public bool AllowNonEmptyDirectoryDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content should be flushed to disk after every write operation.
        /// </summary>
        public bool FlushAfterWrite { get; set; }
    }
}
