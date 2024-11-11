using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Shared
{
    public class HeaderModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public HeaderModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; }  = new List<Category>();
        public void OnGet()
        {
        }
    }
}
