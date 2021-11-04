using Avio.Domain;
using System;

namespace Avio.Data
{
    public interface IRepositoryUser:IRepository<User>
    {
        User GetByUsernameAndPassword(User k);
        bool AlreadyExist(string username);
        
    }
}
