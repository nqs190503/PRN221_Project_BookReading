using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Profile
{
    public class BookModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public BookModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        [BindProperty]
        public BusinessObject.Models.Book Book { get; set; } = default!;
        public void OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");

        }
        public IActionResult OnPost()
        {
            var selectedCategoryIds = Request.Form["category"].Select(int.Parse).ToList();
            var selectedCategories = context.Categories
                                           .Where(c => selectedCategoryIds.Contains(c.CateId))
                                           .ToList();
            List<CategoryInBook> categoryInBooks = new List<CategoryInBook>();

            var userId = HttpContext.Session.GetString("userId");

            if(userId != null)
            {
                var user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(userId));
                Book.User = user;
                //Book.UserId = user.UserId;
            }
            Book.PublishDate = DateTime.Now;
            Book.Status = "Updating";
            Book.Approve = "Pending";
            context.Books.Add(Book);
            context.SaveChanges();
            foreach (var category in selectedCategories)
            {
                categoryInBooks.Add(new CategoryInBook
                {
                    CateId = category.CateId,
                    BookId = Book.BookId,
                });
            }
            context.CategoryInBooks.AddRange(categoryInBooks);
            context.SaveChanges();

            return RedirectToPage("/Homepage/Index");
        }
    }
}
