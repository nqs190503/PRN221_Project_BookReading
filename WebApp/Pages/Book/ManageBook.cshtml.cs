using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Book
{
    public class ManageBookModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public ManageBookModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;

        public List<BusinessObject.Models.Book> Books { get; set; } = new List<BusinessObject.Models.Book>();
        public void OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(UserId))
            {
                Books = context.Books.Where(x => x.UserId == int.Parse(UserId) && !x.Status.Equals("Delete")).Include(x => x.User).ToList();
            }
            foreach(var b in Books)
            {
                if (b.Img != null && b.Img.Contains("/images"))
                {
                    //Img= "~" + exist.Img.Substring(1);
                    b.Img = "~" + b.Img.Substring(1);

                }
            }
        }
    }
}
