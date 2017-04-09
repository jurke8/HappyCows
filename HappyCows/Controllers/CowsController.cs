using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HappyCows.Models;

namespace HappyCows.Controllers
{
    public class CowsController : Controller
    {
        private HappyCowsContext db = new HappyCowsContext();

        // GET: Cows
        public ActionResult Index(string sortOrder, int? StateFilter)
        {
            var cows = db.Cows.ToList();

            // Filtering
            if (StateFilter != null)
            {
                ViewBag.StateFilter = StateFilter;
                cows = cows.Where(c => (int)c.State == StateFilter).ToList();
            }

            //Set duration
            foreach (var cow in cows)
            {
                cow.DurationOfState = (DateTime.Now - cow.DateOfPreviousEvent).Days;
            }

            //Sorting
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.StateSortParm = sortOrder == "state" ? "state_desc" : "state";
            ViewBag.DurationSortParm = sortOrder == "duration" ? "duration_desc" : "duration";

            switch (sortOrder)
            {
                case "name":
                    cows = cows.OrderBy(c => c.Name).ToList();
                    break;
                case "name_desc":
                    cows = cows.OrderByDescending(c => c.Name).ToList();
                    break;
                case "state":
                    cows = cows.OrderBy(c => c.State).ToList();
                    break;
                case "state_desc":
                    cows = cows.OrderByDescending(c => c.State).ToList();
                    break;
                case "duration":
                    cows = cows.OrderBy(c => c.DurationOfState).ToList();
                    break;
                case "duration_desc":
                    cows = cows.OrderByDescending(c => c.DurationOfState).ToList();
                    break;
                default:
                    cows = cows.OrderBy(c => c.DateCreated).ToList();
                    break;
            }
            return View(cows);
        }

        // GET: Cows/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cow cow = db.Cows.Find(id);
            if (cow == null)
            {
                return HttpNotFound();
            }
            return View(cow);
        }

        // GET: Cows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,State,Note,Name,Deleted,DateCreated")] Cow cow)
        {
            if (ModelState.IsValid)
            {
                cow.DateOfPreviousEvent = DateTime.Now;
                db.Cows.Add(cow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cow);
        }

        // GET: Cows/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cow cow = db.Cows.Find(id);
            if (cow == null)
            {
                return HttpNotFound();
            }
            return View(cow);
        }

        // POST: Cows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,State,Note,Name,Deleted,DateCreated")] Cow cow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cow);
        }

        // GET: Cows/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cow cow = db.Cows.Find(id);
            if (cow == null)
            {
                return HttpNotFound();
            }
            return View(cow);
        }

        // POST: Cows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Cow cow = db.Cows.Find(id);
            db.Cows.Remove(cow);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
