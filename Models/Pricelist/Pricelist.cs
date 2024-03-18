using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricelistGenerator.Models
{
    public class Pricelist
    {
        public List<PricelistSupportItem> PricelistSupportItems { get; set; }
        
        public Pricelist()
        {
            PricelistSupportItems = new List<PricelistSupportItem>();
        }
    }
}
