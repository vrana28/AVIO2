using AvioProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avio.Data
{
    public interface IRepositoryReservation:IRepository<Reservation>
    {
        List<Reservation> GetAllWithId(int id);
        void AddReservation (List<Reservation> listOfReservation, List<Reservation> oldReservation);
        //void izbrisiSvePSala(int id, List<Projekcija> projekcije);
        //void izbrisiSvePFilm(int id, List<Projekcija> projekcije);
    }
}
