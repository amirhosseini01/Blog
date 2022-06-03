using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Repositories.Contracts;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;
public class BlogDetailModel : PageModel
{
    private readonly IBlogRep _blogRep;
    public BlogDetailModel(IBlogRep blogRep)
    {
        _blogRep = blogRep;
    }

    //properties
    public int? Id { get; set; }
    public VmBlogDetail VmInput { get; set; }

    //methods
    public void OnGet(int? id)
    {
        Id = id;
    }
}