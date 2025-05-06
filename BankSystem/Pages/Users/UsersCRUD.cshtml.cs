using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UsersCRUDModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
