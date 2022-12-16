using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.ClaimHelper;
using Site.Configurations.DataTableJs;
using Site.Features.BlogCategory;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;

[ClaimRequirement(claimType: nameof(CategoriesModel), claimValue: ClaimStore.View)]
public class CategoriesModel : PageModel
{
    private readonly ICategoryRep _categoryRep;
    public CategoriesModel(ICategoryRep categoryRep)
    {
        _categoryRep = categoryRep;
    }

    public int? PId { get; set; }
    public VmCategoryInput VmInput { get; set; }
    public void OnGet(int? pid)
    {
        PId = pid;
    }

    public async Task<JsonResult> OnGetGetById(int itemId)
    {
        var entity = await _categoryRep.GetById(itemId);
        return new JsonResult(entity);
    }
    public JsonResult OnPostList()
    {
        Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);

        var query = _categoryRep.GetQuery();

        if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
        {
            query = query.Where(x =>
                                     x.Id.ToString().Contains(filtersFromRequest.SearchValue) ||
                                     x.Title.Contains(filtersFromRequest.SearchValue));
        }

        var result = query.Select(x => new VmCategoryList()
        {
            Id = x.Id,
            OrderView = x.OrderView,
            Title = x.Title,
            IsHidden = x.IsHidden,
            CreateDate = x.CreateDate,
            UpdateDate = x.UpdateDate,
            PersianCreateDate = x.CreateDate.ToPersianDate(),
            PersianUpdateDate = x.UpdateDate.ToPersianDate()
        }).ToDataTableJs(filtersFromRequest);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnPostAdd(VmCategoryInput VmInput)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        string currentUserId = User.GetLoggedInUserId<string>();
        Category entity = new()
        {
            Title = VmInput.Title,
            IsHidden = false,
        };

        await _categoryRep.Add(entity, currentUserId);
        var result = await _categoryRep.Save();
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnPostEdit(VmCategoryInput VmInput)
    {
        if (!ModelState.IsValid || VmInput.Id is null || VmInput.Id <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _categoryRep.GetById(VmInput.Id.Value);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        string currentUserId = User.GetLoggedInUserId<string>();
        entity.Title = VmInput.Title;

        _categoryRep.Update(entity, currentUserId);
        var result = await _categoryRep.Save();
        return new JsonResult(result);
    }
    public async Task<JsonResult> OnGetChangeOrder(int id, int order)
    {
        if (id <= 0 || order <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _categoryRep.GetById(id);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        entity.OrderView = order;
        _categoryRep.Update(entity, User.GetLoggedInUserId<string>());
        var result = await _categoryRep.Save();
        return new JsonResult(result);
    }
    public async Task<JsonResult> OnGetToggleStatus(int id)
    {
        if (id <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _categoryRep.GetById(id);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        entity.IsHidden = !entity.IsHidden;
        _categoryRep.Update(entity, User.GetLoggedInUserId<string>());
        var result = await _categoryRep.Save();
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetDelete(int itemId)
    {
        if (itemId <= 0)
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));

        var entity = await _categoryRep.GetById(itemId);
        if (entity is null)
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        _categoryRep.Remove(entity);
        return new JsonResult(await _categoryRep.Save());
    }

}