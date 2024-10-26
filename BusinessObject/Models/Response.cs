using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Response
    {
        public int ResponseId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public DateTime ResponseTime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
