using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Site.Areas.Identity.Pages.Account;
public class AccessDeniedModel : PageModel
{
    public string ReturnUrl { get; set; }
    public void OnGet(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ReturnUrl = returnUrl;
    }
}