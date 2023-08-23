using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.OrderDetailRepository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.OrderDetails.Remove(orderDetail);
            context.SaveChanges();
        }

        public IEnumerable<OrderDetail> GetByOrderId(int orderId)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.OrderDetails.Where(o => o.OrderId == orderId).Include(c=>c.FlowerBouquet).ToList();
        }

        public OrderDetail? GetByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.OrderDetails.Where(o => o.OrderId == orderId && o.FlowerBouquetId == flowerBouquetId).FirstOrDefault();
        }

        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
