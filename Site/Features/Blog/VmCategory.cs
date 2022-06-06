using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Features.Blog;

public class VmCategoryClientList
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public int BlogCount { get; set; }
    public string Title { get; set; }
}