using System.ComponentModel;

namespace PricelistGenerator.Models.Menu;

public enum PricelistTypeMenuOptions
{
    [Description("Proda Pricelist")]
    ProdaPricelist = 1,
    [Description("Pace Pricelist")]
    PacePricelist = 2,
    [Description("Custom Pricelist")]
    CustomPricelist = 3,
    [Description("Exit")]
    Exit = 4
}