using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;

namespace Cibrary.Models
{
    public class Book
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        //public Category category { get; set; }
        public string release_year { get; set; }
        public string description { get; set; }
        public string external_link { get; set; }
        public string version { get; set; }
    }
}