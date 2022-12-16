using Site.Data;
using Site.Models;
using Site.Repositories.Generics;

namespace Site.Features.Blog;
public class BlogRep : GenericRepository<Blog>, IBlogRep
{
    private readonly DataBaseContext _context;
    public BlogRep(DataBaseContext context) : base(context)
    {
        _context = context;
    }
}