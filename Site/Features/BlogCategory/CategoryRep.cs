using Site.Data;
using Site.Models;
using Site.Repositories.Generics;

namespace Site.Features.BlogCategory;
public class CategoryRep : GenericRepository<Category>, ICategoryRep
{
    private readonly DataBaseContext _context;
    public CategoryRep(DataBaseContext context) : base(context)
    {
        _context = context;
    }
}