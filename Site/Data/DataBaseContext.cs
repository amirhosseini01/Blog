using Microsoft.EntityFrameworkCore;
using Site.Features.Blog;
using Site.Features.BlogCategory;
using Site.Features.Menu;
using Site.Models;

namespace Site.Data;
public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Menu> Menus { get; set; }
}