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
    public static void CreatePRODAPricelist(List<int> MenuChoice, NDISSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        Pricelist pricelist = new Pricelist();
        
        foreach (var region in MenuChoice)
        {
            var selectedRegion = GetMenuChoiceRegionSelection(region);
            pricelist = PricelistService.CreatePRODAPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = CSVHelper.ExportPRODAPricelistToCSV(pricelist, spreadsheetFile, selectedRegion);
            
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

    public static void CreatePACEPricelist(List<int> MenuChoice, NDISSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        Pricelist pricelist = new Pricelist();

        foreach (var region in MenuChoice)
        {
            var selectedRegion = GetMenuChoiceRegionSelection(region);
            pricelist = PricelistService.CreatePACEPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
            var csvFilePath = CSVHelper.ExportPACEPricelistToCSV(pricelist, spreadsheetFile, selectedRegion);
            
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


    public static String GetMenuChoiceRegionSelection(int MenuChoice)
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
    