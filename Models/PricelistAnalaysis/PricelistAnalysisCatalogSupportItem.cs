namespace PricelistGenerator.Models;

public class PricelistAnalysisCatalogSupportItem
{
    // Used as Reference Point for the Pricelist Analysis
    public string SupportItemNumber { get; set; }
    
    public NdisSupportCatalogueSupportItem oldSupportItem { get; set; }
    public NdisSupportCatalogueSupportItem newSupportItem { get; set; }
    
    public PricelistAnalysisPriceControlChanges PriceControlChanges{ get; set; }
    
    public bool NameChanged { get; set; }

    public bool RegistrationGroupNameChange { get; set; }
    
    public bool RegistrationGroupNumberChange { get; set; }
    
    public bool SupportPurposeChanged { get; set; }
    
    public bool UnitChanged { get; set; }
    
    public bool ProdaSupportCategoryNumberChanged { get; set; }
    public bool PaceSupportCategoryNumberChanged { get; set; }
    public bool ProdaSupportCategoryNameChange { get; set; }
    public bool PaceSupportCategoryNameChange { get; set; }
    
    public PriceChangeStatus ActPriceChange { get; set; }
    public PriceChangeStatus NswPriceChange { get; set; }
    public PriceChangeStatus VicPriceChange { get; set; }
    public PriceChangeStatus QldPriceChange { get; set; }
    public PriceChangeStatus SaPriceChange { get; set; }
    public PriceChangeStatus TasPriceChange { get; set; }
    public PriceChangeStatus WaPriceChange { get; set; }
    public PriceChangeStatus NtPriceChange { get; set; }
    public PriceChangeStatus RemotePriceChange { get; set; } 
    public PriceChangeStatus VeryRemotePriceChange { get; set; }
    
    public enum PriceChangeStatus
    {
        Unchanged,
        Increased,
        Decreased
    }

    public string GetSupportPurpose(string supportItem)
    {
        var identifier = supportItem.Substring(supportItem.Length - 1);
        // Uses the generic identifier to identify the support Purpose if the support item does not end with _T
        if (!supportItem.Contains("T"))
            switch (identifier)
            {
                case "1":
                    return "Core";
                case "2":
                    return "Capital";
                case "3":
                    return "Capacity Building";
                default:
                    return "Unmapped Support Purpose";
            }
        else
        {
            // Mapping for support items that end with _T or _D > Changes to third last character
            identifier = supportItem.Substring(supportItem.Length - 3);
            switch (identifier)
            {
                case "1_T":
                    return "Core";
                case "1_D":
                    return "Core";
                // Represents Identifier 1_T_D
                case "T_D":
                    return "Core";
                    break;               default:
                    return "Unmapped Support Purpose";

            }
        }
    }
}