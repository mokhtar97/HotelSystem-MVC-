using Day8.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Day8.Controllers
{
    [Authorize(Roles = "Admin")]
   //[AllowAnonymous]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string rolename)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
            IdentityRole role = new IdentityRole();
            role.Name = rolename;
            manager.Create(role);
            return RedirectToAction("Index");
        }
    }
}