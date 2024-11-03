using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public LoginModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string UserId { get; set; } = default!;
        [BindProperty]
        public User UserLogin { get; set; } = default!;
        public void OnGet()
        {
            Categories = context.Categories.ToList();
        }
        public IActionResult OnPost()
        {
            var exist = context.Users.FirstOrDefault(x => x.UserName.Equals(UserLogin.UserName)
            && x.Password.Equals(UserLogin.Password));
            if (exist != null)
            {
                HttpContext.Session.SetString("userId", exist.UserId.ToString());
                return RedirectToPage("/Homepage/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
