using Microsoft.EntityFrameworkCore;
using Site.Models;

namespace Site.Data;
public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public DbSet<Menu> Menus { get; set; }
}