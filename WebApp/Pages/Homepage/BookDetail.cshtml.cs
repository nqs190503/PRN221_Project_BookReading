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
        public Book Book { get; set; } = default!;
        public void OnGet(int id)
        {
            var exist = context.Books.Find(id);
            if (exist != null)
            {
                if (exist.Img != null && exist.Img.Contains("/images"))
                {
                    //Img= "~" + exist.Img.Substring(1);
                    exist.Img = "~" + exist.Img.Substring(1);
                    
                }
                Book = exist;
            }
        }
    }
}
