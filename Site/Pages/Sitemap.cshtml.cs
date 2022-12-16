using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Features.Blog;

namespace Site.Pages
{
    public class SitemapModel : PageModel
    {
        private readonly IBlogRep _blogRep;
        public SitemapModel(IBlogRep blogRep)
        {
            _blogRep = blogRep;
        }
        public IReadOnlyList<Blog> Blogs { get; set; }
        public async Task OnGet()
        {
            Blogs = await _blogRep
            .GetQuery()
            .ConfigQuery()
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        }
    }
}
