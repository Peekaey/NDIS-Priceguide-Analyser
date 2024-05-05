namespace PricelistGenerator.Models;

public class PricelistAnalysisSupportItemsWithPriceChanges
{
    
    public string SupportItemNumber { get; set; }
    public string OldActPrice { get; set; }
    public string OldNswPrice { get; set; }
    public string OldNtPrice { get; set; }
    public string OldQldPrice { get; set; }
    public string OldSaPrice { get; set; }
    public string OldTasPrice { get; set; }
    public string OldVicPrice { get; set; }
    public string OldWaPrice { get; set; }
    public string OldRemotePrice { get; set; }
    public string OldVeryRemotePrice { get; set; }
    public string NewActPrice { get; set; }
    public string NewNswPrice { get; set; }
    public string NewNtPrice { get; set; }
    public string NewQldPrice { get; set; }
    public string NewSaPrice { get; set; }
    public string NewTasPrice { get; set; }
    public string NewVicPrice { get; set; }
    public string NewWaPrice { get; set; }
    public string NewRemotePrice { get; set; }
    public string NewVeryRemotePrice { get; set; }
    
    public string ActPercentage { get; set; }
    
    public string NtPercentage { get; set; }
    
    public string RemotePercentage { get; set; }
    
    public string VeryRemotePercentage { get; set; }
}