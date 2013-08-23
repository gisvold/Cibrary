using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Cibrary.Models;

namespace Cibrary.Controllers
{
    public class LoanController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {

            return View(db.Loans.ToList());
        }

        
    }
}