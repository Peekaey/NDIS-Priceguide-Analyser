using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Menu;

namespace PricelistGenerator.Interfaces.Handler;

public interface IPricelistHandler
{
    public void ExportProdaPricelist(List<int> menuChoice, NdisSupportCatalogue ndisSupportCatalogue, 
        SpreadsheetFile spreadsheetFile);
    
    public void ExportPacePricelist(List<int> menuChoice, NdisSupportCatalogue ndisSupportCatalogue, 
        SpreadsheetFile spreadsheetFile);
    
    public Pricelist CreateProdaPricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, 
        RegionMenuOptions selectedRegion);
    
    public Pricelist CreatePacePricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist,
        RegionMenuOptions selectedRegion);
}