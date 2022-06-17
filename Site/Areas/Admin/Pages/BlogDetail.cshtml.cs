using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.ClaimHelper;
using Site.Features.Blog;
using Site.Features.BlogCategory;
using Site.ViewModels;

namespace Site.Areas_Admin_Pages;

[ClaimRequirement(claimType: nameof(BlogDetailModel), claimValue: ClaimStore.View)]
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

        if (id is not null)
        {
            var entity = await _blogRep.GetById(id.Value);
            VmInput = new()
            {
                CanonicalUrl = entity.CanonicalUrl,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                Id = entity.Id,
                ImgAlt = entity.ImgAlt,
                IsHidden = entity.IsHidden,
                ImgTitle = entity.ImgTitle,
                MetaDescription = entity.MetaDescription,
                ImgUrl = entity.ImgUrl,
                Title = entity.Title,
                KeyWords = entity.KeyWords
            };
        }
    }

    public async Task<JsonResult> OnPostAdd(VmBlogDetail VmInput)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        if (string.IsNullOrEmpty(VmInput.ImgBase64))
        {
            return new JsonResult(new ResponsePayload(false, Messages.ChosseFile));
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
            CanonicalUrl = VmInput.CanonicalUrl,
            MetaDescription = VmInput.MetaDescription,
            CategoryId = VmInput.CategoryId,
            ImgAlt = VmInput.ImgAlt,
            ImgTitle = VmInput.ImgTitle,
            IsHidden = VmInput.IsHidden,
            KeyWords = VmInput.KeyWords
        };

        await _blogRep.Add(entity, currentUserId);
        var result = await _blogRep.Save();
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnPostEdit(VmBlogDetail VmInput)
    {
        if (!ModelState.IsValid || VmInput.Id is null || VmInput.Id <= 0)
        {
            return new JsonResult(new ResponsePayload(false, Messages.InvalidData));
        }

        var entity = await _blogRep.GetById(VmInput.Id.Value);
        if (entity is null)
        {
            return new JsonResult(new ResponsePayload(false, Messages.NotFound));
        }

        if (!string.IsNullOrEmpty(VmInput.ImgBase64))
        {
            var uploadRes = await VmInput.ImgBase64.SaveBase64("/images/blog/", new FileSizeType() { Size = 500 });
            if (!uploadRes.Succeeded)
            {
                return new JsonResult(uploadRes);
            }

            entity.ImgUrl.DeleteFile();
            entity.ImgUrl = uploadRes.Obj;
        }

        string currentUserId = User.GetLoggedInUserId<string>();
        entity.Title = VmInput.Title;
        entity.Description = VmInput.Description;
        entity.CanonicalUrl = VmInput.CanonicalUrl;
        entity.MetaDescription = VmInput.MetaDescription;
        entity.CategoryId = VmInput.CategoryId;
        entity.ImgAlt = VmInput.ImgAlt;
        entity.ImgTitle = VmInput.ImgTitle;
        entity.IsHidden = VmInput.IsHidden;
        entity.KeyWords = VmInput.KeyWords;

        _blogRep.Update(entity , currentUserId);
        var result = await _blogRep.Save();
        return new JsonResult(result);
    }
}