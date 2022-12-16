using Microsoft.EntityFrameworkCore;
using Site.Configurations;
using Site.ViewModels;

namespace Site.Features.Blog;

public static class BlogClientService
{
    public static IQueryable<Blog> ConfigQuery(this IQueryable<Blog> query)
    {
        return query.AsNoTracking().Where(x => !x.IsHidden && !x.Category.IsHidden);
    }
    public static IQueryable<VmBlogClientList> GetBlogs(this IQueryable<Blog> query)
    {
        return query.SelectBlogs();
    }
    public static IQueryable<VmBlogClientShortLink> GetShortLink(this IQueryable<Blog> query, BlogClientFilterType filterType)
    {
        var selectQuery = query.Select(x => new VmBlogClientShortLink
        {
            Id = x.Id,
            Title = x.Title,
            OrderView = x.OrderView,
        });

        return filterType switch
        {
            BlogClientFilterType.Recommended => selectQuery.OrderByDescending(x => x.OrderView),
            BlogClientFilterType.Latest => selectQuery.OrderByDescending(x => x.Id),
            _ => selectQuery,
        };
    }
    public static IQueryable<VmBlogClientList> GetBlogs(this IQueryable<Blog> query, VmBlogFilter vmFilter)
    {
        if (!string.IsNullOrWhiteSpace(vmFilter.Q))
        {
            query = query.Where(x =>
                x.Description.Contains(vmFilter.Q) ||
                x.MetaDescription.Contains(vmFilter.Q) ||
                x.Title.Contains(vmFilter.Q) ||
                x.KeyWords.Contains(vmFilter.Q) ||
                x.Category.Title.Contains(vmFilter.Q) ||
                x.ImgAlt.Contains(vmFilter.Q) ||
                x.ImgTitle.Contains(vmFilter.Q)
            );
        }

        if (vmFilter.CategoryId is not null && vmFilter.CategoryId > 0)
        {
            query = query.Where(x => x.CategoryId == vmFilter.CategoryId);
        }
        return query.SelectBlogs();
    }
    private static IQueryable<VmBlogClientList> SelectBlogs(this IQueryable<Blog> query)
    {
        return query
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
                    });
    }
}