using Avio.Domain;
using AvioProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Avio.Data.Implementation
{
    public class RepositoryFlight : IRepositoryFlight
    {
        private AvioContext context;

        public RepositoryFlight(AvioContext context)
        {
            this.context = context;
        }

        public void Add(Flight s)
        {
            context.Flight.Add(s);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Flight s)
        {
            context.Flight.Remove(s);
        }

        public Flight Find(int id)
        {
            return context.Flight.Find(id);
        }

        public List<Flight> GetAll()
        {
            return context.Flight.ToList();
        }

        public List<Flight> Search(Expression<Func<Flight, bool>> p)
        {
            throw new NotImplementedException();
        }

        public void Update(Flight s)
        {
            context.Flight.Find(s.FlightId).Canceled = true;
        }
    }
}
