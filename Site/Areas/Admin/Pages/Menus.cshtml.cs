using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.ClaimHelper;
using Site.Configurations.DataTableJs;
using Site.Features.Menu;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;

[ClaimRequirement(claimType: nameof(BlogsModel), claimValue: ClaimStore.View)]
public class MenusModel : PageModel
{
    private readonly IMenuRep _menuRep;
    public MenusModel(IMenuRep menuRep)
    {
        _menuRep = menuRep;
    }

    public int? PId { get; set; }
    public void OnGet(int? pid)
    {
        PId = pid;
    }
    public JsonResult OnPostList()
    {
        Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);
        
         int? pid;
        if(!int.TryParse(Request.Form["pid"].FirstOrDefault(), out int convertedPid))
        {
            pid = null;
        }
        else
        {
            pid = convertedPid;
        }

        var query = _menuRep.GetQuery();

        if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
        {
            query = query.Where(x =>
                                     x.Id.ToString().Contains(filtersFromRequest.SearchValue) ||
                                     x.Title.Contains(filtersFromRequest.SearchValue) ||
                                     x.Url.Contains(filtersFromRequest.SearchValue));
        }

        var result = query.Where(x=>x.PId == pid).Select(x => new VmMenuList()
        {
            Id = x.Id,
            Url = x.Url,
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
    public async Task<JsonResult> OnGetChangeOrder(int id, int order)
    {
        if (id <= 0 || order <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _menuRep.GetById(id);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        entity.OrderView = order;
        _menuRep.Update(entity, User.GetLoggedInUserId<string>());
        var result = await _menuRep.Save();
        return new JsonResult(result);
    }
    public async Task<JsonResult> OnGetToggleStatus(int id)
    {
        if (id <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _menuRep.GetById(id);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        entity.IsHidden = !entity.IsHidden;
        _menuRep.Update(entity, User.GetLoggedInUserId<string>());
        var result = await _menuRep.Save();
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetDelete(int itemId)
    {
        if (itemId <= 0)
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));

        var entity = await _menuRep.GetById(itemId);
        if (entity is null)
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        _menuRep.Remove(entity);
        return new JsonResult(await _menuRep.Save());
    }

}