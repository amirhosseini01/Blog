using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;
using Site.Configurations.ClaimHelper;

namespace Site.Areas_Admin_Pages;

[ClaimRequirement(claimType: nameof(IndexModel), claimValue: ClaimStore.View)]
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }
    public async Task<JsonResult> OnPostUploadImage(IFormFile file)
    {
        var res = await file.UploadFile($"/images/uploadedImage/{Guid.NewGuid()}", new FileSizeType() { Size = 500 });

        return new JsonResult(res);
    }
}