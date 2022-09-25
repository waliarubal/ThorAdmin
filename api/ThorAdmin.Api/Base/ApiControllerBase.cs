using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Api.Base;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public abstract class ApiControllerBase<TController>: ControllerBase where TController : ControllerBase
{
    readonly ILogger<TController> _logger;
    readonly Settings _settings;

    protected ApiControllerBase(ILogger<TController> logger, IOptions<Settings> settings)
    {
        _logger = logger;
        _settings = settings.Value;
    } 

    #region properties

    protected ILogger<TController> Logger => _logger;

    protected Settings Settings => _settings;

    #endregion

    protected IActionResult SendOk<TData>(TData data)
    {
        var result = new ApiResponse<TData>(data);
        return Ok(result);
    }

    protected IActionResult SendError(string error)
    {
        var result = new ApiResponse<byte>(byte.MinValue, error);
        return Ok(result);
    }
}
