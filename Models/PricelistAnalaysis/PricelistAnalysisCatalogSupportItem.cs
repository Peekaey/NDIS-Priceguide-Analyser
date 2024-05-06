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
}