using Avio.Data.Implementation;
using AvioProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avio.Data.UnitOfWork
{
    public class AvioUnitOfWork : IUnitOfWork
    {
        private AvioContext context;

        public AvioUnitOfWork (AvioContext context) {
            this.context = context;
            Flight = new RepositoryFlight(this.context);
            User = new RepositoryUser(this.context);
            Reservation = new RepositoryReservation(this.context);
        }

        public IRepositoryFlight Flight { get; set ; }
        public IRepositoryReservation Reservation { get; set; }
        public IRepositoryUser User { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
