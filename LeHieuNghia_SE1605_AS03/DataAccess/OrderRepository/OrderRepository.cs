using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetAll()
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Orders.Include(c=>c.Customer).ToList();
        }

        public Order? GetById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Orders.SingleOrDefault(c => c.OrderId == id);
        }

        public void Save(Order entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Orders.Add(entity);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            var entity = context.Orders.SingleOrDefault(c => c.OrderId == id);
            if (entity != null)
            {
                context.Orders.Remove(entity);
            }
            context.SaveChanges();
        }

        public void Update(Order entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Entry<Order>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
       /* public IEnumerable<Order> GetAllOrderByCustomerID(int id)
        {
            IEnumerable<Order> orders = IEn;
            var listOrder = GetAll();
            foreach (var item in listOrder)
            {
                if (item.CustomerId == id)
                {
                    orders().Add(item);
                }
            }

        }*/
    }
}
