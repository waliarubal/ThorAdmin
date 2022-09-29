using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public interface IFileSystemService
{
    FileSystemEntry GetParentEntry(string directory, Settings settings);

    IEnumerable<FileSystemEntry> GetEntries(string directory, Settings settings);
}
