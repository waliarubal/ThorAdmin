using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using ThorAdmin.Api.Base;
using ThorAdmin.Services;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Api.Controllers;

public class FileSystemController : ApiControllerBase<FileSystemController>
{
    readonly IFileSystemService _fileSystemService;

    public FileSystemController(ILogger<FileSystemController> logger, IOptions<Settings> settings, IFileSystemService fileSystemService) : base(logger, settings)
    {
        _fileSystemService = fileSystemService;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetParentEntry([FromQuery] string directory)
    {
        try
        {
            var result = _fileSystemService.GetParentEntry(directory, Settings);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetEntries([FromQuery] string directory)
    {
        try
        {
            var result = _fileSystemService.GetEntries(directory, Settings);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }

    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteEntry([FromBody] FileSystemEntry entry)
    {
        try
        {
            var result = _fileSystemService.DeleteEntry(entry, Settings);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }

    [HttpPatch]
    [Route("[action]")]
    public IActionResult RenameEntry([FromBody] FileSystemEntry entry, [FromQuery] string newName)
    {
        try
        {
            var result = _fileSystemService.RenameEntry(entry, newName, Settings);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateEntry([FromBody] FileSystemEntry entry)
    {
        try
        {
            var result = await _fileSystemService.CreateEntry(entry, Settings);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> DownloadEntry([FromBody] FileSystemEntry entry)
    {
        try
        {
            var bytes = await _fileSystemService.GetContents(entry, Settings);
            return File(bytes, "application/octet-stream", entry.Name);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }
    }
}
