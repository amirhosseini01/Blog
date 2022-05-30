using Site.Data;
using Site.Models;
using Site.Repositories.Contracts;
using Site.Repositories.Generics;

namespace Site.Repositories.Implementations;
public class MenuRep : GenericRepository<Menu>, IMenuRep
{
    private readonly DataBaseContext _context;
    public MenuRep(DataBaseContext context) : base(context)
    {
        _context = context;
    }
}