using System;
using System.Collections.Generic;
using System.Text;

namespace Avio.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepositoryFlight Flight{ get; set; }
        public IRepositoryReservation Reservation { get; set; }
        public IRepositoryUser User { get; set; }
        void Commit();

    }
}
