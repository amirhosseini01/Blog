using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        Blogs = await _blogRep
            .GetQuery()
            .ConfigQuery()
            .GetBlogs()
            .AppendPagination(new VmRequestPagination(Take: 50))
            .ToListAsync();

        const int take = 4;
        RecommendedBlogs = await _blogRep
          .GetQuery()
          .ConfigQuery()
          .GetShortLink(BlogClientFilterType.Recommended)
          .AppendPagination(new VmRequestPagination(Take: take))
          .ToListAsync();

        LatestBlogs = await _blogRep.GetQuery()
        .ConfigQuery()
        .GetShortLink(BlogClientFilterType.Latest)
        .AppendPagination(new VmRequestPagination(Take: take))
        .ToListAsync();
        Categories = await _categoryRep.GetQuery().GetForIndex(new VmRequestPagination(Take: take));
    }
}
