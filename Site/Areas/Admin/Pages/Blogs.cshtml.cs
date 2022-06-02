using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Repositories.Contracts;

namespace Site.Areas_Admin_Pages;
public class BlogsModel : PageModel
{
    private readonly IBlogRep _blogRep;
    public BlogsModel(IBlogRep blogRep)
    {
        _blogRep = blogRep;
    }
    public void OnGet()
    {

    }
}