using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class BookDetailModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public BookDetailModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Category> CategoryInBook { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public BusinessObject.Models.Book Book { get; set; } = default!;
        public int BookId { get; set; }

        public int Role { get; set; } = -1;
        public IActionResult OnGet(int id)
        {
            BookId = id;

            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var exist = context.Books.Include(x => x.Chapters).FirstOrDefault(x => x.BookId == id);
            
            var rateList = context.Rates.Where(x => x.BookId == id).Select(x => x.Point).ToList();
            ViewData["RateTime"] = rateList.Count;
            var role = HttpContext.Session.GetString("role");
            if (role != null)
            {
                Role = int.Parse(role);
            }
            var ratePoint = 0;
            foreach (var rate in rateList)
            {
                ratePoint += rate;
            }
            if(rateList.Count > 0)
            {
                ViewData["RatePoint"] = ratePoint / rateList.Count;
            }
            if (exist != null)
            {
                CategoryInBook = context.CategoryInBooks.Where(x=>x.BookId == exist.BookId).Select(x=>x.Cate).ToList();
                Book = exist;
                return Page();
            }
            else
            {
                return NotFound();
            }
           
        }
    }
}
