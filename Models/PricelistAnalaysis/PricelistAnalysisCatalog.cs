namespace PricelistGenerator.Models;

public class PricelistAnalysisCatalog
{
    public List<PricelistAnalysisCatalogSupportItem> pricelistAnalysisCatalogSupportItems { get; set; }
    public List<NdisSupportCatalogueSupportItem> SupportItemsRemoved { get; set; }
    public List<NdisSupportCatalogueSupportItem> SupportItemsAdded { get; set; }
    public List<NdisSupportCatalogueSupportItem> DuplicateItemsAdded { get; set; }
    
    public List<PricelistAnalysisSupportItemsWithPriceChanges> SupportItemsWithPriceChanges { get; set; }


    public decimal CalculatePriceIncreasePercentage(decimal oldPrice, decimal newPrice)
    {
        return ((newPrice - oldPrice) / oldPrice) * 100m;
    }

    public decimal CalculatePriceDecreasePercentage(decimal oldPrice, decimal newPrice)
    {
        return ((oldPrice - newPrice) / oldPrice) * 100m;
    }
    
    public int GetItemsRenamedCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem => supportItem.NameChanged);
    }
    
    public int GetUnitChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.UnitChanged);
    }
    
    public int GetPaceSupportCategoryNumberChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.PaceSupportCategoryNumberChanged);
    }
    
    public int GetProdaSupportCategoryNumberChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.ProdaSupportCategoryNumberChanged);
    }
    
    public int GetPaceSupportCategoryNameChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.PaceSupportCategoryNameChange);
    }
    
    public int GetProdaSupportCategoryNameChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.ProdaSupportCategoryNameChange);
    }
    
    public int GetDifferentSupportCategoryNumberCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.newSupportItem.PaceSupportCategoryNumber != 
            supportItem.newSupportItem.ProdaSupportCategoryNumber);
    }

    public int GetDifferentSupportCategoryNameCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.newSupportItem.PaceSupportCategoryName !=
            supportItem.newSupportItem.ProdaSupportCategoryName);
    }
    
    public int GetRegistrationGroupNameChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.RegistrationGroupNameChange);
    }
    
    public int GetRegistrationGroupNumberChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.RegistrationGroupNumberChange);
    }

    public int GetPriceIncreaseCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem => 
            supportItem.ActPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.NswPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.NtPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.QldPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.TasPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.VicPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.WaPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.NtPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.RemotePriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased ||
            supportItem.VeryRemotePriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased);
    }
    
    public int GetPriceDecreaseCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem => 
            supportItem.ActPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.NswPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.NtPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.QldPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.TasPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.VicPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.WaPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.NtPriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.RemotePriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased ||
            supportItem.VeryRemotePriceChange == PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased);
    }
    
    
    public int GetPriceControlChangeCount()
    {
        return pricelistAnalysisCatalogSupportItems.Count(supportItem =>
            supportItem.ActPriceControlChanged ||
            supportItem.NswPriceControlChanged ||
            supportItem.VicPriceControlChanged ||
            supportItem.QldPriceControlChanged ||
            supportItem.SaPriceControlChanged ||
            supportItem.TasPriceControlChanged ||
            supportItem.WaPriceControlChanged ||
            supportItem.NtPriceControlChanged ||
            supportItem.RemotePriceControlChanged ||
            supportItem.VeryRemotePriceControlChanged);
    }
    
}