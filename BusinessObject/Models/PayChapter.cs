using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class PayChapter
    {
        public int PayId { get; set; }
        public int UserId { get; set; }
        public int ChapterId { get; set; }
        public DateTime PayDate { get; set; }

        public virtual Chapter Chapter { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
