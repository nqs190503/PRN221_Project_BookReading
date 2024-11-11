using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace WebApp.Pages.UserReport
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public IndexModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<string> ReportTypes { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public BusinessObject.Models.Book Book { get; set; }
        public bool IsCooldownActive { get; set; } = false;

        public void OnGet(int bookId)
        {
            ReportTypes = context.ReportTypes.Select(x => x.ReportType1).ToList();
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Book = context.Books.FirstOrDefault(b => b.BookId == bookId);
        }
        //public IActionResult OnPost(int bookId, string report, int chapter, string description)
        //{
        //        UserId = HttpContext.Session.GetString("userId");
        //        Categories = context.Categories.ToList();

        //        var reportEntry = new Report
        //        {
        //            UserId = int.Parse(UserId),
        //            BookId = bookId,
        //            Problem = context.ReportTypes.FirstOrDefault(x => x.ReportType1 == report).ReportId,
        //            Chapter = chapter.ToString(),
        //            Detail = description,
        //            ReplyStatus = "Pending"
        //        };

        //        context.Reports.Add(reportEntry);
        //        context.SaveChanges();
        //    return RedirectToPage("/Homepage/Index");
        //}
        public IActionResult OnPost(int bookId, string report, int chapter, string description)
        {
            UserId = HttpContext.Session.GetString("userId");
            Categories = context.Categories.ToList();
            ReportTypes = context.ReportTypes.Select(x => x.ReportType1).ToList();
            Book = context.Books.FirstOrDefault(b => b.BookId == bookId);


            // Check if the user has submitted a report in the last 3 hours
            var LastReport = context.Reports
         .Where(r => r.UserId == int.Parse(UserId))
         .OrderByDescending(r => r.ReportTime)
         .Select(r => r.ReportTime)
         .FirstOrDefault();

            if (LastReport == null || (DateTime.Now - LastReport.Value).TotalHours >= 3)
            {
                // Allow report submission if no previous report or cooldown period has passed
                var reportEntry = new Report
                {
                    UserId = int.Parse(UserId),
                    BookId = bookId,
                    Problem = context.ReportTypes.FirstOrDefault(x => x.ReportType1 == report)?.ReportId ?? 0,
                    Chapter = chapter.ToString(),
                    Detail = description,
                    ReplyStatus = "Pending",
                    ReportTime = DateTime.Now
                };

                context.Reports.Add(reportEntry);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Feedback sent successfully!";
                return RedirectToPage("/UserReport/Index", new { Id = bookId} );

            }
            else
            {
                TempData["ErrorMessage"] = "You can only report once every 3 hours.";

                return RedirectToPage("/UserReport/Index", new { bookId });
            }

        }
    }
}

