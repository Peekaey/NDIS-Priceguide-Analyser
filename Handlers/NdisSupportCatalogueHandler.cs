using ClosedXML.Excel;
using PricelistGenerator.Interfaces;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;

namespace PricelistGenerator.Handlers;

public class NdisSupportCatalogueHandler : INDISSupportCatalogueHandler
{
    private INDISSupportCatalogueService _ndisSupportCatalogueService;
    
    public NdisSupportCatalogueHandler(INDISSupportCatalogueService ndisSupportCatalogueService)
    {
        _ndisSupportCatalogueService = ndisSupportCatalogueService;
    }
    public NdisSupportCatalogue ImportNdiSupportCatalogue(SpreadsheetFile spreadsheetFile)
    {
        NdisSupportCatalogue ndisSupportCatalogue = new NdisSupportCatalogue();
        ndisSupportCatalogue = _ndisSupportCatalogueService.ImportNdiSupportCatalogue(spreadsheetFile, ndisSupportCatalogue);
        return ndisSupportCatalogue;
    }
}