namespace PricelistGenerator.Models.ExportAnalysisChanges;

public class ExportAnalysisChanges
{
    public List<BaseItem> ItemsAdded { get; set; }
    public List<BaseItem> ItemsRemoved { get; set; }
    public List<BaseItem> DuplicateItems { get; set; }
    public List<PriceChange> PriceIncreases { get; set; }
    public List<PriceChange> PriceDecreases { get; set; }
    public List<ItemNameChange> NameChanges { get; set; }
    public List<PriceLimitChange> PriceLimitChanges { get; set; }
    public List<UnitOfMeasureChange> UnitChanges { get; set; }
    public List<SupportCategoryNumberChange> PaceSupportCategoryNumberChanges { get; set; }
    public List<SupportCategoryNumberChange> ProdaSupportCategoryNumberChanges { get; set; }
    public List<SupportCategoryNameChange> PaceSupportCategoryNameChanges { get; set; }
    public List<SupportCategoryNameChange> ProdaSupportCategoryNameChanges { get; set; }
    public List<DifferentSupportCategoryNumberOrName> DifferentSupportCategoryNumberOrNames { get; set; }
    public List<RegistrationGroupNameChange> RegistrationGroupNameChanges { get; set; }
    public List<RegistrationGroupNumberChange> RegistrationGroupNumberChanges { get; set; }
}