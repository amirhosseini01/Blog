using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.ClaimHelper;
using Site.Models;
using Site.Repositories.Contracts;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;
public class BlogDetailModel : PageModel
{
    private readonly IBlogRep _blogRep;
    private readonly ICategoryRep _categoryRep;
    public BlogDetailModel(IBlogRep blogRep, ICategoryRep categoryRep)
    {
        _blogRep = blogRep;
        _categoryRep = categoryRep;
    }

    //properties
    public int? Id { get; set; }
    public VmBlogDetail VmInput { get; set; }
    public List<Category> Categories { get; set; }

    //methods
    public async Task OnGet(int? id)
    {
        Id = id;

        Categories = await _categoryRep.GetAll();
    }

    public async Task<JsonResult> OnPostAdd(VmBlogDetail VmInput)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new ResponsePayload(false, "خطا در اطلاعات ورودی"));
        }

        if (string.IsNullOrEmpty(VmInput.ImgBase64))
        {
            return new JsonResult(new ResponsePayload(false, "فایل را وارد کنید"));
        }

        var uploadRes = await VmInput.ImgBase64.SaveBase64("/images/blog/", new FileSizeType() { Size = 500 });
        if (!uploadRes.Succeeded)
        {
            return new JsonResult(uploadRes);
        }

        string currentUserId = User.GetLoggedInUserId<string>();
        Blog entity = new()
        {
            Title = VmInput.Title,
            Description = VmInput.Description,
            ImgUrl = uploadRes.Obj,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            CreateUserId = currentUserId,
            UpdateUserId = currentUserId,
            CanonicalUrl = VmInput.CanonicalUrl,
            MetaDescription = VmInput.MetaDescription,
            CategoryId = VmInput.CategoryId,
            ImgAlt = VmInput.ImgAlt,
            ImgTitle = VmInput.ImgTitle,
            IsHidden = VmInput.IsHidden
        };

        await _blogRep.Add(entity);
        var result = await _blogRep.Save();
        return new JsonResult(result);
    }
}