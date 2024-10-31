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
        [BindProperty]
        public User UserLogin { get; set; } = default!;
        public void OnGet()
        {
            Categories = context.Categories.ToList();
        }
        public void OnPost()
        {
            var exist = context.Users.FirstOrDefault(x => x.UserName.Equals(UserLogin.UserName)
            && x.Password.Equals(UserLogin.Password));
            if (exist != null)
            {
                
            }
        }
    }
}
