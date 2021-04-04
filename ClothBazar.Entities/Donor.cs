using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClothBazar.Entities
{
    public class Donor
    {
        [Key]
        public int donarId { get; set; }
        public string donorUsername { get; set; }
        public string donorAddress { get; set; }
        public int itemsNumber { get; set; }
        public string dDescription { get; set; }
    }
}
