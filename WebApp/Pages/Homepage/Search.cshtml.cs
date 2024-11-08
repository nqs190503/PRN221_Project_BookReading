using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class SearchModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public SearchModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<BusinessObject.Models.Book> Books { get; set; } = new List<BusinessObject.Models.Book>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public void OnGet(string key_word)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Books = context.Books.Where(x => x.Title.Contains(key_word) && x.Approve.Equals("Approved") && !x.Status.Equals("Delete")).ToList();
            
        }
    }
}
