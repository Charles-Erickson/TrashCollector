using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Owin;
using System.Web.Mvc;
using TrashCollector;
using TrashCollector.Models;
using Microsoft.AspNet.Identity;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {

        //string currentlyLoggedInUserId = User.Identity.GetUserId();

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var EmployeeLoggedIn = User.Identity.GetUserId();
            var employees = db.Employees;
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,ZipCode,UserId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ApplicationUserId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("EmployeeCustomerList","Customers");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", employee.ApplicationUserId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", employee.ApplicationUserId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,ZipCode,UserId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", employee.ApplicationUserId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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


        public ActionResult AddToBill(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            var customerid = pickup.CustomerId;
            Customer customer = db.Customers.Find(customerid);
            customer.BillAmount = customer.BillAmount + 40;
            return RedirectToAction("EmployeeProfile");
        }

        //public ActionResult FindDayOfWeek(Employee employee)
        //{
        //    var employeeId = employee.EmployeeId;
        //    var customerDay = db.Employees.Where(u => u.EmployeeId == employeeId).Select(db.Customers.Startdate);
        //    var customernumber = customerDay.DayOfWeek;
        //    var dayNumber = DateTime.Now.DayOfWeek;
        //}





        //public ActionResult PickUp()
        //{
        //    var EmployeeLoggedIn = User.Identity.GetUserId();
        //    var employees = db.Employees.Include(e => e.ApplicationUserId == EmployeeLoggedIn);
        //    return View(employees);
        //}


        //public ActionResult PickUp(Employee employee)
        //{
        
        //}
    }
}


