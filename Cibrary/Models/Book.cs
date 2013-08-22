using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace Cibrary.Models
{
    public class Book
    {
        
        public int BookId { get; set; }

        [Required]
        [Display(Name = "Tittel")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Forfatter")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Utgivelsesår")]
        [Range(minimum:1900, maximum: 2200, ErrorMessage = "Må ha gyldig årstall.")]
        public int ReleaseYear { get; set; }

        [Required]
        [Display(Name = "Beskrivelse")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Ekstern link")]
        public string ExternalLink { get; set; }

        [Display(Name = "Utgave")]
        public string Edition { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Loan> Loans{ get; set; }

        
    }
}