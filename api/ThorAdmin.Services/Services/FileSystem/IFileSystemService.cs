using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public interface IFileSystemService
{
    FileSystemEntry GetParentEntry(string directory, Settings settings);

    IEnumerable<FileSystemEntry> GetEntries(string directory, Settings settings);

    bool DeleteEntry(FileSystemEntry entry, Settings settings);

    bool RenameEntry(FileSystemEntry entry, string newName, Settings settings);

    Task<byte[]> GetContents(FileSystemEntry entry, Settings settings);
}
