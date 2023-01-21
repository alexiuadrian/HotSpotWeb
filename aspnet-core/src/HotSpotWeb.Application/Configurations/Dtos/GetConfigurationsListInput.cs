namespace HotSpotWeb.Configurations.Dtos;

public class GetConfigurationsListInput
{
    public string Filter { get; set; }
    public string Sorting { get; set; }
    public int MaxResultCount { get; set; }
}