using PricelistGenerator.Models;

namespace PricelistGenerator.Interfaces.Service;

public interface IPreviewService
{
    public void ScaffoldPreviewTable(Pricelist pricelist);
    public void RenderPricelistAnalysisSummary(PricelistAnalysisCatalog pricelistAnalysisCatalog);
    public void RenderPricelistAnalysisDetailed(PricelistAnalysisCatalog pricelistAnalysisCatalog);
}