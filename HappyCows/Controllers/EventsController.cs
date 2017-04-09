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
    public class EventsController : Controller
    {
        private HappyCowsContext db = new HappyCowsContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            var cows = db.Cows.Where(c => (int)c.State == 0).ToList();
            ViewBag.CowId = new SelectList(cows, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventType,EventDate,CowId,Name,Deleted,DateCreated,Note")] Event @event)
        {
            if (ModelState.IsValid && @event.CowId != Guid.Empty)
            {
                var cow = db.Cows.Where(c => c.Id == @event.CowId).First();
                cow.State = (cow.State == Enums.CowStateEnum.PREGNANT) ? Enums.CowStateEnum.OPEN : (Enums.CowStateEnum)(++cow.State);
                db.Entry(cow).State = EntityState.Modified;
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var cows = db.Cows.Where(c => (int)c.State == (int)@event.EventType).ToList();
            ViewBag.CowId = new SelectList(cows, "Id", "Name", @event.CowId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name", @event.CowId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventType,EventDate,CowId,Name,Deleted,DateCreated,Note")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name", @event.CowId);

            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetCows(int stateId)
        {
            var cows = db.Cows.Where(c => (int)c.State == stateId).ToList();
            SelectList cowsList = new SelectList(cows, "Id", "Name");
            return Json(cowsList);
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
