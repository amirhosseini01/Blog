using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Features.Blog;
using Site.ViewModels;

namespace Site.Pages;

public class IndexModel : PageModel
{
    private readonly IBlogRep _blogRep;

    public IndexModel(IBlogRep blogRep)
    {
        _blogRep = blogRep;
    }

    public List<VmBlogClientList> Blogs { get; set; }
    public async Task OnGet()
    {
        Blogs = await _blogRep.GetQuery().GetForIndex(new VmRequestPagination(Take: 50));
    }
}
