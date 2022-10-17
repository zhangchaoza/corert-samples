using System.Net;
using FluentFTP;

var token = new CancellationToken();

using var conn = new AsyncFtpClient();
conn.Host = Dns.GetHostName();
conn.Port = 10021;
conn.Credentials = new NetworkCredential("zc", "zc123");
conn.Config.EncryptionMode = FtpEncryptionMode.Auto;
conn.Config.ValidateAnyCertificate = true;

await conn.AutoConnect(token);

// get a list of files and directories in the "/" folder
foreach (FtpListItem item in await conn.GetListing("/"))
{
    Console.WriteLine(item);
}

var tempFile = Path.GetTempFileName();
await File.WriteAllTextAsync(tempFile, "hello world");

// upload a file
await conn.UploadFile(tempFile, "/file");

// move the uploaded file
await conn.MoveFile("/file", "/file2");

var tempFile2 = Path.GetTempFileName();
// download the file again
await conn.DownloadFile(tempFile2, "/file2");

// compare the downloaded file with the server
var e = await conn.CompareFile(tempFile2, "/file2");
Console.WriteLine("File equal:{0}", e);

// delete file
await conn.DeleteFile("/file2");

await conn.Disconnect();
