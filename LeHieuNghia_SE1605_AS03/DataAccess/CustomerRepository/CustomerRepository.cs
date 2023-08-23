using BusinessObject.DataTransferObject;
using BusinessObject.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccess.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> GetAll()
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Customers.ToList();
        }

        public Customer? GetById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Customers.SingleOrDefault(c => c.CustomerId == id);
        }
        public Customer? GetByEmail(string email)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Customers.SingleOrDefault(c => c.Email.Equals(email));
        }

        public void Save(Customer customer)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            var customer = context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if(customer != null)
            {
                context.Customers.Remove(customer);
            }
            context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public Customer? GetById(int? id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Customers.SingleOrDefault(c => c.CustomerId == id);
        }

        public Customer? Login(AccountDTO account)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Customers.SingleOrDefault(m => m.Email.Equals(account.Email) && m.Password.Equals(account.Password));
        }
    }
}
