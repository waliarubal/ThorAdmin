using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public interface IMachineInfoService
{
    Task<IEnumerable<ProcessInfo>> GetProcesses();

    bool KillProcess(int id);
}
