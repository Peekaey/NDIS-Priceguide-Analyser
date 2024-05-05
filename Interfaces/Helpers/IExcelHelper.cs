using PricelistGenerator.Models.ExportAnalysisChanges;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Interfaces.Helpers;

public interface IExcelHelper
{
    public bool ValidateProvidedFile(string providedFilePath);
    public SpreadsheetFile CreateFileFromProvidedFilePath(string providedFilePath);
    public void ExportAnalysisToExcel(ExportAnalysisChanges exportAnalysisChanges, SpreadsheetFile spreadsheetFile);
}