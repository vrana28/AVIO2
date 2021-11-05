using Avio.Domain;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;

namespace Avio.Data
{
    public interface IRepositoryFlight:IRepository<Flight>
    {

        List<Flight> Search(Expression<Func<Flight, bool>> p);
        void UpdateSeats(Flight f, int reservedSeats);
    }
}
