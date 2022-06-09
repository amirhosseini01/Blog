using System.ComponentModel.DataAnnotations;
using Site.Models;

namespace Site.Features.Menu;
public class Menu : BaseEntity
{
    [Required]
    [StringLength(maximumLength:60)]
    public string Title { get; set; }
    [StringLength(maximumLength:60)]
    public string IconClassName { get; set; }
    public int? PId { get; set; }
    [Required]
    [StringLength(maximumLength:450)]
    public string Url { get; set; }
    public int? OrderView { get; set; }
    public bool IsHidden { get; set; }
}