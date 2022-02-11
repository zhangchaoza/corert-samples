using Renci.SshNet;

var connectionInfo = new ConnectionInfo(
    host: "127.0.0.1",
    port: 2222,
    username: "root",
    new PasswordAuthenticationMethod("root", "admin"));
using (var client = new SftpClient(connectionInfo))
{
    client.Connect();

    foreach (var f in client.ListDirectory("."))
    {
        Console.WriteLine(f.Name);
    }
}
