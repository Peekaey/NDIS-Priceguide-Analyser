using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Menu;

namespace PricelistGenerator.Interfaces.Helpers;

public interface ICsvHelper
{
    public string ExportProdaPricelistToCsv(Pricelist pricelist, SpreadsheetFile spreadsheetFile,
        RegionMenuOptions selectedRegion);
    public string ExportPacePricelistToCsv(Pricelist pricelist, SpreadsheetFile spreadsheetFile,
        RegionMenuOptions selectedRegion);
}