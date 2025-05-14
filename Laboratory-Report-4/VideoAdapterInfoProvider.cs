using System.Management;

namespace Laboratory_Report_4;

public class VideoAdapterInfo
{
    public string? Model { get; set; }
    public double? MemoryGb { get; set; }
    public string? Resolution { get; set; }
    public string? RefreshRateHz { get; set; }
    public string? DeviceId { get; set; }
}

public class VideoAdapterInfoProvider : WmiInfoProvider<VideoAdapterInfo>
{
    private const double ConvertToGigabyte = 1024*1024*1024;
        
    protected override string WmiClassName => "Win32_VideoController";

    protected override VideoAdapterInfo MapFrom(ManagementObject obj)
    {
        var pnpId = obj["PNPDeviceID"]?.ToString();
        return new VideoAdapterInfo
        {
            Model = obj["Name"]?.ToString(),
            MemoryGb =
                obj["AdapterRAM"] != null ? Math.Round(Convert.ToUInt64(obj["AdapterRAM"]) / ConvertToGigabyte, 2) : null,
            Resolution = $"{obj["CurrentHorizontalResolution"]}x{obj["CurrentVerticalResolution"]}",
            RefreshRateHz = obj["CurrentRefreshRate"]?.ToString(),
            DeviceId = pnpId?[..pnpId.IndexOf("SUBSYS", StringComparison.OrdinalIgnoreCase)],
        };
    }
}