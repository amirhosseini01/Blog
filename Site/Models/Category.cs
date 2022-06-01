using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Site.Models;

public class Category : BaseEntity
{
    [Required]
    [StringLength(maximumLength:60)]
    public string Title { get; set; }
    public int? OrderView { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }
}