using PricelistGenerator.Models;

namespace PricelistGenerator.Interfaces.Handler;

public interface IPreviewHandler
{
    public void RenderProdaPricelist(int menuChoice, NdisSupportCatalogue ndisSupportCatalogue);
    public void RenderPacePricelist(int menuChoice, NdisSupportCatalogue ndisSupportCatalogue);
    public void RenderCompletePricelistAnalysis(PricelistAnalysisCatalog pricelistAnalysisCatalog);
}