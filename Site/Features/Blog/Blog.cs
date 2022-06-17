using System.ComponentModel.DataAnnotations;
using Site.Features.BlogCategory;
using Site.Models;

namespace Site.Features.Blog;

public class Blog : BaseEntity
{
    public int CategoryId { get; set; }

    [Required]
    [StringLength(maximumLength: 60)]
    public string Title { get; set; }

    [Required]
    [StringLength(maximumLength: 160)]
    public string MetaDescription { get; set; }

    [Required]
    public string Description { get; set; }

    public bool IsHidden { get; set; }

    [Required]
    [StringLength(maximumLength: 450)]
    public string ImgUrl { get; set; }

    [Required]
    [StringLength(maximumLength: 60)]
    public string ImgAlt { get; set; }

    [Required]
    [StringLength(maximumLength: 60)]
    public string ImgTitle { get; set; }

    [StringLength(maximumLength: 450)]
    public string CanonicalUrl { get; set; }

    [Required]
    [StringLength(maximumLength: 500)]
    public string KeyWords { get; set; }
    public int? OrderView { get; set; }

    public virtual Category Category { get; set; }
}