using PricelistGenerator.Models;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Interfaces;

public interface INDISSupportCatalogueService
{
    public NdisSupportCatalogue ImportNdiSupportCatalogue(SpreadsheetFile spreadsheetFile,
        NdisSupportCatalogue ndisSupportCatalogue);
}