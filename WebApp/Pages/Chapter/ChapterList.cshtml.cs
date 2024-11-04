using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Chapter
{
    public class ChapterListModel : PageModel
    {
        private readonly PRN221_ProjectContext context;

        public ChapterListModel(PRN221_ProjectContext context)
        {
            this.context = context;
        }
        public List<BusinessObject.Models.Chapter> Chapters { get; set; } = new List<BusinessObject.Models.Chapter>();
        public Dictionary<int,int> CountWords { get; set; } = new Dictionary<int, int>();
        public void OnGet(int id)
        {
            Chapters = context.Chapters.Where(x => x.BookId == id).ToList();
            foreach (var chapter in Chapters)
            {
                if(chapter.Contents1 !=null && chapter.Contents2 != null)
                {
                    var count = chapter.Contents1.Length + chapter.Contents2.Length;
                    CountWords.Add(chapter.ChapterId, count);
                    
                }
                else
                {
                    CountWords.Add(chapter.ChapterId,0);
                }
            }

        }
    }
}
