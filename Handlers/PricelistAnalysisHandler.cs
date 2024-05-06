using PricelistGenerator.Interfaces;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Interfaces.Helpers;
using PricelistGenerator.Models;
using PricelistGenerator.Models.ExportAnalysisChanges;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;

namespace PricelistGenerator.Handlers;

public class PricelistAnalysisHandler: IPricelistAnalysisHandler
{
    private IPreviewHandler _previewHandler;
    private IPricelistAnalysisService _pricelistAnalysisService;
    private IExcelHelper _excelHelper;
    public PricelistAnalysisHandler(IPreviewHandler previewHandler, IPricelistAnalysisService pricelistAnalysisService, IExcelHelper excelHelper)
    {
        _previewHandler = previewHandler;
        _pricelistAnalysisService = pricelistAnalysisService;
        _excelHelper = excelHelper;
    }
    
    public void CompletePricelistAnalysis(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue, SpreadsheetFile spreadsheetFile, SpreadsheetFile oldSpreadsheetFile)
    {
        var analysisCatalogue = MapAnalysisSupportCatalogue(oldNdisSupportCatalogue, newNdisSupportCatalogue);
        _previewHandler.RenderCompletePricelistAnalysis(analysisCatalogue);
        
    }
    public void RenderDetailedPricelistAnalysis(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue, SpreadsheetFile spreadsheetFile,
        SpreadsheetFile oldSpreadsheetFile)
    {
        var analysisCatalogue = MapAnalysisSupportCatalogue(oldNdisSupportCatalogue, newNdisSupportCatalogue);
        _previewHandler.RenderDetailedPricelistAnalysis(analysisCatalogue);
    }
    
    public PricelistAnalysisCatalog MapAnalysisSupportCatalogue(NdisSupportCatalogue oldNdisSupportCatalogue, 
        NdisSupportCatalogue newNdisSupportCatalogue)
    {
        PricelistAnalysisCatalog pricelistAnalysisCatalogue = new PricelistAnalysisCatalog();
        var analysisCatalogue = _pricelistAnalysisService.PopulateNDISSupportCatalogue(oldNdisSupportCatalogue
            , newNdisSupportCatalogue, pricelistAnalysisCatalogue);
        return analysisCatalogue;
    }

    public void ExportChangesToCSV(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue, SpreadsheetFile spreadsheetFile, SpreadsheetFile oldSpreadsheetFile)
    {
        PricelistAnalysisCatalog pricelistAnalysisCatalogue = new PricelistAnalysisCatalog();
        var analysisCatalogue = _pricelistAnalysisService.PopulateNDISSupportCatalogue(oldNdisSupportCatalogue
            , newNdisSupportCatalogue, pricelistAnalysisCatalogue);
        
        ExportAnalysisChanges exportAnalysisChanges = new ExportAnalysisChanges();
        exportAnalysisChanges =
            _pricelistAnalysisService.MapPricelistAnalysisCatalogToExportAnalysis(analysisCatalogue);

        _excelHelper.ExportAnalysisToExcel(exportAnalysisChanges, spreadsheetFile);
    }

    

    
    
    
}