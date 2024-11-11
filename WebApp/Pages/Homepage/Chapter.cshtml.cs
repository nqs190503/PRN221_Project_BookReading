using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Homepage
{
    public class ChapterModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public ChapterModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public BusinessObject.Models.Chapter Chapter { get; set; } = default!;
        public BusinessObject.Models.Book Book { get; set; } = default!;
        public IActionResult OnGet(int id, int bookId)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            
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
            if (exist == null)
            {
                return NotFound();
            }
            if (existBook == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(UserId) && exist != null && existBook != null)
            {
                var uid = int.Parse(UserId);
                var user = context.Users.Find(uid);
                var reading = context.Readings.FirstOrDefault(x => x.UserId == uid && x.Chapterid == id);
                if (reading == null && user!=null)
                {
                    context.Readings.Add(new Reading
                    {
                        User = user,
                        Book = existBook,
                        Chapter = exist,
                        Chapterid = exist.ChapterId,
                        Bookid = exist.BookId,
                        UserId = uid,
                        ReadingDate = DateTime.Now,
                    });
                    context.SaveChanges();
                }
            }
            return Page();
            
        }
    }
}
