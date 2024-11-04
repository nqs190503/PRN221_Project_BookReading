using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class CategoryModel : PageModel
    {

        private readonly PRN221_ProjectContext context;

        public CategoryModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }

        public List<BusinessObject.Models.Book> Books { get; set; } = new List<BusinessObject.Models.Book>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;

        public void OnGet(int id)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Books = context.CategoryInBooks.Include(x => x.Book).Where(x => x.CateId == id && !x.Book.Status.Equals("Delete")).Select(x => x.Book).ToList();
            foreach (var book in Books)
            {
                if (book.Img != null && book.Img.Contains("/images"))
                {
                    //Img= "~" + exist.Img.Substring(1);
                    book.Img = "~" + book.Img.Substring(1);

                }
            }


        }
    }
}
