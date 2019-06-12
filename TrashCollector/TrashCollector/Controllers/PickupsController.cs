using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class PickupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pickups
        public ActionResult Index()
        {
            var EmployeeLoggedIn = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(d => d.ApplicationUserId == EmployeeLoggedIn).FirstOrDefault();
            var pickups = db.Pickups.Include(p => p.Customer).Include(p => p.Employee);
            pickups = pickups.Where(j => j.PickupDone == false).Where(u => u.EmployeeId == employee.EmployeeId);
            return View(pickups.ToList());
        }

        // GET: Pickups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        //// GET: Pickups/Create
       
        //public ActionResult Create(int id)
        //{
        //    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName");
        //    return View();
        //}

        // POST: Pickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Pickup pickup, int id)
        {
            if (ModelState.IsValid)
            {
                var EmployeeLoggedIn = User.Identity.GetUserId();
                Employee employee = db.Employees.Where(d => d.ApplicationUserId == EmployeeLoggedIn).FirstOrDefault();
                pickup.EmployeeId = employee.EmployeeId;
                pickup.Date = DateTime.Now;
                pickup.DayOfWeek = pickup.Date.ToString("dddd");
                pickup.CustomerId = id;
                db.Pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", pickup.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", pickup.EmployeeId);
            return View(pickup);
        }

        // GET: Pickups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", pickup.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", pickup.EmployeeId);
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,PickupDone,EmployeeId,CustomerId")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddToBill","Employees");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", pickup.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", pickup.EmployeeId);
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            db.Pickups.Remove(pickup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Complete(int id)
        {
            var date = DateTime.Now;
            var day = date.ToString("dddd");
            Pickup pickup = db.Pickups.Find(id);
            pickup.PickupDone = true;
            db.SaveChanges();
            return RedirectToAction("AddToBill", "Employee");
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
