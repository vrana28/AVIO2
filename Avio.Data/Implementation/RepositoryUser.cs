using Avio.Domain;
using AvioProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avio.Data.Implementation
{
    public class RepositoryUser : IRepositoryUser
    {
        private AvioContext context;

        public RepositoryUser (AvioContext context)
        {
            this.context = context;
        }

        public void Add(User s)
        {
            context.User.Add(s);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(User s)
        {
            context.User.Remove(s);
        }

        public User Find(int id)
        {
            return context.User.Find(id);
        }

        public List<User> GetAll()
        {
            return context.User.ToList();
        }

        public void Update(User s)
        {
            throw new NotImplementedException();
        }
    }
}
