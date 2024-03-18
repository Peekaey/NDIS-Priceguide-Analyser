using ClosedXML.Excel;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;

namespace PricelistGenerator.Handlers;

public class NDISSupportCatalogueHandler
{
    public static NDISSupportCatalogue importNDISupportCatalogue(SpreadsheetFile spreadsheetFile)
    {
        NDISSupportCatalogue ndisSupportCatalogue = new NDISSupportCatalogue();
        
        ndisSupportCatalogue = NDISSupportCatalogueService.importNDISupportCatalogue(spreadsheetFile, ndisSupportCatalogue);

        
        return ndisSupportCatalogue;
    }
    
}