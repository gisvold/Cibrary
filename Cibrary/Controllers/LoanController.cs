using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Cibrary.Models;
using Microsoft.AspNet.Identity;

namespace Cibrary.Controllers
{
    public class LoanController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            var loansNotDelivered = db.Loans.Where(l => l.TimeDelievered == null);
            return View(loansNotDelivered.ToList());
        }
        public ActionResult DeliverBook(Int32 id)
        {
            String userId = User.Identity.GetUserId();
            Loan loanToDeliver = db.Loans.Find(id, userId);
            loanToDeliver.TimeDelievered = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
    }
    
}