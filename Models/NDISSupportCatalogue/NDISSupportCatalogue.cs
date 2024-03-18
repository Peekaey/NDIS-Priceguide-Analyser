using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricelistGenerator.Models
{
    public class NDISSupportCatalogue
    {
        public List<NDISSupportCatalogueSupportItem> NDISSupportCatalogueSupportItems { get; set; }
        
        public NDISSupportCatalogue()
        {
            NDISSupportCatalogueSupportItems = new List<NDISSupportCatalogueSupportItem>();
        }
    }
}
