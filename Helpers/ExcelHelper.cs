using PricelistGenerator.Exceptions;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Helpers;

public class ExcelHelper
{
    
    public bool ValidateProvidedFile(string providedFilePath)
    {
        string trimmedFilePath = RemoveQuotesFromFilePath(providedFilePath);

        if (!CheckFileExists(trimmedFilePath))
        {
            return false;
        }

        return CheckFileExtensionValid(trimmedFilePath);
    }
    
    private bool CheckFileExists(string providedFilePath)
    {
        return File.Exists(providedFilePath);
    }
    
    private bool CheckFileExtensionValid(string providedFilePath)
    {
        string fileExtension = GetFileExtension(providedFilePath);
        
        bool isValidExtension = !string.IsNullOrEmpty(fileExtension) &&
                                string.Equals(fileExtension, ".xlsx", StringComparison.OrdinalIgnoreCase);
        
        return isValidExtension;
    }
    
    private String GetFileExtension(string providedFilePath)
    {
        try
        {
            return Path.GetExtension(providedFilePath);
        }
        catch (Exception e)
        {
            throw new FileValidationException("Error getting file extension", e);
        }
    }
    
    public SpreadsheetFile CreateFileFromProvidedFilePath(string providedFilePath)
    {
        string trimmedFilePath = RemoveQuotesFromFilePath(providedFilePath);
        SpreadsheetFile spreadsheetFile = new SpreadsheetFile();

        try
        {
            spreadsheetFile.Name = Path.GetFileName(providedFilePath);
            spreadsheetFile.Extension = GetFileExtension(providedFilePath);
            spreadsheetFile.FullFilePath = trimmedFilePath;
            spreadsheetFile.Path = Path.GetDirectoryName(providedFilePath);
            
        }
        catch (Exception e)
        {
            throw new FileCreationException("Error creating file from provided file path: " + e.Message);
        }

        return spreadsheetFile;
    }
    
    private String RemoveQuotesFromFilePath(string providedFilePath)
    {
        if (providedFilePath.StartsWith("\"") && providedFilePath.EndsWith("\""))
        {
            providedFilePath = providedFilePath.Trim('"');
        }
        return providedFilePath;
    }
    
    
}