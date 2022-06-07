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
                OrderView = x.OrderView,
                MetaDescription = x.MetaDescription,
                ImgUrl = x.ImgUrl,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                PersianUpdateDate = x.UpdateDate.ToPersianDate(),
            })
            .OrderByDescending(x => x.OrderView).ThenByDescending(x => x.Id).GetPagiantionQuery(vm).ToListAsync();
    }
    public static async Task<List<VmBlogClientShortLink>> GetLatest(this IQueryable<Blog> query, VmRequestPagination vm)
    {
        return await query.BaseConditions()
            .Select(x => new VmBlogClientShortLink
            {
                Id = x.Id,
                Title = x.Title,
                OrderView = x.OrderView,
            })
            .OrderByDescending(x => x.Id).GetPagiantionQuery(vm).ToListAsync();
    }

    public static async Task<List<VmBlogClientShortLink>> GetSimilars(this IQueryable<Blog> query, Blog currentBlog, VmRequestPagination vm)
    {
        return await query.BaseConditions()
        .Where(x =>
        x.Description.Contains(currentBlog.Title) ||
        x.MetaDescription.Contains(currentBlog.Title) ||
        x.Title.Contains(currentBlog.Title) ||
        x.KeyWords.Contains(currentBlog.Title)
        )
            .Select(x => new VmBlogClientShortLink
            {
                Id = x.Id,
                Title = x.Title,
                OrderView = x.OrderView,
            })
            .OrderByDescending(x => x.Id).GetPagiantionQuery(vm).ToListAsync();
    }
    public static async Task<List<VmBlogClientShortLink>> GetRecommended(this IQueryable<Blog> query, VmRequestPagination vm)
    {
        return await query.BaseConditions()
            .Select(x => new VmBlogClientShortLink
            {
                Id = x.Id,
                Title = x.Title,
                OrderView = x.OrderView,
            })
            .OrderByDescending(x => x.OrderView).GetPagiantionQuery(vm).ToListAsync();
    }
    private static IQueryable<Blog> BaseConditions(this IQueryable<Blog> query)
    {
        return query.Where(x => !x.IsHidden && !x.Category.IsHidden);
    }
}