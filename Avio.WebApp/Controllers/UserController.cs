using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avio.Data.UnitOfWork;
using Avio.Domain;
using Avio.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avio.WebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        [ActionName("Login")]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                
                User u = unitOfWork.User.GetByUsernameAndPassword(new User
                {
                    UserName = model.Username,
                    Password = model.Password
                });
                //ViewBag.IsLoggedIn = true;
                HttpContext.Session.SetInt32("userid", u.UserId);
                HttpContext.Session.SetString("username", u.UserName);
                HttpContext.Session.SetString("typeOfUser", u.TypeOfUser.ToString());
                HttpContext.Session.Set("user", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(u));
                // serijalizacija celog objekta u ime sesije
                if ((int)u.TypeOfUser == 0)
                {
                    return RedirectToAction("NewUser", "User");
                }
                else if ((int)u.TypeOfUser == 1)
                {
                    return RedirectToAction("Create", "Flight");
                }
                else
                {
                    return RedirectToAction("Search", "Flight");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Pogresna sifra ili username.");
                return View();
            }
        }

        [ActionName("NewUser")]
        public ActionResult NewUser()
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            return View("NewUser");
        }

        [ActionName("NewUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            try
            {
                if (model.FirstName == null || model.LastName == null || model.Username == null || model.Password == null ) throw new Exception();
                bool exist = unitOfWork.User.AlreadyExist(model.Username);
                if (exist) throw new Exception();
                User u = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Username,
                    Password = model.Password,
                    TypeOfUser = model.TypeOfUser
                };
                unitOfWork.User.Add(u);
                unitOfWork.Commit();
                
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Losi parametri ili korisnik vec postoji");
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        

    }
}