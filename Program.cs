using PricelistGenerator.Handlers;
using PricelistGenerator.Helpers;
using PricelistGenerator.Interfaces;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Interfaces.Helpers;
using PricelistGenerator.Interfaces.Service;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;
using Spectre.Console;

namespace PricelistGenerator
{
    internal class Program
    {
        private IMenuHandler _menuHandler;
        private INDISSupportCatalogueHandler _ndisSupportCatalogueHandler;
        private IExcelHelper _excelHelper;
        
        public Program(IMenuHandler menuHandler, INDISSupportCatalogueHandler ndisSupportCatalogueHandler, IExcelHelper excelHelper)
        {
            _menuHandler = menuHandler;
            _ndisSupportCatalogueHandler = ndisSupportCatalogueHandler;
            _excelHelper = excelHelper;
        }
        public void Main(string[] args)
        {
            AnsiConsole.Write(
                new FigletText("NDIS Pricelist Analyser")
                    .LeftJustified()
                    .Color(Color.Aquamarine1));
            
            // Declaring Global Variables to be used Program wide
            SpreadsheetFile catalogFile = new SpreadsheetFile();
            NdisSupportCatalogue ndisSupportCatalogue = new NdisSupportCatalogue();
            string providedFilePath = "";
            
            while (true)
            {
                providedFilePath =
                    AnsiConsole.Ask<string>(
                        "[bold white]Enter Full File Path of the Latest NDIS Support Catalogue[/]\n" +
                        "[italic grey]Generated file will be saved in the same directory as the provided file [/]\n" +
                        "[underline olive]Starting with C:\\ and ending with filename.xlsx :[/]");
                try
                {
                    if (_excelHelper.ValidateProvidedFile(providedFilePath))
                    {
                        
                        catalogFile = _excelHelper.CreateFileFromProvidedFilePath(providedFilePath);
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
                "[dim slowblink]Now importing NDIS Support Catalogue...[/]");

            ndisSupportCatalogue = _ndisSupportCatalogueHandler.ImportNdiSupportCatalogue(catalogFile);
            AnsiConsole.MarkupLine(
                "NDIS Support Catalogue Successfully Imported [bold green1] SUCCESS[/]"); 
            
            _menuHandler.DisplayMainMenu(ndisSupportCatalogue, catalogFile);
        }
    }

    abstract class MainProgram
    {
        static void Main(string[] args)
        {
            // Initialising a whole host of dependencies to call a non-static method from a static method
            // Possibly move this to Ninject
            IPricelistService pricelistService = new PricelistService();
            ICsvHelper csvHelper = new Helpers.CsvHelper();
            IPricelistHelper pricelistHelper = new PricelistHelper();
            IPricelistAnalysisService pricelistAnalysisService = new PricelistAnalysisService();
            IPreviewService previewService = new PreviewService();
            INDISSupportCatalogueService ndisSupportCatalogueService = new NdisSupportCatalogueService();

            PricelistHandler pricelistHandler = new PricelistHandler(pricelistService, csvHelper, pricelistHelper);
            PreviewHandler previewHandler = new PreviewHandler(previewService, pricelistHandler, pricelistHelper);
            ExcelHelper excelHelper = new ExcelHelper();
            PricelistAnalysisHandler pricelistAnalysisHandler = new PricelistAnalysisHandler(previewHandler, pricelistAnalysisService, excelHelper );
            NdisSupportCatalogueHandler ndisSupportCatalogueHandler = new NdisSupportCatalogueHandler(ndisSupportCatalogueService);
            MenuHandler menuHandler = new MenuHandler(pricelistHandler, previewHandler, pricelistAnalysisHandler, ndisSupportCatalogueHandler);
            IMenuHandler _menuHandler = new MenuHandler(pricelistHandler, previewHandler, pricelistAnalysisHandler, ndisSupportCatalogueHandler);
            INDISSupportCatalogueHandler _ndisSupportCatalogueHandler = new NdisSupportCatalogueHandler(ndisSupportCatalogueService);
            IExcelHelper _excelHelper = new ExcelHelper();
            
            var program = new Program(_menuHandler, _ndisSupportCatalogueHandler, _excelHelper);
            program.Main(args);
        }
    }
}