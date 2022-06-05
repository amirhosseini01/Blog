using Site.Data;
using Site.Models;
using Site.Repositories.Contracts;
using Site.Repositories.Generics;

namespace Site.Features.Blog;
public class CategoryRep : GenericRepository<Category>, ICategoryRep
{
    private readonly DataBaseContext _context;
    public CategoryRep(DataBaseContext context) : base(context)
    {
        _context = context;
    }
}