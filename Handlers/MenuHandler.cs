using System.ComponentModel;
using System.Reflection;
using DocumentFormat.OpenXml.Office.CustomUI;
using PricelistGenerator.Helpers;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Mappings;
using PricelistGenerator.Models.Menu;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class MenuHandler
{
    private string GetRegionEnumDescription(PricelistRegions option)
    {
        FieldInfo fieldInfo = option.GetType().GetField(option.ToString());
        DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
        return attribute == null ? option.ToString() : attribute.Description;
        
    }

    private string GetPricelistTypeEnumDescription(PricelistType pricelistType)
    {
        FieldInfo fieldInfo = pricelistType.GetType().GetField(pricelistType.ToString());
        DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
        return attribute == null ? pricelistType.ToString() : attribute.Description;
    }
    
    private static Dictionary<string, int> MainMenuOptionMap { get; } = new Dictionary<string, int>()
    {
        { "Generate Standard PRODA Pricelist", 1 },
        { "Generate Standard PACE Pricelist ", 2 },
        { "Generate Custom Pricelist", 3},
        { "Preview a Regions Pricelist", 4 },
        { "Pricelist Analysis", 5 },
        { "Exit", 6 },
    };
    
    private static Dictionary<string, int> RegionMenuMap { get; } = new Dictionary<string, int>()
    {
        { "ACT, NSW, QLD, VIC Price", 1 },
        { "NT, SA, TAS, WA Pricelist", 2 },
        { "Remote Pricelist", 3 },
        { "Very Remote Pricelist", 4 },
        { "Exit", 5 },
    };

    private static Dictionary<string, int> PricelistTypeMap { get; } = new Dictionary<string, int>()
    {
        { "PRODA Pricelist", 1 },
        { "PACE Pricelist", 2 },
        { "Custom Pricelist", 3 },
        { "Exit", 4 }
    };
    
    public void DisplayMainMenu(NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile catalogFile)
    {
        AnsiConsole.WriteLine("\nProgram Options:");
        foreach (var option in MainMenuOptionMap)
        {
            AnsiConsole.WriteLine($"[{option.Value}] {option.Key}");
        }
        
        var menuChoice = AnsiConsole.Prompt(new SelectionPrompt<int>().Title("\nWhat would you like to do?")
            .PageSize(MainMenuOptionMap.Count)
            .AddChoices(MainMenuOptionMap.Values));
        
        switch (menuChoice)
        {
            case 1: DisplayExportRegionMenu(1, ndisSupportCatalogue, catalogFile);
                break;
            case 2: DisplayExportRegionMenu(2, ndisSupportCatalogue, catalogFile);
                break;
            case 3: AnsiConsole.WriteLine("Generate Custom Pricelist");
                break;
            case 4: DisplayRegionPreviewMenu(ndisSupportCatalogue, catalogFile);
                break;
            case 5: AnsiConsole.WriteLine("\nExit");
                break;
            default: 
                AnsiConsole.WriteLine("Pricelist Analysis");
                break;
        }
        
    }
    
    private void DisplayExportRegionMenu(int choice, NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        AnsiConsole.WriteLine("\nPlease select the regions you wish to generate a pricelist for:");
        foreach (var option in RegionMenuMap)
        {
            AnsiConsole.WriteLine($"[{option.Value}] {option.Key}");
        }
        
        var menuChoice = AnsiConsole.Prompt(
            new MultiSelectionPrompt<int>()
                .Title("")
                .Required()
                .PageSize(RegionMenuMap.Count)
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to select a region, " +
                    "[green]<enter>[/] to accept)[/]" +
                    "\n        [italic]Toggle 5 only to go back to main menu[/]")
                .AddChoices(RegionMenuMap.Values));

        if (!menuChoice.Contains(5) && menuChoice.Count > 0)
        {
            var selectedRegions = "";
            foreach (var region in menuChoice)
            {
                if (Enum.IsDefined(typeof(PricelistRegions), region))
                {
                    var enumMember = (PricelistRegions)region;
                    var description = GetRegionEnumDescription(enumMember);
                    selectedRegions = selectedRegions + "," + $"[{description}]";
                }
            }
            
            selectedRegions = selectedRegions.TrimEnd(',', ' ');
            
            AnsiConsole.WriteLine($"This will generate a pricelist in the directory {spreadsheetFile.Path}" +
                                  $"\nFor the following regions {selectedRegions}");
            if (AnsiConsole.Confirm("Do you want to proceed?"))
            {
                PricelistHandler pricelistHandler = new PricelistHandler();
                switch (choice)
                {
                    case 1:
                        pricelistHandler.ExportProdaPricelist(menuChoice, ndisSupportCatalogue, spreadsheetFile);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    case 2:
                        pricelistHandler.ExportPacePricelist(menuChoice, ndisSupportCatalogue, spreadsheetFile);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    case 5:
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                }
            }
            else
            {
                DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
            }
        }
        else
        {
            DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
        }
    }

    public void DisplayRegionPreviewMenu(NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        PreviewHandler previewHandler = new PreviewHandler();
        PricelistHelper pricelistHelper = new PricelistHelper();
        AnsiConsole.WriteLine("\nRegion Options:");
        foreach (var option in RegionMenuMap)
        {
            AnsiConsole.WriteLine($"[{option.Value}] {option.Key}");
        }
        
        var menuChoice = AnsiConsole.Prompt(new SelectionPrompt<int>().Title("\nPlease select the region you wish " +
                                                                             "to preview the pricelist for?")
            .PageSize(RegionMenuMap.Count)
            .AddChoices(RegionMenuMap.Values));
        
        if (menuChoice != 5 && menuChoice > 0)
        {

            // foreach (var option in PricelistTypeMap)
            // {
            //     AnsiConsole.WriteLine($"[{option.Value}] {option.Key}");
            // }
            
            var pricelistChoice = AnsiConsole.Prompt(new SelectionPrompt<int>().Title("\nPlease select the type of pricelist"+
                    " you wish to preview" + "\n Proda(1) Pace(2) Custom(3) Exit(4)")
                .PageSize(PricelistTypeMap.Count)
                .AddChoices(PricelistTypeMap.Values));

            switch (pricelistChoice)
            {
                case 4:
                    DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                    break;
                default:
                    break;
            }
            
            var selectedRegionDescription = "";
            if (Enum.IsDefined(typeof(PricelistRegions), menuChoice))
            {
                var regionEnum = (PricelistRegions)menuChoice;
                selectedRegionDescription = GetRegionEnumDescription(regionEnum);

            }
            
            var selectedPricelistType = "";
            if (Enum.IsDefined(typeof(PricelistType), pricelistChoice))
            {
                var pricelistEnum = (PricelistType)pricelistChoice;
                selectedPricelistType = GetPricelistTypeEnumDescription(pricelistEnum);
            }
            
            AnsiConsole.WriteLine($"This will display a preview of the pricelist type [underline]{selectedPricelistType}[/] for" +
                                  $"\nregion [underline]{selectedRegionDescription}[/]");
            
            if (AnsiConsole.Confirm("Do you want to proceed?"))
            {
                switch (pricelistChoice)
                {
                    case 1:
                        previewHandler.RenderProdaPricelist(menuChoice, ndisSupportCatalogue);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    case 2:
                        previewHandler.RenderPacePricelist(menuChoice, ndisSupportCatalogue);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    case 5:
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                }
            }
            else
            {
                DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
            }
        }
        else
        {
            DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
        }
    }
    
}