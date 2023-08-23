using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SupplierRepository
{
    public class SupplierRepository : ISupplierRepository
    {
        public IEnumerable<Supplier> GetAll()
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Suppliers.ToList();
        }

        public Supplier? GetById(int? id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Suppliers.SingleOrDefault(c => c.SupplierId == id);
        }

        public void Save(Supplier entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Suppliers.Add(entity);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            var entity = context.Suppliers.SingleOrDefault(c => c.SupplierId == id);
            if (entity != null)
            {
                context.Suppliers.Remove(entity);
            }
            context.SaveChanges();
        }

        public void Update(Supplier entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Entry<Supplier>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public Supplier? GetById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Suppliers.SingleOrDefault(c => c.SupplierId == id);
        }
    }
}
