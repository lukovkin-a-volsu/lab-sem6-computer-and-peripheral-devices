using System.Diagnostics;
using System.Management;

namespace Laboratory_Report_4;

public abstract class WmiInfoProvider<T>
{
    protected abstract string WmiClassName { get; }
    protected abstract T MapFrom(ManagementObject obj);

    public List<T> GetInfo()
    {
        var result = new List<T>();
        try
        {
            using var searcher = new ManagementObjectSearcher($"SELECT * FROM {WmiClassName}");
            foreach (var managementBaseObject in searcher.Get())
            {
                var managementObject = (ManagementObject)managementBaseObject;
                result.Add(MapFrom(managementObject));
            }
        }
        catch
        {
            Debug.Print("Error getting info");
        }
        return result;
    }
}