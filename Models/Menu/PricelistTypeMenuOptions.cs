namespace PricelistGenerator.Models.Mappings;
using System.ComponentModel;

public enum PricelistTypeMenuOptions
{
    [Description("PRODA")]
    Proda = 1,
    [Description("PACE")]
    Pace = 2,
    [Description("Exit")]
    Exit = 3
}