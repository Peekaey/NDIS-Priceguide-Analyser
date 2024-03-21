using PricelistGenerator.Handlers;
using PricelistGenerator.Helpers;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using Spectre.Console;

namespace PricelistGenerator
{
    internal class Program
    {
        static void Main(string[] args)
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
                    ExcelHelper excelHelper = new ExcelHelper();
                    if (excelHelper.ValidateProvidedFile(providedFilePath))
                    {
                        
                        catalogFile = excelHelper.CreateFileFromProvidedFilePath(providedFilePath);
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

            NdisSupportCatalogueHandler ndisSupportCatalogueHandler = new NdisSupportCatalogueHandler();
            ndisSupportCatalogue = ndisSupportCatalogueHandler.ImportNdiSupportCatalogue(catalogFile);
            AnsiConsole.MarkupLine(
                "NDIS Support Catalogue Successfully Imported [bold green1] SUCCESS[/]"); 
            
            MenuHandler menuHandler = new MenuHandler();
            menuHandler.DisplayMainMenu(ndisSupportCatalogue, catalogFile);
            
        }
    }
}