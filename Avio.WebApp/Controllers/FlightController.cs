using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avio.Data.UnitOfWork;
using Avio.Domain;
using Avio.WebApp.Filters;
using Avio.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avio.WebApp.Controllers
{
    public class FlightController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public FlightController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        [ActionName("Create")]
        public IActionResult Create ()
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            return View("Create");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFlightViewModel viewModel)
        {
            try
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.Username = HttpContext.Session.GetString("username");
                ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
                if (viewModel == null) throw new NullReferenceException();
                if (viewModel.Flight.Departure == 0 || viewModel.Flight.Arrival == 0 || viewModel.Flight.DateOfFlight == null
                    || viewModel.Flight.NumberOfTransfers < 0 || viewModel.Flight.Seats <= 0 || viewModel.Flight.Departure == viewModel.Flight.Arrival) throw new Exception();
                unitOfWork.Flight.Add(viewModel.Flight);
                unitOfWork.Commit();
                return RedirectToAction("Index", "Flight");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Bad input!");
                return RedirectToAction("Create");

            }
        }

        public ActionResult Index()
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            List<Flight> model = unitOfWork.Flight.GetAll();
            model = model.OrderBy(x => x.DateOfFlight).ToList();
            return View(model);
        }

        public ActionResult Update(int id)
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");

            Flight f = unitOfWork.Flight.Find(id);
            unitOfWork.Flight.Update(f);
            unitOfWork.Commit();
            return View("Index");
        }
    }
}