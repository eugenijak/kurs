using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kurs.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Kurs.ViewModel;
using System.Linq;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Kurs.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;

        public HomeController(UsersContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AllViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.email == model.user.email);
                if (user == null)
                {
                    user = new User {
                        name = model.user.name,
                        email = model.user.email,
                        password = model.user.password
                    };
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    CurrentUser.currentuser = user;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Erroremail = "Пользователь с таким email уже сущесвует";
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AllViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.email == model.login.email && u.password == model.login.password);
                if (user != null)
                {
                    await Authenticate(model.login.email);
                    CurrentUser.currentuser = user;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = "Неправильный email или пароль";
            }
            return View("Index",model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            CurrentUser.currentuser = null;
            return RedirectToAction("Index", "Home");
        }

        public User Get()
        {
            return CurrentUser.currentuser;
        }

        [HttpPost]
        public async Task<IActionResult> Update(AllViewModel model)
        {
           if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.email == CurrentUser.currentuser.email);
                user.password = model.pass.password;
                db.Update(user);
                db.SaveChanges();
                ViewBag.Pass = "Пароль изменен";
            }
            ViewBag.Collections = GetGPX();
            return View("Room");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(AllViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.gpx != null)
                {
                    var gpx = new UsersGPX
                    {
                        Id_user = CurrentUser.currentuser.Id,
                        name = model.gpx.name,
                     //   filepath = "C:\\GPX files\\" + model.gpx.filepath,
                        price = model.gpx.price
              
                    };
                    db.Gpx.Add(gpx);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Room", "Home");
                }
            }
            ViewBag.Collections = GetGPX();
            return View("Room", model);
        }

        public IActionResult Delete(int id)
        {
            UsersGPX gpx = db.Gpx.FirstOrDefault(x => x.Id == id);
            if (gpx == null)
            {
                return NotFound();
            }
            db.Gpx.Remove(gpx);
            db.SaveChanges();
            ViewBag.Collections = GetGPX();
            return View("Room");
        }

        public List<UsersGPX> GetGPX()
        {
            return db.Gpx.Where(x => x.Id_user == CurrentUser.currentuser.Id).ToList();
        }

        public string FileGPX() 
        {
            if (CurrentUser.path != null)
            {
                XDocument doc = XDocument.Load(CurrentUser.path);
                return doc.ToString();
            }
            else return null;
        }


        public IActionResult Room()
        {
            ViewBag.Collections = GetGPX();
            return View();
        }

        public IActionResult CurrentGPX(int id)
        {
            UsersGPX gpx = db.Gpx.FirstOrDefault(x => x.Id == id);
            CurrentUser.path = gpx.name;

            return View("Index");
        }

        public IActionResult Gpx()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Location()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
