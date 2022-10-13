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
            var cpuUsageTotal = 0D;
            try
            {
                var startTime = DateTime.UtcNow;
                var startCpuUsage = proc.TotalProcessorTime;

                await Task.Delay(10);

                proc.Refresh();
                var endTime = DateTime.UtcNow;
                var endCpuUSage = proc.TotalProcessorTime;

                var usedCpuMs = (endCpuUSage - startCpuUsage).TotalMilliseconds;
                var totaPassedMs = (endTime - startTime).TotalMilliseconds;
                cpuUsageTotal = usedCpuMs / (processorCount * totaPassedMs);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }

            var procInfo = new ProcessInfo
            {
                Id = proc.Id,
                Name = proc.ProcessName,
                Memory = proc.WorkingSet64,
                Cpu = cpuUsageTotal * 100
            };
            procInfos.Add(procInfo);
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
