using Microsoft.AspNetCore.Http;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public interface IFileSystemService
{
    FileSystemEntry GetParentEntry(string directory, Settings settings);

    IEnumerable<FileSystemEntry> GetEntries(string directory, Settings settings);

    bool DeleteEntry(FileSystemEntry entry, Settings settings);

    bool RenameEntry(FileSystemEntry entry, string newName, Settings settings);

    Task<bool> CreateEntry(FileSystemEntry entry, Settings settings);

    Task<byte[]> GetContents(FileSystemEntry entry, Settings settings);

    Task<bool> WriteContents(FileSystemEntry entry, IFormFile file, Settings settings);
}
