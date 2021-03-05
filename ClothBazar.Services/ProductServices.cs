using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entities;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class ProductServices
    {
       
        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Find(ID);
            }
        }
        public List<Product> GetProducts()
        {

            using (var context = new CBContext())
            {
                return context.Products.Include(x=>x.Category).ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                /*   context.Entry(category).State = System.Data.Entity.EntityState.Deleted;*/
                var cc = context.Products.Find(ID);
                context.Products.Remove(cc);
                context.SaveChanges();
            }
        }
    }
}
