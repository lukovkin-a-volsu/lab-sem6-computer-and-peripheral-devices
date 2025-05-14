using System.Management;

namespace Laboratory_Report_4;

public class ProcessorInfo
{
    public string? Model { get; set; }
    public string? Cores { get; set; }
    public double? MaxFrequencyGHz { get; set; }
    public string? L2CacheKb { get; set; }
    public string? L3CacheKb { get; set; }
}

public class ProcessorInfoProvider : WmiInfoProvider<ProcessorInfo>
{
    private const double ConvertToGigaHertz = 1000;

    protected override string WmiClassName => "Win32_Processor";

    protected override ProcessorInfo MapFrom(ManagementObject obj)
    {
        return new ProcessorInfo
        {
            Model = obj["Name"]?.ToString(),
            Cores = obj["NumberOfCores"]?.ToString(),
            MaxFrequencyGHz = obj["MaxClockSpeed"] != null
                ? Math.Round(Convert.ToDouble(obj["MaxClockSpeed"]) / ConvertToGigaHertz, 1)
                : null,
            L2CacheKb = obj["L2CacheSize"]?.ToString(),
            L3CacheKb = obj["L3CacheSize"]?.ToString()
        };
    }
}