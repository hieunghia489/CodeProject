using BusinessObject.DataTransferObject;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomerRepository
{
    public interface ICustomerRepository : IRepositoryCRUD<Customer>
    {
        void DeleteById(int id);
        Customer? GetById(int? id);
        Customer? Login(AccountDTO account);
        Customer? GetByEmail(string email);
    }
}
