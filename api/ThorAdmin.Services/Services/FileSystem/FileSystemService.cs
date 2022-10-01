﻿using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public class FileSystemService : IFileSystemService
{
    public bool DeleteEntry(FileSystemEntry entry, Settings settings)
    {
        var path = Path.Join(settings.RootDirectory, entry.Path);
        if (entry.IsDirectory && Directory.Exists(path))
            Directory.Delete(path, true);
        else 
            File.Delete(path);

        return true;
    }

    public bool RenameEntry(FileSystemEntry entry, string newName, Settings settings)
    {
        var path = Path.Join(settings.RootDirectory, entry.Path);
        var newPath = Path.Join(path.Remove(path.Length - entry.Name.Length, entry.Name.Length), newName);
        if (entry.IsDirectory && Directory.Exists(path))
            Directory.Move(path, newPath);
        else
            File.Move(path, newPath);

        return true;
    }

    public FileSystemEntry GetParentEntry(string directory, Settings settings)
    {
        if (string.IsNullOrEmpty(directory))
            throw new Exception("Can not traverse beyond server's root directory.");

        var dirInfo = new DirectoryInfo(Path.Join(settings.RootDirectory, directory)).Parent;
        return new FileSystemEntry
        {
            IsDirectory = true,
            Name = dirInfo.Name,
            Path = dirInfo.FullName.Replace(settings.RootDirectory, string.Empty, StringComparison.OrdinalIgnoreCase),
            Created = dirInfo.CreationTimeUtc,
            Modified = dirInfo.LastWriteTimeUtc
        }; ;
    }

    public IEnumerable<FileSystemEntry> GetEntries(string directory, Settings settings)
    {
        var entries = new List<FileSystemEntry>();

        var directoryInfo = new DirectoryInfo(Path.Join(settings.RootDirectory, directory));
        foreach (var dirInfo in directoryInfo.GetDirectories())
        {
            var entry = new FileSystemEntry
            {
                IsDirectory = true,
                Name = dirInfo.Name,
                Path = dirInfo.FullName.Replace(settings.RootDirectory, string.Empty, StringComparison.OrdinalIgnoreCase),
                Created = dirInfo.CreationTimeUtc,
                Modified = dirInfo.LastWriteTimeUtc
            };
            entries.Add(entry);
        }
        foreach (var fileInfo in directoryInfo.GetFiles())
        {
            var entry = new FileSystemEntry
            {
                Name = fileInfo.Name,
                Path = fileInfo.FullName.Replace(settings.RootDirectory, string.Empty, StringComparison.OrdinalIgnoreCase),
                Created = fileInfo.CreationTimeUtc,
                Modified = fileInfo.LastWriteTimeUtc,
                Size = fileInfo.Length,
                IsReadOnly = fileInfo.IsReadOnly
            };
            entries.Add(entry);
        }

        return entries;
    }
}