namespace Site.Models;
public class Menu : BaseEntity
{
    public string Title { get; set; }
    public int? PId { get; set; }
    public string Url { get; set; }
    public int? OrderView { get; set; }
    public bool IsHidden { get; set; }
}