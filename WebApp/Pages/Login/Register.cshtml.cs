using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public RegisterModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public User UserModel { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var existUsername = context.Users.FirstOrDefault(x => x.UserName.Equals(UserModel.UserName));
            if (existUsername != null)
            {
                Message = "Tên đăng nhập đã tồn tại";
                return Page();
            }
            string confirmPassword = Request.Form["confirmPassword"];
            if (!confirmPassword.Equals(UserModel.Password))
            {
                Message = "Mật khẩu nhập lại không trùng";
                return Page();
            }
            context.Users.Add(UserModel);
            context.SaveChanges();
            HttpContext.Session.SetString("userId", UserModel.UserId.ToString());
            return RedirectToPage("/Homepage/Index");
        }
    }
}
