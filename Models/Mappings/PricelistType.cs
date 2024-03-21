namespace PricelistGenerator.Models.Mappings;
using System.ComponentModel;

public enum PricelistType
{
    [Description("PRODA")]
    Proda = 1,
    [Description("PACE")]
    Pace = 2
}