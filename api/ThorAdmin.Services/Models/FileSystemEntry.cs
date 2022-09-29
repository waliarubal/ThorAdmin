namespace ThorAdmin.Services.Models;

public class FileSystemEntry
{
    public bool IsDirectory { get; set; }

    public bool IsReadOnly { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public long Size { get; set; }
}
