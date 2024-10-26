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
        public void OnGet()
        {
            Books = context.Books.ToList();
        }
    }
}
