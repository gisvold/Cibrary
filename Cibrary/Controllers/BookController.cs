using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Cibrary.Models;

namespace Cibrary.Controllers
{
    public class BookController : Controller
    {
        private DataContext db = new DataContext();


        //
        // GET: /Book/
    /*    public ActionResult Index()
        {
            return View(db.Books.ToList());
        }
     */   
        //
        // GET: /Book/Details/5
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
        public ActionResult Create(Book book, int[] selectedCategories)
        {
            if (book.Categories == null)
            {
                book.Categories = new Collection<Category>();
            }

            foreach (var selectedCategory in selectedCategories)
            {
                var category = db.Category.Find(selectedCategory);

                book.Categories.Add(category);
            }



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
        public ActionResult Edit(Int32 id)
        {
            Book book = db.Books.Find(id);
            ViewBag.Categories = db.Category.ToList();
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
        public ActionResult Edit(Book book, int[] selectedCategories)
        {
            var dbBook = db.Books.Find(book.BookId);
            var oldCategories = dbBook.Categories;
            book.Categories = new Collection<Category>();
            if (oldCategories != null)
            {
                book.Categories = oldCategories;
            }
            

            UpdateBookCategories(selectedCategories,book);
            if (ModelState.IsValid)
            {
                db.Entry(dbBook).CurrentValues.SetValues(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }
        private void UpdateBookCategories(int[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.Categories.Clear();
                return;
            }
            var selectedCategoriesHS = new HashSet<int>(selectedCategories);
            var bookCategories = new HashSet<int>
                (bookToUpdate.Categories.Select(c => c.CategoryId));
            foreach (var category in db.Category)
            {
                if (selectedCategoriesHS.Contains(category.CategoryId))
                {
                    if (!bookCategories.Contains(category.CategoryId))
                    {
                        bookToUpdate.Categories.Add(category);
                    }
                }
                else
                {
                    if (bookCategories.Contains(category.CategoryId))
                    {
                        bookToUpdate.Categories.Remove(category);
                    }
                }
            }
        }
        //
        // GET: /Book/Delete/5
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

        //new code 21/08
        public ActionResult Index(string searchString)
        {
            IQueryable<Book> books = from m in db.Books
                       select m;


            if (!String.IsNullOrEmpty(searchString))
            {
                int AsciiString = (int)searchString[0];
                int intString =0;
                if (!(AsciiString < 48 || AsciiString > 57))
                    intString = Convert.ToInt32(searchString);
                books = books.Where(s => (s.Title.Contains(searchString) || (s.Author.Contains(searchString)) || (s.Description.Contains(searchString)) || (s.Edition.Contains(searchString)) || (s.Categories.Any(c => c.Name.Equals(searchString)))||(s.ReleaseYear==intString)));
                
            }

            return View(books);
        }

    }
}
