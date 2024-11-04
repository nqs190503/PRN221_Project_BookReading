using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace WebApp.Pages.UserReport
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public IndexModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<string> ReportTypes { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public BusinessObject.Models.Book Book { get; set; }    
        public void OnGet(int bookId)
        {
            ReportTypes = context.ReportTypes.Select(x=>x.ReportType1).ToList();
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Book = context.Books.FirstOrDefault(b => b.BookId == bookId);
        }
        public IActionResult OnPost(int bookId, string report, int chapter, string description)
        {
                UserId = HttpContext.Session.GetString("userId");
                Categories = context.Categories.ToList();

                var reportEntry = new Report
                {
                    UserId = int.Parse(UserId),
                    BookId = bookId,
                    Problem = context.ReportTypes.FirstOrDefault(x => x.ReportType1 == report).ReportId,
                    Chapter = chapter.ToString(),
                    Detail = description,
                    ReplyStatus = "Pending"
                };

                context.Reports.Add(reportEntry);
                context.SaveChanges();
            return RedirectToPage("/Homepage/Index");
        }
    }
}
