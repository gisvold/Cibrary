using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibrary.Models;

namespace Cibrary.Controllers
{
    public class BookController : Controller
    {
        private DataContext db = new DataContext();

        // GET: /Book/Details/5
        [Authorize]
        public ActionResult Details(Int32 id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // GET: /Book/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Category.ToList();
            return View();
        }

        //
        // POST: /Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        //
        // GET: /Book/Edit/5
        [Authorize]
        public ActionResult Edit(Int32 id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        //
        // GET: /Book/Delete/5
        [Authorize]
        public ActionResult Delete(Int32 id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //Seach Function
        public ActionResult Index(string searchString)
        {
            IQueryable<Book> book = from m in db.Books
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {

            int intString;
            int.TryParse(searchString, out intString);

            book = book.Where(s => (s.Title.Contains(searchString) || (s.Author.Contains(searchString)) || (s.Description.Contains(searchString)) || (s.Edition.Contains(searchString)) || (s.Categories.Any(c => c.Name.Equals(searchString)))||(s.ReleaseYear==intString)));
                
            }

            //book = book.Where(s => (s.Loans.Any(c => (c.BookId.Equals(s.BookId)))));
            
            return View(book);
        }

    }
}
