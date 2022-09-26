using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ThorAdmin.Api.Base;
using ThorAdmin.Services;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Api.Controllers;

public class WordPressController : ApiControllerBase<WordPressController>
{
    readonly IWordPressService _wordPressService;

    public WordPressController(ILogger<WordPressController> logger, IOptions<Settings> settings, IWordPressService wordpressService) : base(logger, settings)
    {
        _wordPressService = wordpressService;
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> GetInstance(string id)
    {
        try
        {
            var result = await _wordPressService.GetInstance(id, Settings.RootDirectory);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }

    [HttpPost]
    [Route("[action]/{instanceName}")]
    public async Task<IActionResult> CreateInstance(string instanceName)
    {
        try
        {
            var result = await _wordPressService.CreateInstance(instanceName, Settings.RootDirectory, Settings.DbServer, Settings.DbUser, Settings.DbPassword, Settings.WordPressArchive);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }
    }

    [HttpDelete]
    [Route("[action]/{id}")]
    public async Task<IActionResult> DeleteInstance(string id)
    {
        try
        {
            var result = await _wordPressService.DeleteInstance(id, Settings.RootDirectory);
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
    public async Task<IActionResult> GetInstances()
    {
        try
        {
            var result = await _wordPressService.GetInstances(Settings.RootDirectory);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }
    }

}
