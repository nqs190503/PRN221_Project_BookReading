using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Homepage
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public IndexModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }

        public List<Book> Books { get; set; } = new List<Book>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public void OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Books = context.Books.ToList();
        }
    }
}
