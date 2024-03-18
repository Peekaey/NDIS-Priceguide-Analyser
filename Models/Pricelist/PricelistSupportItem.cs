using System.ComponentModel.DataAnnotations;


public class PricelistSupportItem
{
    [Required]
    public string RegistrationGroup { get; set; }
    [Required]
    public string SupportPurpose { get; set; }
    [Required]
    public string ExternalID { get; set; }
    [Required]
    public string SupportItem { get; set; }
    public string SupportItemDescription { get; set; }
    [Required]
    public string UnitOfMeasure { get; set; }
    [Required]
    public string Price { get; set; }
    [Required]
    public string PriceControl { get; set; }
    [Required]
    public string SupportCategories { get; set; }
    [Required]
    public string OutcomeDomain { get; set; }
}