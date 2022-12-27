namespace HotSpotWeb.Applications.Dtos;

public class GetApplicationListInput
{
    public bool IncludeUnpublished { get; set; }
    public bool IncludeDeleted { get; set; }
    public string Filter { get; set; }
    public string Sorting { get; set; }
    public int MaxResultCount { get; set; }
}