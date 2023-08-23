using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication.Pages.ADMIN
{
    public class HomepageModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToPage("/Welcome");
            }
            if (!HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return RedirectToPage("/Unauthen");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Welcome");
        }
    }
}
