using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class BookDetailModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public BookDetailModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public BusinessObject.Models.Book Book { get; set; } = default!;
        public int BookId { get; set; }

        public IActionResult OnGet(int id)
        {
            BookId = id;

            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var exist = context.Books.Include(x => x.Chapters).FirstOrDefault(x => x.BookId == id);
            
            var rateList = context.Rates.Where(x => x.BookId == id).Select(x => x.Point).ToList();
            ViewData["RateTime"] = rateList.Count;
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
                //if (exist.Img != null && exist.Img.Contains("/images"))
                //{
                //    //Img= "~" + exist.Img.Substring(1);
                //    exist.Img = "~" + exist.Img.Substring(1);

                //}
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
