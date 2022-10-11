using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ThorAdmin.Api.Base;
using ThorAdmin.Services;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Api.Controllers;

public class MachineInfoController : ApiControllerBase<MachineInfoController>
{
    readonly IMachineInfoService _machineInfoService;

    public MachineInfoController(ILogger<MachineInfoController> logger, IOptions<Settings> settings, IMachineInfoService machineInfoService) : base(logger, settings)
    {
        _machineInfoService = machineInfoService;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetProcesses()
    {
        try
        {
            var result = _machineInfoService.GetProcesses();
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
    public IActionResult KillProcess(int id)
    {
        try
        {
            var result = _machineInfoService.KillProcess(id);
            return SendOk(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SendError(ex.Message);
        }

    }
}
