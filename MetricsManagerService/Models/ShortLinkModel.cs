namespace MetricsManagerService.Models;

public class ShortLinkModel
{
    public int Id { get; set; }
    public DateTime DateCreate { get; set; }
    public string Name { get; set; }
    public string ShortLink { get; set; }
    public string Url { get; set; }
}
