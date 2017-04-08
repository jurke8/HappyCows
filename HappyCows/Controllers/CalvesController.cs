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
    public class CalvesController : Controller
    {
        private HappyCowsContext db = new HappyCowsContext();

        // GET: Calves
        public ActionResult Index()
        {
            var calves = db.Calves.Include(c => c.Cow);
            return View(calves.ToList());
        }

        // GET: Calves/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calf calf = db.Calves.Find(id);
            if (calf == null)
            {
                return HttpNotFound();
            }
            return View(calf);
        }

        // GET: Calves/Create
        public ActionResult Create()
        {
            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name");
            return View();
        }

        // POST: Calves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateOfBirth,CowId,Name,Note,Deleted,DateCreated")] Calf calf)
        {
            if (ModelState.IsValid)
            {
                calf.Id = Guid.NewGuid();
                db.Calves.Add(calf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name", calf.CowId);
            return View(calf);
        }

        // GET: Calves/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calf calf = db.Calves.Find(id);
            if (calf == null)
            {
                return HttpNotFound();
            }
            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name", calf.CowId);
            return View(calf);
        }

        // POST: Calves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateOfBirth,CowId,Name,Note,Deleted,DateCreated")] Calf calf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calf).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name", calf.CowId);
            return View(calf);
        }

        // GET: Calves/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calf calf = db.Calves.Find(id);
            if (calf == null)
            {
                return HttpNotFound();
            }
            return View(calf);
        }

        // POST: Calves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Calf calf = db.Calves.Find(id);
            db.Calves.Remove(calf);
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
