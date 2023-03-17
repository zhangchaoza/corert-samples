using SharpCompress.Archives.GZip;
using SharpCompress.Archives.Tar;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;

{
    var tarbz2Path = "data.txt.tar";
    using Stream stream = File.OpenWrite(tarbz2Path);
    using var writer = WriterFactory.Open(stream, ArchiveType.Tar, CompressionType.None);
    writer.Write("data.txt", "data.txt");
}

{
    var tarbz2Path = "data.txt.tar.gz";
    using Stream stream = File.OpenWrite(tarbz2Path);
    using var writer = WriterFactory.Open(stream, ArchiveType.Tar, CompressionType.GZip);
    writer.Write("a/data.txt", "data.txt");
    writer.Write("data.txt", "data.txt");
}

{
    using Stream stream = File.OpenRead("data.txt.tar.gz");
    using var reader = ReaderFactory.Open(stream);
    while (reader.MoveToNextEntry())
    {
        if (!reader.Entry.IsDirectory)
        {
            Console.WriteLine(reader.Entry.Key);
            reader.WriteEntryToFile(reader.Entry.Key + "_1");
        }
    }
}

{
    var tarbz2Path = "data2.tar.gz";
    using Stream stream = File.Open(tarbz2Path, FileMode.Create);
    using var tar = TarArchive.Open(stream);
    // tar.AddAllFromDirectory("a");
    using var a = File.OpenRead("data.txt");
    tar.AddEntry("data.txt", a, closeStream: false, a.Length);
    tar.AddEntry("a/data.txt", a, closeStream: false, a.Length);
    tar.SaveTo(stream, new WriterOptions(CompressionType.GZip) { LeaveStreamOpen = false });
}