using PricelistGenerator.Helpers;
using PricelistGenerator.Interfaces;
using PricelistGenerator.Interfaces.Handler;
using PricelistGenerator.Interfaces.Helpers;
using PricelistGenerator.Interfaces.Service;
using PricelistGenerator.Models;
using PricelistGenerator.Service;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class PreviewHandler : IPreviewHandler
{
    private IPreviewService _previewService;
    private IPricelistHandler _pricelistHandler;
    private IPricelistHelper _pricelistHelper;
    
    public PreviewHandler(IPreviewService previewService, IPricelistHandler pricelistHandler, IPricelistHelper pricelistHelper)
    {
        _previewService = previewService;
        _pricelistHandler = pricelistHandler;
        _pricelistHelper = pricelistHelper;
    }

    public void RenderProdaPricelist(int menuChoice, NdisSupportCatalogue ndisSupportCatalogue)
    {
        Pricelist pricelist = new Pricelist();
        var selectedRegion = _pricelistHelper.GetSelectedRegion(menuChoice);
        pricelist = _pricelistHandler.CreateProdaPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
        _previewService.ScaffoldPreviewTable(pricelist);
    }
    
    public void RenderPacePricelist(int menuChoice, NdisSupportCatalogue ndisSupportCatalogue)
    {
        Pricelist pricelist = new Pricelist();
        var selectedRegion = _pricelistHelper.GetSelectedRegion(menuChoice);
        pricelist = _pricelistHandler.CreatePacePricelist(ndisSupportCatalogue, pricelist, selectedRegion);
        _previewService.ScaffoldPreviewTable(pricelist);
    }
    
    public void RenderCompletePricelistAnalysis(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        _previewService.RenderPricelistAnalysisSummary(pricelistAnalysisCatalog);
    }
    
    public void RenderDetailedPricelistAnalysis(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        _previewService.RenderPricelistAnalysisDetailed(pricelistAnalysisCatalog);
    }
    
}