using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FlowerBouquetRepository
{
    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {
        public IEnumerable<FlowerBouquet> GetAll()
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.FlowerBouquets.Include(c=>c.Category).Include(s=>s.Supplier).ToList();
        }

        public FlowerBouquet? GetById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.FlowerBouquets.SingleOrDefault(c => c.FlowerBouquetId == id);
        }

        public void Save(FlowerBouquet entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.FlowerBouquets.Add(entity);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            var entity = context.FlowerBouquets.SingleOrDefault(c => c.FlowerBouquetId == id);
            if (entity != null)
            {
                context.FlowerBouquets.Remove(entity);
            }
            context.SaveChanges();
        }

        public void Update(FlowerBouquet entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Entry<FlowerBouquet>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
