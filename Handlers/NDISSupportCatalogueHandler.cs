using ClosedXML.Excel;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;

namespace PricelistGenerator.Handlers;

public class NdisSupportCatalogueHandler
{
    public NdisSupportCatalogue ImportNdiSupportCatalogue(SpreadsheetFile spreadsheetFile)
    {
        NdisSupportCatalogue ndisSupportCatalogue = new NdisSupportCatalogue();
        NdisSupportCatalogueService ndisSupportCatalogueService = new NdisSupportCatalogueService();
        
        ndisSupportCatalogue = ndisSupportCatalogueService.ImportNdiSupportCatalogue(spreadsheetFile, ndisSupportCatalogue);

        
        return ndisSupportCatalogue;
    }
    
}