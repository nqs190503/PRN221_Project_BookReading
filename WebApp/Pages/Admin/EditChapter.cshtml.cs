using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Admin
{
    public class EditChapterModel : PageModel
    {
        private readonly PRN221_ProjectContext context;
        public EditChapterModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public BusinessObject.Models.Chapter Chapter { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public void OnGet(int id)
        {
            var exist = context.Chapters.Find(id);
            if (exist != null)
            {
                Chapter = exist;
            }
        }
        public IActionResult OnPost()
        {
            try
            {
                string content = Request.Form["content"];
                string content1 = content.Substring(0, content.Length/2);
                string content2 = content.Substring(content.Length/2);
                Chapter.Contents1 = content1;
                Chapter.Contents2 = content2;
                context.Chapters.Update(Chapter);
                context.SaveChanges();
                Message = "Đã cập nhật chapter";
                return RedirectToPage("ManageChapter/",new {id = Chapter.BookId });
            }
            catch (Exception)
            {
                Message = "Có lỗi xảy ra khi cập nhật chapter";
                return Page();
            }
        }
    }
}
