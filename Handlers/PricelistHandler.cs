using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using PricelistGenerator.Helpers;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Mappings;
using PricelistGenerator.Service;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class PricelistHandler
{
    public void ExportProdaPricelist(List<int> menuChoice, NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        Helpers.CsvHelper csvHelper = new Helpers.CsvHelper();
        PricelistService pricelistService = new PricelistService();
        PricelistHelper pricelistHelper = new PricelistHelper();

        foreach (var region in menuChoice)
        {
            Pricelist pricelist = new Pricelist();

            var selectedRegion = pricelistHelper.GetMenuChoiceRegionSelection(region);
            pricelist = CreateProdaPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = csvHelper.ExportProdaPricelistToCsv(pricelist, spreadsheetFile, selectedRegion);
            
            if (csvFilePath.Equals("error"))
            {
                AnsiConsole.WriteLine($"Error exporting {selectedRegion} Pricelist");
            }
            else
            {
                AnsiConsole.WriteLine($"\nPricelist for {selectedRegion} has [bold green1] SUCCESSFULLY [/] been exported to {csvFilePath}");
            }
        }
    }

    public void ExportPacePricelist(List<int> menuChoice, NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        Helpers.CsvHelper csvHelper = new Helpers.CsvHelper();
        PricelistService pricelistService = new PricelistService();
        PricelistHelper pricelistHelper = new PricelistHelper();

        foreach (var region in menuChoice)
        {
            Pricelist pricelist = new Pricelist();
            var selectedRegion = pricelistHelper.GetMenuChoiceRegionSelection(region);
            pricelist = CreatePacePricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = csvHelper.ExportPacePricelistToCsv(pricelist, spreadsheetFile, selectedRegion);
            
            if (csvFilePath.Equals("error"))
            {
                AnsiConsole.WriteLine($"Error exporting {selectedRegion} Pricelist");
            }
            else
            {
                AnsiConsole.WriteLine($"Pricelist for {selectedRegion} has been exported to {csvFilePath}");
            }
        }
    }

    public Pricelist CreateProdaPricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, string selectedRegion)
    {
        PricelistService pricelistService = new PricelistService();
        return pricelistService.CreateProdaPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
    }

    public Pricelist CreatePacePricelist(NdisSupportCatalogue ndisSupportCatalogue, Pricelist pricelist, string selectedRegion)
    {
        PricelistService pricelistService = new PricelistService();
        return pricelistService.CreatePacePricelist(ndisSupportCatalogue, pricelist, selectedRegion);
    }
    
    
}
    