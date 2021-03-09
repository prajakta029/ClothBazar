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
    #region Singleton
    public class CategoriesService
    {
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;

            }
        }
        private static CategoriesService instance { get; set; }
        private CategoriesService()
        {
        }
#endregion

        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);
            }
        }
        public List<Category> GetCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Include(x => x.Products).ToList();
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(x => x.ImageURL != null).ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            using (var context=new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                /*   context.Entry(category).State = System.Data.Entity.EntityState.Deleted;*/
                var cc=context.Categories.Find(ID);
                context.Categories.Remove(cc);
                context.SaveChanges();
            }
        }
    }
}
