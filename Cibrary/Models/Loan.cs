using System;
using System.Collections;

namespace Cibrary.Models
{
    public class Loan
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime TimeLoaned { get; set; }
        public DateTime? TimeDelievered { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }

    }
}