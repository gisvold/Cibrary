using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cibrary.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Kategorinavn")]
        public string Name { get; set; }
        public virtual ICollection<Book> Books{ get; set; }

    }
}