using AvioProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avio.Data.Implementation
{
    public class RepositoryReservation : IRepositoryReservation
    {
        private AvioContext context;

        public RepositoryReservation(AvioContext context)
        {
            this.context = context;
        }

        public void Add(Reservation s)
        {
            context.Add(s);
        }

        public void AddReservation(List<Reservation> listOfReservation, List<Reservation> oldReservation)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Delete(Reservation s)
        {
            throw new NotImplementedException();
        }

        public Reservation Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetAllWithId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Reservation s)
        {
            throw new NotImplementedException();
        }
    }
}
