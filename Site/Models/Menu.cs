using System.ComponentModel.DataAnnotations;

namespace Site.Models;
public class Menu : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string IconClassName { get; set; }
    public int? PId { get; set; }
    [Required]
    public string Url { get; set; }
    public int? OrderView { get; set; }
    public bool IsHidden { get; set; }
}