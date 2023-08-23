using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryCRUD<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Update(T entity);
        void DeleteById(int id);
        void Save(T entity);
    }
}
