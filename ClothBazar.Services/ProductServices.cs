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
        #region Singleton
        public static ProductServices Instance
        {
            get
            {
                if (instance == null) instance = new ProductServices();
                return instance;
            }
        }
        private static ProductServices instance { get; set; }
        private ProductServices()
        {

        }
        #endregion

        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }
        public List<Product> GetProducts()
        {

            using (var context = new CBContext())
            {
                return context.Products.Include(x=>x.Category).ToList();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => IDs.Contains(x.ID)).ToList();
            }
        }
        public List<Product> GetMens()
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.Name=="Mens").ToList();
            }
        }
        public List<Product> GetWomens()
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.Name == "Womens").ToList();
            }
        }
        public List<Product> GetKids()
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.Name == "Kids").ToList();
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

        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.ID == categoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                //if (minimumPrice.HasValue)
                //{
                //    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                //}

                //if (maximumPrice.HasValue)
                //{
                //    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                //}

                //if (sortBy.HasValue)
                //{
                //    switch (sortBy.Value)
                //    {
                //        case 2:
                //            products = products.OrderByDescending(x => x.ID).ToList();
                //            break;
                //        case 3:
                //            products = products.OrderBy(x => x.Price).ToList();
                //            break;
                //        default:
                //            products = products.OrderByDescending(x => x.Price).ToList();
                //            break;
                //    }
                //}

                return products.ToList();
            }
        }
    }
}