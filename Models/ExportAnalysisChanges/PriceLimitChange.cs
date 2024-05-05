namespace PricelistGenerator.Models.ExportAnalysisChanges;

public class PriceLimitChange
{
    public string SupportItemNumber { get; set; }
    public string NewPriceLimit { get; set; }
    public string OldPriceLimit { get; set; }
}