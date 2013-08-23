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

        
        public string AgeOfLoan()
        {
            TimeSpan span = DateTime.Now - this.TimeLoaned;
            string timeSinceLoanedString = string.Format("{0}{1}",
                span.Duration().Days > 0
                    ? string.Format("{0:0} dag{1}, ", span.Days, span.Days == 1 ? String.Empty : "er")
                    : string.Empty,
                span.Duration().Hours >= 0
                    ? string.Format("{0:0} time{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "r")
                    : string.Empty);

            if (timeSinceLoanedString.EndsWith(", "))
            {
                timeSinceLoanedString = timeSinceLoanedString.Substring(0, timeSinceLoanedString.Length - 2);
            }

            return timeSinceLoanedString+" siden";
        }

    }
}