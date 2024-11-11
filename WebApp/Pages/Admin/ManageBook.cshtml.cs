using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Admin
{
    public class ManageBookModel : PageModel
    {
        private readonly PRN221_ProjectContext context;
        public ManageBookModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();

        public List<int> CategoriesInBook { get; set; } = new List<int>();
        [BindProperty]
        public BusinessObject.Models.Book Book { get; set; } = default!;
        public string? UserId { get; set; } = default!;
        public void OnGet(int bookId)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var exist = context.Books.Find(bookId);

            if (exist != null)
            {
                CategoriesInBook = context.CategoryInBooks.Where(x => x.BookId == exist.BookId).Select(x => x.CateId).ToList();

                Book = exist;
            }
        }
        public IActionResult OnPost()
        {
            var selectedCategoryIds = Request.Form["category"].Select(int.Parse).ToList();
            var selectedCategories = context.Categories
                                           .Where(c => selectedCategoryIds.Contains(c.CateId)).Select(x => x.CateId)
                                           .ToList();
            var CategoryInBook = context.CategoryInBooks
                 .Where(x => x.BookId == Book.BookId).Select(x => x.CateId).ToList();
            Book.Status = "Updating";
            foreach (var category in selectedCategories)
            {
                if (!CategoriesInBook.Contains(category))
                {
                    context.CategoryInBooks.Add(new BusinessObject.Models.CategoryInBook
                    {
                        CateId = category,
                        BookId = Book.BookId,
                    });
                }
            }
            context.Books.Update(Book);
            context.SaveChanges();
            return RedirectToPage("/Homepage/BookDetail",new {id = Book.BookId});
        }
    }
}