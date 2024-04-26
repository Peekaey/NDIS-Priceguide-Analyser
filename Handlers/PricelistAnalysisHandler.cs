using PricelistGenerator.Interfaces;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using PricelistGenerator.Service;

namespace PricelistGenerator.Handlers;

public class PricelistAnalysisHandler: IPricelistAnalysisHandler
{
    IPreviewHandler _previewHandler;
    private IPricelistAnalysisService _pricelistAnalysisService;
    public PricelistAnalysisHandler(IPreviewHandler previewHandler, IPricelistAnalysisService pricelistAnalysisService)
    {
        _previewHandler = previewHandler;
        _pricelistAnalysisService = pricelistAnalysisService;
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
        
    }
    
    public PricelistAnalysisCatalog MapAnalysisSupportCatalogue(NdisSupportCatalogue oldNdisSupportCatalogue, 
        NdisSupportCatalogue newNdisSupportCatalogue)
    {

        PricelistAnalysisCatalog pricelistAnalysisCatalogue = new PricelistAnalysisCatalog();
        var analysisCatalogue = _pricelistAnalysisService.PopulateNDISSupportCatalogue(oldNdisSupportCatalogue
            , newNdisSupportCatalogue, pricelistAnalysisCatalogue);
        return analysisCatalogue;
    }


    
    
    
}