using System.ComponentModel;
using PricelistGenerator.Helpers;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Models.Mappings;
using PricelistGenerator.Models.Menu;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class MenuHandler : IMenuHandler
{
    private readonly IPricelistHandler _pricelistHandler;
    private readonly IPreviewHandler _previewHandler;
    private readonly IPricelistAnalysisHandler _pricelistAnalysisHandler;
    private readonly INDISSupportCatalogueHandler _ndisSupportCatalogueHandler;

    public MenuHandler(IPricelistHandler pricelistHandler, IPreviewHandler previewHandler,
        IPricelistAnalysisHandler pricelistAnalysisHandler,
        INDISSupportCatalogueHandler ndisSupportCatalogueHandler)
    {
        _pricelistHandler = pricelistHandler;
        _previewHandler = previewHandler;
        _pricelistAnalysisHandler = pricelistAnalysisHandler;
        _ndisSupportCatalogueHandler = ndisSupportCatalogueHandler;
    }

    private string GetEnumDescription<TEnum>(TEnum value) where TEnum : Enum
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var descriptionAttribute =
            (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
        return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
    }

    private Dictionary<int, string> GetMenuChoices<TEnum>() where TEnum : Enum
    {
        var values = Enum.GetValues(typeof(TEnum));
        var menuChoices = new Dictionary<int, string>();

        foreach (var value in values)
        {
            if (value is TEnum enumValue)
            {
                var description = GetEnumDescription(enumValue);
                menuChoices.Add((int)Convert.ChangeType(enumValue, typeof(int)), description);
            }
        }

        return menuChoices;
    }


    public void DisplayMainMenu(NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile catalogFile)
    {
        AnsiConsole.WriteLine("\nProgram Options:");

        var menuChoices = GetMenuChoices<MainMenuOptions>();

        foreach (var menuChoice in menuChoices)
        {
            AnsiConsole.WriteLine($"[{menuChoice.Key}] {menuChoice.Value}");
        }

        var selectedIndex = AnsiConsole.Prompt(new SelectionPrompt<int>()
            .Title("\nWhat would you like to do?")
            .PageSize(menuChoices.Count)
            .AddChoices(menuChoices.Keys));

        var selectedEnum = (MainMenuOptions)selectedIndex;

        switch (selectedEnum)
        {
            case MainMenuOptions.GenerateProdaPricelist:
                DisplayExportRegionMenu(selectedEnum, ndisSupportCatalogue, catalogFile);
                break;
            case MainMenuOptions.GeneratePacePricelist:
                DisplayExportRegionMenu(selectedEnum, ndisSupportCatalogue, catalogFile);
                break;
            case MainMenuOptions.PreviewPricelist:
                DisplayRegionPreviewMenu(ndisSupportCatalogue, catalogFile);
                break;
            case MainMenuOptions.PricelistAnalysis:
                DisplayPricelistAnalysisMenu(ndisSupportCatalogue, catalogFile);
                break;
            case MainMenuOptions.Exit:
                break;
            default:
                DisplayMainMenu(ndisSupportCatalogue, catalogFile);
                break;
        }
    }

    private void DisplayExportRegionMenu(MainMenuOptions choosenOption, NdisSupportCatalogue ndisSupportCatalogue,
        SpreadsheetFile spreadsheetFile)
    {
        AnsiConsole.WriteLine("\nPlease select the regions you wish to generate a pricelist for:");

        var menuChoices = GetMenuChoices<RegionMenuOptions>();
        foreach (var menuChoice in menuChoices)
        {
            AnsiConsole.WriteLine($"[{menuChoice.Key}] {menuChoice.Value}");
        }

        var selectedIndexes = AnsiConsole.Prompt(
            new MultiSelectionPrompt<int>()
                .Title("")
                .Required()
                .PageSize(menuChoices.Count)
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to select a region, " +
                    "[green]<enter>[/] to accept)[/]" +
                    "\n        [italic]Toggle 5 only to go back to main menu[/]")
                .AddChoices(menuChoices.Keys));

        var selectedRegionsList = selectedIndexes.Select(index => menuChoices[index]).ToList();
        var returnToMenu = false;
        var selectedRegionsText = "";
        foreach (var region in selectedRegionsList)
        {
            selectedRegionsText = selectedRegionsText + "," + "[" + region + "]";
            if (region == RegionMenuOptions.Exit.ToString())
            {
                returnToMenu = true;
            }
        }

        if (returnToMenu == true)
        {
            DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
        }
        else
        {
            AnsiConsole.WriteLine($"This will generate a pricelist in the directory: {spreadsheetFile.Path}" +
                                  $"\nFor the following regions {selectedRegionsText}");
            if (AnsiConsole.Confirm("Do you want to proceed?"))
            {

                switch (choosenOption)
                {
                    case MainMenuOptions.GenerateProdaPricelist:
                        _pricelistHandler.ExportProdaPricelist(selectedIndexes, ndisSupportCatalogue, spreadsheetFile);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    case MainMenuOptions.GeneratePacePricelist:
                        _pricelistHandler.ExportPacePricelist(selectedIndexes, ndisSupportCatalogue, spreadsheetFile);
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                    default:
                        DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                        break;
                }

            }
            else
            {
                DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
            }
        }
    }

    private void DisplayRegionPreviewMenu(NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile spreadsheetFile)
    {

        var menuChoices = GetMenuChoices<RegionMenuOptions>();
        AnsiConsole.WriteLine("\nRegion Options:");
        foreach (var option in menuChoices)
        {
            AnsiConsole.WriteLine($"{option.Key} [{option.Value}]");
        }

        var selectedIndex = AnsiConsole.Prompt(new SelectionPrompt<int>().Title
            ("\nPlease select the region you wish " +
             "to preview the pricelist for?")
            .PageSize(menuChoices.Count)
            .AddChoices(menuChoices.Keys));

        var selectedEnum = (RegionMenuOptions)selectedIndex;

        if (selectedEnum != (RegionMenuOptions.Exit))
        {
            var priceListTypes = GetMenuChoices<PricelistTypeMenuOptions>();
            var pricelistChoice = AnsiConsole.Prompt(new SelectionPrompt<int>().Title(
                    "\nPlease select the type of pricelist" +
                    " you wish to preview" + "\n Proda(1) Pace(2) Exit(4)")
                .PageSize(priceListTypes.Count)
                .AddChoices(priceListTypes.Keys));

            var selectedPricelistEnum = (PricelistTypeMenuOptions)pricelistChoice;
            if (selectedPricelistEnum != PricelistTypeMenuOptions.Exit)
            {
                var selectedRegionDescription = GetEnumDescription(selectedEnum);
                
                
                var selectedPricelistTypeDescription = GetEnumDescription(selectedPricelistEnum);
                
                AnsiConsole.WriteLine(
                    $"This will display a preview of the pricelist type [underline]{selectedPricelistTypeDescription}[/] for" +
                    $"\nregion [underline]{selectedRegionDescription}[/]");
                if (AnsiConsole.Confirm("Do you want to proceed?"))
                {
                    switch (selectedPricelistEnum)
                    {
                        case PricelistTypeMenuOptions.Proda:
                            _previewHandler.RenderProdaPricelist(selectedIndex, ndisSupportCatalogue);
                            DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                            break;
                        case PricelistTypeMenuOptions.Pace:
                            _previewHandler.RenderPacePricelist(selectedIndex, ndisSupportCatalogue);
                            DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
                            break;
                        default:
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
        else
        {
            DisplayMainMenu(ndisSupportCatalogue, spreadsheetFile);
        }
    }

    private void DisplayPricelistAnalysisMenu(NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile catalogFile)
    {
        NdisSupportCatalogue
            oldNdisSupportCatalogue = new NdisSupportCatalogue();
        SpreadsheetFile oldcatalogFile = new SpreadsheetFile();

        string providedFilePath = "";

        // Need to refactor this 
        while (true)
        {
            providedFilePath =
                AnsiConsole.Ask<string>(
                    "[bold white]Enter Full File Path of the Old NDIS Support Catalogue[/]\n" +
                    "[underline olive]Starting with C:\\ and ending with filename.xlsx :[/]");
            try
            {
                ExcelHelper excelHelper = new ExcelHelper();
                if (excelHelper.ValidateProvidedFile(providedFilePath))
                {

                    oldcatalogFile = excelHelper.CreateFileFromProvidedFilePath(providedFilePath);
                    AnsiConsole.MarkupLine("File existence validation [bold green1]PASSED[/]");
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("File existence validation [bold red]FAILED[/] \n" +
                                           "- Ensure that the file exists in the path and ends with .xlsx ");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Unhandled Exception");
            }

        }

        AnsiConsole.MarkupLine(
            "[dim slowblink]Now importing the Old NDIS Support Catalogue...[/]");


        oldNdisSupportCatalogue = _ndisSupportCatalogueHandler.ImportNdiSupportCatalogue(oldcatalogFile);
        AnsiConsole.MarkupLine(
            "Old NDIS Support Catalogue Successfully Imported [bold green1] SUCCESS[/]");

        AnsiConsole.WriteLine("\nAnalysing Pricelist......");

        // Process to for pricelist
        _pricelistAnalysisHandler.CompletePricelistAnalysis(oldNdisSupportCatalogue,
            ndisSupportCatalogue, catalogFile, oldcatalogFile);
        
        
        AnsiConsole.WriteLine("\n Do you wish to preview or export these reports to an excel file?");
        var menuChoices = GetMenuChoices<PricelistAnalysisMenuOptions>();
        foreach (var menuChoice in menuChoices)
        {
            AnsiConsole.WriteLine($"[{menuChoice.Key}] {menuChoice.Value}");
        }
        
        var selectedIndex = AnsiConsole.Prompt(new SelectionPrompt<int>()
            .Title("\nWhat would you like to do?")
            .PageSize(menuChoices.Count)
            .AddChoices(menuChoices.Keys));
        
        var selectedEnum = (PricelistAnalysisMenuOptions)selectedIndex;
        switch (selectedEnum)
        {
            case PricelistAnalysisMenuOptions.PreviewChanges:
                _pricelistAnalysisHandler.RenderDetailedPricelistAnalysis(oldNdisSupportCatalogue,
                    ndisSupportCatalogue, catalogFile, oldcatalogFile);
                DisplayMainMenu(ndisSupportCatalogue, catalogFile);
                break;
            case PricelistAnalysisMenuOptions.ExportToCSV:
                AnsiConsole.WriteLine("Exporting Analysis to Excel....");
                _pricelistAnalysisHandler.ExportChangesToCSV(oldNdisSupportCatalogue,
                    ndisSupportCatalogue, catalogFile, oldcatalogFile);
                DisplayMainMenu(ndisSupportCatalogue, catalogFile);
                break;
            case PricelistAnalysisMenuOptions.ReturnMainMenu:
                DisplayMainMenu(ndisSupportCatalogue, catalogFile);
                break;
            default:
                DisplayMainMenu(ndisSupportCatalogue, catalogFile);
                break;
        }

        
    }
}
