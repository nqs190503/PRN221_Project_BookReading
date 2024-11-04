using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Chapter
{
    public class DeleteChapterModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public DeleteChapterModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int id)
        {
            var exist = context.Chapters.Find(id);
            if (exist != null)
            {
                context.Chapters.Remove(exist);
                context.SaveChanges();
                return RedirectToPage("/Chapter/ChapterList", new { id = exist.BookId });
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
