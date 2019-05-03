using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                
                var Roles = context.Roles.ToList();
                View(Roles);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                
                var Roles = context.Roles.ToList();
                View(Roles);
                return RedirectToAction("Index", "Home");
            }                   
        }
    }
}