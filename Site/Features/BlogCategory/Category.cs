using System.Collections;
using System.ComponentModel.DataAnnotations;
using Site.Models;

namespace Site.Features.BlogCategory;

public class Category : BaseEntity
{
    [Required]
    [StringLength(maximumLength:60)]
    public string Title { get; set; }
    public int? OrderView { get; set; }
    public bool IsHidden { get; set; }

    public virtual ICollection<Site.Features.Blog.Blog> Blogs { get; set; }
}