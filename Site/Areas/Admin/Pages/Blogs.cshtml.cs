using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.ClaimHelper;
using Site.Configurations.DataTableJs;
using Site.Features.Blog;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;

[ClaimRequirement(claimType: nameof(BlogsModel), claimValue: ClaimStore.View)]
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
            OrderView =  x.OrderView,
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
    public async Task<JsonResult> OnGetChangeOrder(int id, int order)
    {
        if (id <= 0 || order <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _blogRep.GetById(id);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        entity.OrderView = order;
        _blogRep.Update(entity, User.GetLoggedInUserId<string>());
        var result = await _blogRep.Save();
        return new JsonResult(result);
    }
    public async Task<JsonResult> OnGetToggleStatus(int id)
    {
        if (id <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _blogRep.GetById(id);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        entity.IsHidden = !entity.IsHidden;
        _blogRep.Update(entity, User.GetLoggedInUserId<string>());
        var result = await _blogRep.Save();
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetDelete(int itemId)
    {
        if (itemId <= 0)
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));

        var entity = await _blogRep.GetById(itemId);
        if (entity is null)
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        _blogRep.Remove(entity);
        return new JsonResult(await _blogRep.Save());
    }
}