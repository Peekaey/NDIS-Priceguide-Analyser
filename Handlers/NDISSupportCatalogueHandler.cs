using ClosedXML.Excel;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;

namespace PricelistGenerator.Handlers;

public class NDISSupportCatalogueHandler
{
    public NDISSupportCatalogue importNDISupportCatalogue(SpreadsheetFile spreadsheetFile)
    {
        NDISSupportCatalogue ndisSupportCatalogue = new NDISSupportCatalogue();
        NDISSupportCatalogueService NDISSupportCatalogueService = new NDISSupportCatalogueService();
        
        ndisSupportCatalogue = NDISSupportCatalogueService.importNDISupportCatalogue(spreadsheetFile, ndisSupportCatalogue);

        
        return ndisSupportCatalogue;
    }
    
}