using FluentFTP;

// create an FTP client and specify the host, username and password
// (delete the credentials to use the "anonymous" account)

FtpClient client = new FtpClient(host: "127.0.0.1", port: 10021, user: "zc", pass: "zc123");

// connect to the server and automatically detect working FTP settings
await client.AutoConnectAsync();

// get a list of files and directories in the "/" folder
foreach (FtpListItem item in await client.GetListingAsync("/"))
{
    Console.WriteLine(item);
    // // if this is a file
    // if (item.Type == FtpFileSystemObjectType.File)
    // {

    //     // get the file size
    //     long size = await client.GetFileSizeAsync(item.FullName);

    //     // calculate a hash for the file on the server side (default algorithm)
    //     FtpHash hash = await client.GetChecksumAsync(item.FullName);
    // }

    // // get modified date/time of the file or folder
    // DateTime time = await client.GetModifiedTimeAsync(item.FullName);
}

var tempFile = Path.GetTempFileName();
await File.WriteAllTextAsync(tempFile, "hello world");

// upload a file
await client.UploadFileAsync(tempFile, "/file");

// move the uploaded file
await client.MoveFileAsync("/file", "/file2");

var tempFile2 = Path.GetTempFileName();
// download the file again
await client.DownloadFileAsync(tempFile2, "/file2");

// compare the downloaded file with the server
var e = await client.CompareFileAsync(tempFile2, "/file2");
Console.WriteLine("File equal:{0}", e);

// delete the file
await client.DeleteFileAsync("/file2");

// // upload a folder and all its files
// await client.UploadDirectoryAsync(@"C:\website\videos\", @"/public_html/videos", FtpFolderSyncMode.Update);

// // upload a folder and all its files, and delete extra files on the server
// await client.UploadDirectoryAsync(@"C:\website\assets\", @"/public_html/assets", FtpFolderSyncMode.Mirror);

// // download a folder and all its files
// await client.DownloadDirectoryAsync(@"C:\website\logs\", @"/public_html/logs", FtpFolderSyncMode.Update);

// // download a folder and all its files, and delete extra files on disk
// await client.DownloadDirectoryAsync(@"C:\website\dailybackup\", @"/public_html/", FtpFolderSyncMode.Mirror);

// // delete a folder recursively
// await client.DeleteDirectoryAsync("/htdocs/extras/");

// // check if a file exists
// if (await client.FileExistsAsync("/htdocs/big2.txt")) { }

// // check if a folder exists
// if (await client.DirectoryExistsAsync("/htdocs/extras/")) { }

// // upload a file and retry 3 times before giving up
// client.RetryAttempts = 3;
// await client.UploadFileAsync(@"C:\MyVideo.mp4", "/htdocs/big.txt", FtpRemoteExists.Overwrite, false, FtpVerify.Retry);

// disconnect! good bye!
await client.DisconnectAsync();
