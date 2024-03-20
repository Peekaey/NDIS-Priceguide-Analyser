using System.ComponentModel;
using System.Reflection;
using DocumentFormat.OpenXml.Office.CustomUI;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Mappings;
using PricelistGenerator.Models.Menu;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class MenuHandler
{
    private string GetEnumDescription(PricelistRegions option)
    {
        FieldInfo fieldInfo = option.GetType().GetField(option.ToString());
        DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
        return attribute == null ? option.ToString() : attribute.Description;
        
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
    
    public void DisplayMainMenu(NDISSupportCatalogue ndisSupportCatalogue, SpreadsheetFile catalogFile)
    {
        AnsiConsole.WriteLine("\nProgram Options:");
        foreach (var option in MainMenuOptionMap)
        {
            AnsiConsole.WriteLine($"[{option.Value}] {option.Key}");
        }
        
        var MenuChoice = AnsiConsole.Prompt(new SelectionPrompt<int>().Title("\nWhat would you like to do?")
            .PageSize(MainMenuOptionMap.Count)
            .AddChoices(MainMenuOptionMap.Values));
        
        switch (MenuChoice)
        {
            case 1: DisplayRegionMenu(1, ndisSupportCatalogue, catalogFile);
                break;
            case 2: DisplayRegionMenu(2, ndisSupportCatalogue, catalogFile);
                break;
            case 3: AnsiConsole.WriteLine("Generate Custom Pricelist");
                break;
            case 4: DisplayRegionMenu(3, ndisSupportCatalogue, catalogFile);
                break;
            case 5: AnsiConsole.WriteLine("\nExit");
                break;
            default: 
                AnsiConsole.WriteLine("Pricelist Analysis");
                break;
        }
        
    }
    
    private void DisplayRegionMenu(int Choice, NDISSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {
        AnsiConsole.WriteLine("\nPlease select the regions you wish to generate a pricelist for:");
        foreach (var option in RegionMenuMap)
        {
            AnsiConsole.WriteLine($"[{option.Value}] {option.Key}");
        }
        
        var MenuChoice = AnsiConsole.Prompt(
            new MultiSelectionPrompt<int>()
                .Title("")
                .Required()
                .PageSize(RegionMenuMap.Count)
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to select a region, " +
                    "[green]<enter>[/] to accept)[/]" +
                    "\n        [italic]Toggle 5 only to go back to main menu[/]")
                .AddChoices(RegionMenuMap.Values));

        if (!MenuChoice.Contains(5) && MenuChoice.Count > 0)
        {
            var selectedRegions = "";
            foreach (var region in MenuChoice)
            {
                if (Enum.IsDefined(typeof(PricelistRegions), region))
                {
                    var enumMember = (PricelistRegions)region;
                    var description = GetEnumDescription(enumMember);
                    selectedRegions = selectedRegions + "," + $"[{description}]";
                }
            }
            
            selectedRegions = selectedRegions.TrimEnd(',', ' ');
            
            AnsiConsole.WriteLine($"This will generate a pricelist in the directory {spreadsheetFile.Path}" +
                                  $"\nFor the following regions {selectedRegions}");
            if (AnsiConsole.Confirm("Do you want to proceed?"))
            {
                PricelistHandler pricelistHandler = new PricelistHandler();
                switch (Choice)
                {
                    case 1:
                        pricelistHandler.CreatePRODAPricelist(MenuChoice, ndisSupportCatalogue, spreadsheetFile);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    case 2:
                        pricelistHandler.CreatePACEPricelist(MenuChoice, ndisSupportCatalogue, spreadsheetFile);
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

    public void DisplayRegionPreviewMenu()
    {
        
    }
    
}