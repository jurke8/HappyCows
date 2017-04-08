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
        public ActionResult Index()
        {
            return View(db.Cows.ToList());
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
                cow.Id = Guid.NewGuid();
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
