using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public interface IMachineInfoService
{
    IEnumerable<ProcessInfo> GetProcesses();
}
