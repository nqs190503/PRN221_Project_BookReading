using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public IndexModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        [BindProperty]
        public User UserModel { get; set; } = default!;

        public string Message { get; set; } = string.Empty;
        public void OnGet(int id)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");

            var exist = context.Users.Find(id);
            if (exist != null)
            {
                UserModel = exist;
            }
        }
        public IActionResult OnPost()
        {
            string newPassword = Request.Form["newPassword"];
            string currentPassword = Request.Form["currentPassword"];
            string confirmPassword = Request.Form["confirmPassword"];
            string userId = Request.Form["userId"];
            var user = context.Users.Find(int.Parse(userId));
            if (user != null)
            {
                if (!string.IsNullOrEmpty(newPassword))
                {
                    if (currentPassword.Equals(user.Password))
                    {
                        if (newPassword.Equals(currentPassword))
                        {
                            Message = "Mật khẩu mới đang trùng với mật khẩu cũ";
                        }
                        else if (!newPassword.Equals(confirmPassword))
                        {
                            Message = "Nhập lại không trùng khớp";
                        }
                        else
                        {
                            user.Password = newPassword;
                            UserModel = user;
                            context.Users.Update(user);
                            context.SaveChanges();
                            Message = "Đổi mật khẩu thành công";
                        }
                    }
                    else
                    {
                        Message = "Sai mật khẩu hiện tại";
                    }
                }
                else
                {
                    string email = Request.Form["email"];
                    string address = Request.Form["address"];
                    string phone = Request.Form["phone"];
                    user.Email = email;
                    user.Address = address;
                    user.Phone = phone;
                    UserModel = user;
                    context.Users.Update(user);
                    context.SaveChanges();

                }
            }
            
            return Page();
        }
    }
}
