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
    public class DonorServices
    {
        #region Singleton
        public static DonorServices Instance
        {
            get
            {
                if (instance == null) instance = new DonorServices();
                return instance;
            }
        }
        private static DonorServices instance { get; set; }
        private DonorServices()
        {

        }
        #endregion
        public List<Donor> GetDetails(string search)
        {

            using (var context = new CBContext())
            {
                return context.Donors.Where(x => x.donorUsername == search).ToList();
            }
        }
        public void SaveDonor(Donor donor)
        {
            using (var context = new CBContext())
            {
                context.Donors.Add(donor);
                context.SaveChanges();
            }
        }
    }
    
}
