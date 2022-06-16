using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Features.Blog;

namespace Site.Pages
{
    public class SitemapModel : PageModel
    {
        private readonly IBlogRep _blogRep;
        private readonly ICategoryRep _categoryRep;
        public SitemapModel(ICategoryRep categoryRep, IBlogRep blogRep)
        {
            _categoryRep = categoryRep;
            _blogRep = blogRep;
        }
        public string SiteMapXML { get; set; }
        public void OnGet()
        {
            // var blogs = await _blogRep.GetQuery().GetForIndex(new VmRequestPagination(Take: 50));
            // StringBuilder sb = new();


        }
    }
}
