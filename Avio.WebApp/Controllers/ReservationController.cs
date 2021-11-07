using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avio.Data.UnitOfWork;
using Avio.Domain;
using Avio.WebApp.Models;
using AvioProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avio.WebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ReservationController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }



        public ActionResult Create(int id)
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            Flight f = unitOfWork.Flight.Find(id);

            Reservation r = new Reservation
            {
                Flight = f,
                User = unitOfWork.User.Find((int)HttpContext.Session.GetInt32("userid")),
            };
            ViewBag.FreeSeats = r.Flight.Seats;
            MakeReservation model = new MakeReservation
            {
                Reservation = r,
                FlightId = r.Flight.FlightId,
                UserId = r.User.UserId
            };
            return View("Create",model);
        }

        [HttpPost]
        public ActionResult Create(MakeReservation model)
        {
            try
            {
                if (model.Reservation.ReservedSeats > 10) throw new Exception("Ne mozete vise od 4 karte");

                Reservation r = new Reservation
                {
                    Flight = unitOfWork.Flight.Find(model.FlightId),
                    User = unitOfWork.User.Find(model.UserId),
                    FlightId = model.FlightId,
                    UserId = model.UserId,
                    ReservedSeats = model.Reservation.ReservedSeats
                };

                if (r.ReservedSeats > r.Flight.Seats) throw new Exception();
                
                unitOfWork.Reservation.Add(r);
                unitOfWork.Flight.UpdateSeats(r.Flight,r.ReservedSeats);

                unitOfWork.Commit();
                return RedirectToAction("Search", "Flight");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Greska");
                return RedirectToAction("Create");
            }
        }

        public ActionResult Index() {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            List<Reservation> model = unitOfWork.Reservation.GetAll();
            model = model.Where(m=> m.UserId == (int)HttpContext.Session.GetInt32("userid")).ToList();

            List<MakeReservation> lista = new List<MakeReservation>();
            foreach (Reservation r in model) {
                r.Flight = unitOfWork.Flight.Find(r.FlightId);
                MakeReservation r1 = new MakeReservation
                {
                    Reservation = r,
                    FlightId = r.FlightId,
                    UserId = r.UserId
                };
                lista.Add(r1);
            }
            
            return View(lista);
        }

        public ActionResult Index2()
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");
            List<Reservation> model = unitOfWork.Reservation.GetAll();

            List<MakeReservation> lista = new List<MakeReservation>();
            foreach (Reservation r in model)
            {
                r.Flight = unitOfWork.Flight.Find(r.FlightId);
                r.User = unitOfWork.User.Find(r.UserId);
                MakeReservation r1 = new MakeReservation
                {
                    Reservation = r,
                    FlightId = r.FlightId,
                    UserId = r.UserId
                };
                lista.Add(r1);
            }

            return View(lista);
        }

        public ActionResult Update(int id)
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.TypeOfUser = HttpContext.Session.GetString("typeOfUser");

            Reservation r = unitOfWork.Reservation.Find(id);
            unitOfWork.Reservation.Update(r);
            unitOfWork.Commit();
            return RedirectToAction("Index2");
        }

    }
}