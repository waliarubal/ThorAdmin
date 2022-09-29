using Microsoft.AspNetCore.Mvc;
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
}
