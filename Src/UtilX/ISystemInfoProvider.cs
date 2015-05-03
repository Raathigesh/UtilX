using System;
using System.Diagnostics;

namespace UtilX
{
    /// <summary>
    /// Provides system information.
    /// </summary>
    interface ISystemInfoProvider
    {
        double GetRamUsuage();
        double GetCpuUsage();
    }

    /// <summary>
    /// Provides system information.
    /// </summary>
    class SystemInfoProvider : ISystemInfoProvider
    {
        private readonly PerformanceCounter _cpuCounter;

        public SystemInfoProvider()
        {
            _cpuCounter = new PerformanceCounter();

            _cpuCounter.CategoryName = "Processor";
            _cpuCounter.CounterName = "% Processor Time";
            _cpuCounter.InstanceName = "_Total";
        }


        public double GetRamUsuage()
        {
            Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
            double percentFree = ((double)phav / (double)tot) * 100;
            return (100 - percentFree);
        }

        public double GetCpuUsage()
        {
            return _cpuCounter.NextValue();
        }
    }
}
