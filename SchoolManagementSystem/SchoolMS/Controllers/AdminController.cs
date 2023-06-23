using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SchoolMS.Data;
using SchoolMS.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SchoolMS.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SchoolMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                // Session exists, continue with the index page
                string username = HttpContext.Session.GetString("Username");

                var admin = _context.Administrators.FirstOrDefault(a => a.Username == username);

                if (admin != null)
                {
                    ViewBag.AdminName = admin.Name;
                    ViewBag.AdminEmail = admin.Email;

                    return View();
                }
            }

            // Session doesn't exist or admin not found, redirect to the login page
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        //[CustomAuthenticationFilter]
        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Administrators.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(admin);
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Administrators.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Password", password);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "bad data";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");
        }

    }
}
