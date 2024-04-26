using PricelistGenerator.Models;

namespace PricelistGenerator.Interfaces;

public interface IPricelistAnalysisService
{
    public PricelistAnalysisCatalog PopulateNDISSupportCatalogue(NdisSupportCatalogue oldNdisSupportCatalog,
        NdisSupportCatalogue newNdisSupportCatalogue
        , PricelistAnalysisCatalog pricelistAnalysisCatalog);
}