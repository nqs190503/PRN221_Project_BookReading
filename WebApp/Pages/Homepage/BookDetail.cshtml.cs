using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class BookDetailModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public BookDetailModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public Book Book { get; set; } = default!;
        public void OnGet(int id)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var exist = context.Books.Include(x=>x.Chapters).FirstOrDefault(x=>x.BookId == id);
            if (exist != null)
            {
                if (exist.Img != null && exist.Img.Contains("/images"))
                {
                    //Img= "~" + exist.Img.Substring(1);
                    exist.Img = "~" + exist.Img.Substring(1);
                    
                }
                Book = exist;
            }
        }
    }
}
