using Site.Data;
using Site.Models;
using Site.Repositories.Contracts;
using Site.Repositories.Generics;

namespace Site.Repositories.Implementations;
public class BlogRep : GenericRepository<Blog>, IBlogRep
{
    private readonly DataBaseContext _context;
    public BlogRep(DataBaseContext context) : base(context)
    {
        _context = context;
    }
}