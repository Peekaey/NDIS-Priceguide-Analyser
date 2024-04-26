using PricelistGenerator.Models;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Interfaces.Handler;

public interface INDISSupportCatalogueHandler
{
    NdisSupportCatalogue ImportNdiSupportCatalogue(SpreadsheetFile spreadsheetFile);
}