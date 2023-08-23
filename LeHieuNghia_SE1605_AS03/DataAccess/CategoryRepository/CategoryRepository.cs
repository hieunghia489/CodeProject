using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAll()
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            return context.Categories.SingleOrDefault(c => c.CategoryId == id);
        }

        public void Save(Category entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var context = new FUFlowerBouquetManagementContext();
            var entity = context.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (entity != null)
            {
                context.Categories.Remove(entity);
            }
            context.SaveChanges();
        }

        public void Update(Category entity)
        {
            using var context = new FUFlowerBouquetManagementContext();
            context.Entry<Category>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
