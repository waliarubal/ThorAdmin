using System.ComponentModel;
using System.Diagnostics;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Services;

public class MachineInfoService : IMachineInfoService
{
    public async Task<IEnumerable<ProcessInfo>> GetProcesses()
    {
        var processorCount = Environment.ProcessorCount;

        var procInfos = new List<ProcessInfo>();
        foreach (var proc in Process.GetProcesses())
        {
            try
            {
                var name = proc.MainModule.FileVersionInfo.FileDescription;
                if (string.IsNullOrEmpty(name))
                    name = proc.ProcessName;

                // skip processes with no name
                if (string.IsNullOrEmpty(name))
                    continue;

                var startTime = DateTime.UtcNow;
                var startCpuUsage = proc.TotalProcessorTime;

                await Task.Delay(10);

                proc.Refresh();
                var endTime = DateTime.UtcNow;
                var endCpuUSage = proc.TotalProcessorTime;

                var usedCpuMs = (endCpuUSage - startCpuUsage).TotalMilliseconds;
                var totaPassedMs = (endTime - startTime).TotalMilliseconds;
                var cpuUsageTotal = usedCpuMs / (processorCount * totaPassedMs);

                var procInfo = new ProcessInfo
                {
                    Id = proc.Id,
                    Name = name,
                    Memory = proc.WorkingSet64,
                    Cpu = cpuUsageTotal * 100
                };
                procInfos.Add(procInfo);
            }
            catch (Win32Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
        return procInfos;
    }

    public bool KillProcess(int id)
    {
        var process = Process.GetProcessById(id);
        process.Kill(true);
        return true;
    }
}
