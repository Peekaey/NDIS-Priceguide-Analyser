namespace PricelistGenerator.Models.File;

public class SpreadsheetFile
{
    public SpreadsheetFile()
    {
        
    }
    public string Name { get; set; }
    public string Extension { get; set; }
    
    public string FullFilePath { get; set; }

    public string? Path { get; set; }

    
    
}