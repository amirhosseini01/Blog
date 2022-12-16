using Site.Data;
using Site.Models;
using Site.Repositories.Generics;

namespace Site.Features.Menu;
public class MenuRep : GenericRepository<Menu>, IMenuRep
{
    private readonly DataBaseContext _context;
    public MenuRep(DataBaseContext context) : base(context)
    {
        _context = context;
    }
}