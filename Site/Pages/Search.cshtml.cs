using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Features.Blog;
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
    public async Task OnGet(string q = null, int? categoryId = null)
    {
        ViewData["SearchValue"] = q;
        Blogs = await _blogRep.GetQuery().SearchInBlogs(new VmRequestPagination(Take: 50), new VmBlogFilter()
        {
            Q = q,
            CategoryId = categoryId
        });
    }
}