using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages.Logout
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("isAdmin");

            return Redirect("/login");
        }
    }
}
