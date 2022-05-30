using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Configurations;

namespace Site.Areas_Admin_Pages;

[ClaimRequirement(claimType: nameof(IndexModel), claimValue: ClaimStore.View)]
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }
}