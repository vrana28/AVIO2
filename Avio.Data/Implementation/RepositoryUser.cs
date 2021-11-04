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

        public bool AlreadyExist(string username)
        {
            return context.User.Any(k => k.UserName == username);
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

        public User GetByUsernameAndPassword(User k)
        {
            return context.User.Single(u => u.UserName == k.UserName && u.Password == k.Password);
        }

        public void Update(User s)
        {
            throw new NotImplementedException();
        }
    }
}
