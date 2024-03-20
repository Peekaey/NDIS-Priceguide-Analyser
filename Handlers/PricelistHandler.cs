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
    public void CreatePRODAPricelist(List<int> MenuChoice, NDISSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        CSVHelper csvHelper = new CSVHelper();
        PricelistService pricelistService = new PricelistService();
        
        foreach (var region in MenuChoice)
        {
            Pricelist pricelist = new Pricelist();

            var selectedRegion = GetMenuChoiceRegionSelection(region);
            pricelist = pricelistService.CreatePRODAPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = csvHelper.ExportPRODAPricelistToCSV(pricelist, spreadsheetFile, selectedRegion);
            
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

    public void CreatePACEPricelist(List<int> MenuChoice, NDISSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        Pricelist pricelist = new Pricelist();
        CSVHelper csvHelper = new CSVHelper();


        foreach (var region in MenuChoice)
        {
            PricelistService pricelistService = new PricelistService();
            var selectedRegion = GetMenuChoiceRegionSelection(region);
            pricelist = pricelistService.CreatePACEPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = csvHelper.ExportPACEPricelistToCSV(pricelist, spreadsheetFile, selectedRegion);
            
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


    public String GetMenuChoiceRegionSelection(int MenuChoice)
    {
        switch ((PricelistRegions)MenuChoice)
        {
            case PricelistRegions.NSW:
                return "ACT";
            case PricelistRegions.NT:
                return "NT";
            case PricelistRegions.Remote:
                return "Remote";
            case PricelistRegions.VeryRemote:
                return "VeryRemote";
            default:
                return "Invalid Choice";
        }
    }
  
    
}
    