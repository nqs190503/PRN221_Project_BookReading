using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Problem { get; set; }
        public string? Chapter { get; set; }
        public string? Detail { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual ReportType ProblemNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
