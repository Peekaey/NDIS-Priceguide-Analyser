using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace PricelistGenerator.Models.Menu;

public enum MainMenuOptions
{
    [Description("Generate Standard PRODA Pricelist")]
    GenerateProdaPricelist = 1,
    [Description("Generate Standard PACE Pricelist")]
    GeneratePacePricelist = 2,
    [Description("Preview a Regions Pricelist")]
    PreviewPricelist = 3,
    [Description("Pricelist Analysis")]
    PricelistAnalysis = 4,
    [Description("Close Program")]
    Exit = 5,
}