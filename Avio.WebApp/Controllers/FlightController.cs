using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avio.Data.UnitOfWork;
using Avio.WebApp.Filters;
using Avio.WebApp.Models;
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
        public IActionResult Index()
        {
            return View("Create");
        }

        [LoggedInUser]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFlightViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new NullReferenceException();
                if (viewModel.Flight.Departure == 0 || viewModel.Flight.Arrival == 0 || viewModel.Flight.DateOfFlight == null
                    || viewModel.Flight.NumberOfTransfers < 0 || viewModel.Flight.Seats <= 0 || viewModel.Flight.Departure == viewModel.Flight.Arrival) throw new Exception();
                unitOfWork.Flight.Add(viewModel.Flight);
                unitOfWork.Commit();
                return RedirectToAction("Create", "Flight");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Bad input!");
                return RedirectToAction("Create");

            }
        }

        
       
    }
}