using Microsoft.EntityFrameworkCore;
using Site.Configurations;
using Site.ViewModels;

namespace Site.Features.Blog;

public static class BlogService
{
    public static async Task<List<VmBlogClientList>> GetForIndex(this IQueryable<Blog> query, VmRequestPagination vm)
    {
        return await query.BaseConditions()
            .Select(x => new VmBlogClientList
            {
                Id = x.Id,
                Title = x.Title,
                MetaDescription = x.MetaDescription,
                ImgUrl = x.ImgUrl,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                PersianUpdateDate = x.UpdateDate.ToPersianDate(),
            })
            .OrderByDescending(x => x.Id).GetPagiantionQuery(vm).ToListAsync();
    }
    private static IQueryable<Blog> BaseConditions(this IQueryable<Blog> query)
    {
        return query.Where(x => !x.IsHidden);
    }
}