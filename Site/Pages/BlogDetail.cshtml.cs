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

namespace Site.Pages
{
    public class BlogDetailModel : PageModel
    {
        private readonly IBlogRep _blogRep;
        private readonly ICategoryRep _categoryRep;
        public BlogDetailModel(IBlogRep blogRep, ICategoryRep categoryRep)
        {
            _blogRep = blogRep;
            _categoryRep = categoryRep;
        }

        public Blog Blog { get; set; }
        public List<VmCategoryClientList> Categories { get; set; }
        public List<VmBlogClientShortLink> LatestBlogs { get; set; }
        public List<VmBlogClientList> SimilarBlogs { get; set; }
        public List<VmBlogClientShortLink> RecommendedBlogs { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            Blog = await _blogRep.GetById(id);
            if (Blog is null || Blog.IsHidden)
            {
                return NotFound("Blog not found");
            }

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

            SimilarBlogs = await _blogRep
            .GetQuery()
            .ConfigQuery()
            .GetBlogs(new VmBlogFilter() {CategoryId = Blog.CategoryId, Q = Blog.Title})
            .GetPagiantionQuery(new VmRequestPagination(Take: take))
            .ToListAsync();
            Categories = await _categoryRep.GetQuery().GetForIndex(new VmRequestPagination(Take: take));

            return Page();
        }
    }
}
