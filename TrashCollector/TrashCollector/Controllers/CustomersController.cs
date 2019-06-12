using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var CustomerLoggedIn = User.Identity.GetUserId();
            var customers = db.Customers.Where(u => u.ApplicationUserId == CustomerLoggedIn);
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
           var id= User.Identity.GetUserId();
             Customer customer = new Customer();
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,LastName,Address,Zipcode,City,StartDate,State")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ApplicationUserId=User.Identity.GetUserId();

                string address = (customer.Address + "+" + customer.City + "+" + customer.State + "+" + customer.Zipcode);
                GeoController geocode = new GeoController();
                geocode.SendRequest(address);
                customer.Lat = geocode.latitude;
                customer.Lng = geocode.longitude;
                UpdateDates(customer);
                //customer.EmployeeId = db.Employees.Where(k => k.ZipCode == customer.Zipcode).Select(w=>w.EmployeeId).FirstOrDefault();
                customer.DayOfWeek = customer.StartDate.ToString("dddd");
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("CustomerProfile");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }



        public void UpdateDates(Customer customer)
        {
            if (customer.PauseStart < SqlDateTime.MinValue.Value)
            {
                customer.PauseStart = SqlDateTime.MinValue.Value;
            }
            if (customer.StartDate < SqlDateTime.MinValue.Value)
            {
                customer.StartDate = SqlDateTime.MinValue.Value;
            }
            if (customer.OneTimePickUp < SqlDateTime.MinValue.Value)
            {
                customer.OneTimePickUp = SqlDateTime.MinValue.Value;
            }
            if (customer.PauseEnd < SqlDateTime.MinValue.Value)
            {
                customer.PauseEnd = SqlDateTime.MinValue.Value;
            }
        }


        public ActionResult EmployeeCustomerList()
        {
            var date = DateTime.Now;
            var day = date.ToString("dddd");
            var EmployeeLoggedIn = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(d => d.ApplicationUserId == EmployeeLoggedIn).FirstOrDefault();
            IQueryable<Customer> customer = db.Customers.Where(n => n.Zipcode == employee.ZipCode).Where(j=>j.OneTimePickUpDay==day||j.DayOfWeek==day);
            return View(customer);
        }


        public ActionResult TotalCustomerList(string searchString)
        {
       
            var EmployeeLoggedIn = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(d => d.ApplicationUserId == EmployeeLoggedIn).FirstOrDefault();
            IQueryable<Customer> customer = db.Customers.Where(n => n.Zipcode == employee.ZipCode);
            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.DayOfWeek.Contains(searchString));
            }

           
            return View(customer);
        }

        public ActionResult EmployeeCustomerView(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //foreach (var change in customer.OfType(DateTime))
        //{
        //    var values = change.CurrentValues;
        //    foreach (var name in values.PropertyNames)
        //    {
        //        var value = values[name];
        //        if (value is DateTime)
        //        {
        //            var date = (DateTime)value;
        //            if (date < SqlDateTime.MinValue.Value)
        //            {
        //                values[name] = SqlDateTime.MinValue.Value;
        //            }
        //            else if (date > SqlDateTime.MaxValue.Value)
        //            {
        //                values[name] = SqlDateTime.MaxValue.Value;
        //            }
        //        }
        //    }
        //}






        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Billing,Address,Zipcode,City,StarteDate,EndDate,OneTimePickUp,State,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }


        // GET: Customers/SetOneTime/5
        public ActionResult SetOneTime(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }

        // POST: Customers/SetOneTime/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetOneTime([Bind(Include = "CustomerId,FirstName,LastName,Billing,Address,Zipcode,City,StarteDate,EndDate,OneTimePickUp,State,UserId")] Customer customer)
        {

            //var CustomerLoggedIn = User.Identity.GetUserId();
            //customer = db.Customers.Where(u => u.ApplicationUserId == CustomerLoggedIn).FirstOrDefault();
            if (ModelState.IsValid)
            {

                db.Entry(customer).State = EntityState.Modified;
                customer.OneTimePickUpDay = customer.OneTimePickUp.ToString("dddd");
                db.SaveChanges();
                return RedirectToAction("CustomerProfile");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }

        public ActionResult CustomerProfile()
        {
            var CustomerLoggedIn = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(f => f.ApplicationUserId == CustomerLoggedIn).FirstOrDefault();
            return View(customer);
        }


        //GET: Customers/StartDate
        //public ActionResult StartDate(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);


        //POST:Customer/StartDate
        // public ActionResult StarteDate


        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        //public ActionResult ViewBill(int id)
        //{
        //    Customer customer = db.Customers.Find(id);
        //    View(customer.Billing);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        ////GET
        //public ActionResult SetPickUp()
        //{
        //    var id = User.Identity.GetUserId();
        //    Customer customer = new Customer();
        //    return View(customer);
        //}

        //POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SetPickup()
        //{

        //    var id = User.Identity.GetUserId();
        //    Customer customer = new Customer();
        //        if (ModelState.IsValid)
        //        {


        //        //db.Entry(customer).State = EntityState.Modified;
        //        //db.Entry(customer).State = EntityState.Modified;
        //        //db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
        //        return View(customer);

        //}


        // GET: Customers/Edit/5
        public ActionResult SetPickUp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPickUp([Bind(Include = "StartDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Pause(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pause([Bind(Include = "PauseStart,PauseEnd")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerProfile");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", customer.ApplicationUserId);
            return View(customer);
        }





    }
}
