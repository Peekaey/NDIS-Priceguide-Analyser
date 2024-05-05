using PricelistGenerator.Models;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Interfaces.Handler;

public interface IPricelistAnalysisHandler
{
    public void CompletePricelistAnalysis(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue, SpreadsheetFile spreadsheetFile,
        SpreadsheetFile oldSpreadsheetFile);
    
    public PricelistAnalysisCatalog MapAnalysisSupportCatalogue(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue);
    
    public void RenderDetailedPricelistAnalysis(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue, SpreadsheetFile spreadsheetFile, SpreadsheetFile oldSpreadsheetFile);
    
    public void ExportChangesToCSV(NdisSupportCatalogue oldNdisSupportCatalogue,
        NdisSupportCatalogue newNdisSupportCatalogue, SpreadsheetFile spreadsheetFile, SpreadsheetFile oldSpreadsheetFile);
}