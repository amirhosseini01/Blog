using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Configurations;
using Site.Features.Blog;
using Site.Features.BlogCategory;
using Site.ViewModels;

namespace Site.Pages;
public class SearchModel : PageModel
{
    private readonly IBlogRep _blogRep;
    private readonly ICategoryRep _categoryRep;
    public SearchModel(ICategoryRep categoryRep, IBlogRep blogRep)
    {
        _categoryRep = categoryRep;
        _blogRep = blogRep;
    }
    public List<VmBlogClientList> Blogs { get; set; }
    public List<VmBlogClientShortLink> LatestBlogs { get; set; }
    public List<VmBlogClientShortLink> RecommendedBlogs { get; set; }
    public List<VmCategoryClientList> Categories { get; set; }
    public async Task OnGet(string q = null, int? categoryId = null)
    {
        ViewData["SearchValue"] = q;
        Blogs = await _blogRep
        .GetQuery()
        .ConfigQuery()
        .GetBlogs(new VmBlogFilter()
        {
            Q = q,
            CategoryId = categoryId
        })
        .GetPagiantionQuery(new VmRequestPagination(Take: 50))
        .ToListAsync();

        const int take = 4;
        RecommendedBlogs = await _blogRep
           .GetQuery()
           .ConfigQuery()
           .GetShortLink(BlogClientFilterType.Recommended)
           .GetPagiantionQuery(new VmRequestPagination(Take: take))
           .ToListAsync();

        LatestBlogs = await _blogRep.GetQuery()
        .ConfigQuery()
        .GetShortLink(BlogClientFilterType.Latest)
        .GetPagiantionQuery(new VmRequestPagination(Take: take))
        .ToListAsync();
        Categories = await _categoryRep.GetQuery().GetForIndex(new VmRequestPagination(Take: take));
    }
}