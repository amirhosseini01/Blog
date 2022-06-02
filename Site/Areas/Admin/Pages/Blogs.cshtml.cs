using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.DataTableJs;
using Site.Repositories.Contracts;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;
public class BlogsModel : PageModel
{
    private readonly IBlogRep _blogRep;
    public BlogsModel(IBlogRep blogRep)
    {
        _blogRep = blogRep;
    }
    public void OnGet()
    {

    }
    public JsonResult OnPostList()
    {
        Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);

        var query = _blogRep.GetQuery();

        if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
        {
            query = query.Where(x =>
                                     x.Id.ToString().Contains(filtersFromRequest.SearchValue) ||
                                     x.Title.Contains(filtersFromRequest.SearchValue) ||
                                     x.Description.Contains(filtersFromRequest.SearchValue) ||
                                     x.MetaDescription.Contains(filtersFromRequest.SearchValue));
        }

        var result = query.Select(x => new VmBlogList()
        {
            Id = x.Id,
            Title = x.Title,
            CategoryTitle = x.Category.Title,
            CategoryId = x.CategoryId,
            IsHidden = x.IsHidden,
            CreateDate = x.CreateDate,
            UpdateDate = x.UpdateDate,
            PersianCreateDate = x.CreateDate.ToPersianDate(),
            PersianUpdateDate = x.UpdateDate.ToPersianDate()
        }).ToDataTableJs(filtersFromRequest);
        return new JsonResult(result);
    }
}