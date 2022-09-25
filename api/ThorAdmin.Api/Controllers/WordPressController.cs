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
    [Route("[action]/{instanceName}")]
    public async Task<IActionResult> GetInstance(string instanceName)
    {
        try
        {
            var result = await _wordPressService.GetInstance(instanceName, Settings.RootDirectory);
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
    public IActionResult CreateInstance([FromBody] WordPressInstance instance)
    {
        try
        {
            var result = _wordPressService.CreateInstance(instance.Name, Settings.RootDirectory);
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
    public IActionResult DeleteInstance([FromBody] WordPressInstance instance)
    {
        try
        {
            var result = _wordPressService.DeleteInstance(instance.Name, Settings.RootDirectory);
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
