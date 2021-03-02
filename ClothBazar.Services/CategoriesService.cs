using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entities;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
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
                return context.Categories.ToList();
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

        public void DeleteCategory(Category category)
        {
            using (var context = new CBContext())
            {
                /*   context.Entry(category).State = System.Data.Entity.EntityState.Deleted;*/
                var cc=context.Categories.Find(category.ID);
                context.Categories.Remove(cc);
                context.SaveChanges();
            }
        }
    }
}
