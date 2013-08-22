using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Cibrary.Models
{
    public class Loan
    {
        public int BookId { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Lånetidspunkt")]
        public DateTime TimeLoaned { get; set; }

        [Display(Name = "Leveringstidspunkt")]
        public DateTime? TimeDelievered { get; set; }

        public virtual User UserProfile { get; set; }
        public virtual Book Book { get; set; }

    }
}