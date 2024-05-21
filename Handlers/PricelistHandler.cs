using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using PricelistGenerator.Helpers;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Interfaces.Helpers;
using PricelistGenerator.Interfaces.Service;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Mappings;
using PricelistGenerator.Models.Menu;
using PricelistGenerator.Service;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class PricelistHandler : IPricelistHandler
{
    private IPricelistService _pricelistService;
    private ICsvHelper _csvHelper;
    private IPricelistHelper _pricelistHelper;
    
    public PricelistHandler(IPricelistService pricelistService, ICsvHelper csvHelper, IPricelistHelper pricelistHelper)
    {
        _pricelistService = pricelistService;
        _csvHelper = csvHelper;
        _pricelistHelper = pricelistHelper;
    }
    
    public void ExportProdaPricelist(List<int> menuChoice, NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        
        foreach (var region in menuChoice)
        {
            Pricelist pricelist = new Pricelist();

            var selectedRegion = _pricelistHelper.GetSelectedRegion(region);
            pricelist = CreateProdaPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = _csvHelper.ExportProdaPricelistToCsv(pricelist, spreadsheetFile, selectedRegion);

            AnsiConsole.WriteLine(csvFilePath.Equals("error")
                ? $"Error exporting {selectedRegion} Pricelist"
                : $"\nPricelist for {selectedRegion} has [bold green1] SUCCESSFULLY [/] been exported to {csvFilePath}");
        }
    }

    public void ExportPacePricelist(List<int> menuChoice, NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        foreach (var region in menuChoice)
        {
            Pricelist pricelist = new Pricelist();
            var selectedRegion = _pricelistHelper.GetSelectedRegion(region);
            pricelist = CreatePacePricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = _csvHelper.ExportPacePricelistToCsv(pricelist, spreadsheetFile, selectedRegion);

            AnsiConsole.WriteLine(csvFilePath.Equals("error")
                ? $"Error exporting {selectedRegion} Pricelist"
                : $"Pricelist for {selectedRegion} has been exported to {csvFilePath}");
        }
    }

    public Pricelist CreateProdaPricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, RegionMenuOptions selectedRegion)
    {
        return _pricelistService.CreateProdaPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
    }

    public Pricelist CreatePacePricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, RegionMenuOptions selectedRegion)
    {
        return _pricelistService.CreatePacePricelist(ndisSupportCatalogue, pricelist, selectedRegion);
    }
}
    