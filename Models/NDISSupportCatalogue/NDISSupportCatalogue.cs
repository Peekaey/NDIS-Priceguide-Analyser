using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricelistGenerator.Models
{
    public class NdisSupportCatalogue
    {
        public List<NdisSupportCatalogueSupportItem> NdisSupportCatalogueSupportItems { get; set; }
        
        public NdisSupportCatalogue()
        {
            NdisSupportCatalogueSupportItems = new List<NdisSupportCatalogueSupportItem>();
        }
    }
}
