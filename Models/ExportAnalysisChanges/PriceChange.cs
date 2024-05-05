namespace PricelistGenerator.Models.ExportAnalysisChanges;

public class PriceChange
{
    public string SupportItemNumber { get; set; }
    public string ActPriceChangePercentage { get; set; }
    public string NtPriceChangePercentage { get; set; }
    public string RemotePriceChangePercentage { get; set; }
    public string VeryRemotePriceChangePercentage { get; set; }
}