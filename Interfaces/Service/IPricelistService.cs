using PricelistGenerator.Models;
using PricelistGenerator.Models.Menu;

namespace PricelistGenerator.Interfaces.Service;

public interface IPricelistService
{
    public Pricelist CreateProdaPricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, 
        RegionMenuOptions selectedRegion);
    
    public Pricelist CreatePacePricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, 
        RegionMenuOptions selectedRegion);
}