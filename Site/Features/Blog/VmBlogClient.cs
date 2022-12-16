using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Features.Blog;

public class VmBlogClientList
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public string Title { get; set; }
    public string MetaDescription { get; set; }
    public string ImgUrl { get; set; }
    public string ImgAlt { get; set; }
    public string ImgTitle { get; set; }
    public string PersianUpdateDate { get; set; }
}

public class VmBlogClientShortLink
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public string Title { get; set; }
}

public struct VmBlogFilter
{
    public string Q { get; set; }
    public int? CategoryId { get; set; }
}