using System.Diagnostics;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public class MachineInfoService: IMachineInfoService
{
    public IEnumerable<ProcessInfo> GetProcesses()
    {
        var procInfos = new List<ProcessInfo>();
        foreach(var proc in Process.GetProcesses())
        {
            var procInfo = new ProcessInfo
            {
                Id = proc.Id,
                Name = proc.ProcessName,
                Memory = proc.WorkingSet64
            };
            procInfos.Add(procInfo);
        }
        return procInfos;
    }
}
