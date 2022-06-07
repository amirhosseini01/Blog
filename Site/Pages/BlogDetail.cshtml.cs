using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Features.Blog;
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
        public List<VmBlogClientShortLink> SimilarBlogs { get; set; }
        public List<VmBlogClientShortLink> RecommendedBlogs { get; set; }
        public async Task OnGet(int id)
        {
            Blog = await _blogRep.GetById(id);
            const int take = 4;
            RecommendedBlogs = await _blogRep.GetQuery().GetRecommended(new VmRequestPagination(Take: take));
            LatestBlogs = await _blogRep.GetQuery().GetLatest(new VmRequestPagination(Take: take));
            SimilarBlogs = await _blogRep.GetQuery().GetSimilars(Blog, new VmRequestPagination(Take: take));
            Categories = await _categoryRep.GetQuery().GetForIndex(new VmRequestPagination(Take: take));
        }
    }
}
