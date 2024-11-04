using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Chapter
{
    public class AddModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public AddModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public BusinessObject.Models.Chapter Chapter { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        [BindProperty]
        public int BookId { get; set; }
        public void OnGet(int bookId)
        {
            BookId = bookId;
        }
        public IActionResult OnPost()
        {
            var existChapterNumber = context.Chapters.FirstOrDefault(x => x.BookId == BookId && x.NumberChapter == Chapter.NumberChapter);
            if (existChapterNumber != null)
            {
                Message = "Đã có chương "+existChapterNumber.NumberChapter +" trong truyện";
                return Page();
            }
            var book = context.Books.Find(BookId);
            string content = Request.Form["content"];
            string content1 = content.Substring(0, content.Length);
            string content2 = content.Substring(content.Length);
            Chapter.Contents1 = content1;
            Chapter.Contents2 = content2;
            if (book != null)
            {
                Chapter.Book = book;
                context.Chapters.Add(Chapter);
                context.SaveChanges();
            }
            return RedirectToPage("/Book/ManageChapter");
        }
    }
}
