using System;
using System.Collections.Generic;
using System.Text;

namespace Avio.Data
{
    public interface IRepository<T>
    {
        void Add(T s);
        List<T> GetAll();
        T Find(int id);
        void Delete(T s);
        void Update(T s);
        void Commit();
    }
}
