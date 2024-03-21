using PricelistGenerator.Helpers;
using PricelistGenerator.Models;
using PricelistGenerator.Service;
using Spectre.Console;

namespace PricelistGenerator.Handlers;

public class PreviewHandler
{
    public void RenderProdaPricelist(int menuChoice, NdisSupportCatalogue ndisSupportCatalogue)
    {
        PricelistHandler pricelistHandler = new PricelistHandler();
        Pricelist pricelist = new Pricelist();
        PricelistHelper pricelistHelper = new PricelistHelper();
        PreviewService previewService = new PreviewService();
        var selectedRegion = pricelistHelper.GetMenuChoiceRegionSelection(menuChoice);
        pricelist = pricelistHandler.CreateProdaPricelist(ndisSupportCatalogue, pricelist, selectedRegion);
        previewService.ScaffoldPreviewTable(pricelist);
    }
    
    public void RenderPacePricelist(int menuChoice, NdisSupportCatalogue ndisSupportCatalogue)
    {
        PricelistHandler pricelistHandler = new PricelistHandler();
        Pricelist pricelist = new Pricelist();
        PricelistHelper pricelistHelper = new PricelistHelper();
        PreviewService previewService = new PreviewService();
        var selectedRegion = pricelistHelper.GetMenuChoiceRegionSelection(menuChoice);
        pricelist = pricelistHandler.CreatePacePricelist(ndisSupportCatalogue, pricelist, selectedRegion);
        previewService.ScaffoldPreviewTable(pricelist);
    }
    
}