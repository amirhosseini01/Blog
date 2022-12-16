using Microsoft.EntityFrameworkCore;
using Site.Configurations;
using Site.ViewModels;

namespace Site.Features.BlogCategory;

public static class CategoryService
{
    public static async Task<List<VmCategoryClientList>> GetForIndex(this IQueryable<Category> query, VmRequestPagination vm)
    {
        return await query.BaseConditions()
            .Select(x => new VmCategoryClientList
            {
                Id = x.Id,
                Title = x.Title,
                OrderView = x.OrderView,
                BlogCount = x.Blogs.Count(x => !x.IsHidden)
            })
            .OrderByDescending(x => x.OrderView).ThenByDescending(x => x.Id).GetPagiantionQuery(vm).ToListAsync();
    }
    private static IQueryable<Category> BaseConditions(this IQueryable<Category> query)
    {
        return query.Where(x => !x.IsHidden);
    }
}