using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HappyCows.Models;
using HappyCows.Utils;

namespace HappyCows.Controllers
{
    public class EventsController : Controller
    {
        private HappyCowsContext db = new HappyCowsContext();

        // GET: Events
        public ActionResult Index(string sortOrder, int? TypeFilter, Guid? CowId)
        {
            var events = db.Events.ToList();

            ViewBag.CowId = new SelectList(db.Cows, "Id", "Name");

            // Filtering
            if (TypeFilter != null)
            {
                ViewBag.TypeFilter = TypeFilter;
                events = events.Where(e => (int)e.EventType == TypeFilter).ToList();
            }
            if (CowId != null)
            {
                ViewBag.CowFilter = CowId;
                events = events.Where(e => e.CowId == CowId).ToList();
            }

            //Sorting
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.CowSortParm = sortOrder == "cow" ? "cow_desc" : "cow";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";



            switch (sortOrder)
            {
                case "name":
                    events = events.OrderBy(c => c.Name).ToList();
                    break;
                case "name_desc":
                    events = events.OrderByDescending(c => c.Name).ToList();
                    break;
                case "type":
                    events = events.OrderBy(c => c.EventType).ToList();
                    break;
                case "type_desc":
                    events = events.OrderByDescending(c => c.EventType).ToList();
                    break;
                case "cow":
                    events = events.OrderBy(c => c.Cow.Name).ToList();
                    break;
                case "cow_desc":
                    events = events.OrderByDescending(c => c.Cow.Name).ToList();
                    break;
                case "date":
                    events = events.OrderBy(c => c.EventDate).ToList();
                    break;
                case "date_desc":
                    events = events.OrderByDescending(c => c.EventDate).ToList();
                    break;
                default:
                    events = events.OrderBy(c => c.DateCreated).ToList();
                    break;
            }
            return View(events);
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
        public ActionResult Create([Bind(Include = "Id,EventType,EventDate,CowId,Name,DateCreated,Note")] Event @event)
        {
            if (ModelState.IsValid && @event.CowId != Guid.Empty)
            {
                var cow = db.Cows.Where(c => c.Id == @event.CowId).First();
                cow.State = (cow.State == Enums.CowStateEnum.PREGNANT) ? Enums.CowStateEnum.OPEN : (Enums.CowStateEnum)(++cow.State);
                cow.DateOfPreviousEvent = @event.EventDate;
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
        public ActionResult Edit([Bind(Include = "Id,EventType,EventDate,CowId,Name,Note")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var cow = db.Cows.Where(c => c.Id == @event.CowId).First();

                db.Entry(@event).State = EntityState.Modified;

                cow.DateOfPreviousEvent = @event.EventDate;
                db.Entry(cow).State = EntityState.Modified;
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
        public ActionResult Export(int? TypeFilter, Guid? CowId)
        {
            var fileDownloadName = "Događaji-" + DateTime.Now.ToShortDateString() + ".xlsx";
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var reportUtils = new ReportUtils();

            var fileStream = reportUtils.GenerateReport(TypeFilter,CowId);
            if (fileStream != null)
            {
                var fsr = new FileStreamResult(fileStream, contentType) { FileDownloadName = fileDownloadName };
                return fsr;
            }
            else
            {
                return RedirectToAction("Index", "Events", new { errors = true });
            }
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
