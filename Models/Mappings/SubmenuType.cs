using System.ComponentModel;

namespace PricelistGenerator.Models.Mappings;

public enum SubmenuType
{
    [Description("STANDARD PRODA PRICE LIST")]
    Proda = 1,
    [Description("STANDARD PACE PRICE LIST")]
    Pace = 2,
    [Description("PREVIEW PRICE LIST")]
    Preview = 3,
}