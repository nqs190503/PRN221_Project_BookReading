using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Homepage
{
    public class ChapterModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public ChapterModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public Chapter Chapter { get; set; } = default!;
        public Book Book { get; set; } = default!;
        public void OnGet(int id, int bookId)
        {
            Categories = context.Categories.ToList();
            var existBook = context.Books.FirstOrDefault(x => x.BookId == bookId);
            if (existBook != null)
            {
                Book = existBook;
            }
            var exist = context.Chapters.Find(id);
            if (exist != null)
            {
                Chapter = exist;
            }
        }
    }
}
