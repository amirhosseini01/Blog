using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Features.Blog;
using Site.ViewModels;

namespace Site.Pages;

public class IndexModel : PageModel
{
    private readonly IBlogRep _blogRep;
    private readonly ICategoryRep _categoryRep;

    public IndexModel(IBlogRep blogRep, ICategoryRep categoryRep)
    {
        _blogRep = blogRep;
        _categoryRep = categoryRep;
    }

    public List<VmBlogClientList> Blogs { get; set; }
    public List<VmBlogClientShortLink> LatestBlogs { get; set; }
    public List<VmBlogClientShortLink> RecommendedBlogs { get; set; }
    public List<VmCategoryClientList> Categories { get; set; }
    public async Task OnGet()
    {
        Blogs = await _blogRep.GetQuery().GetForIndex(new VmRequestPagination(Take: 50));
        const int take = 4;
        RecommendedBlogs = await _blogRep.GetQuery().GetRecommended(new VmRequestPagination(Take: take));
        LatestBlogs = await _blogRep.GetQuery().GetLatest(new VmRequestPagination(Take: take));
        Categories = await _categoryRep.GetQuery().GetForIndex(new VmRequestPagination(Take: take));
    }
}
