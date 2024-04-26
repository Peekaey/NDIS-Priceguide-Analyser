using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace PricelistGenerator.Models.Menu;

public enum MainMenuOptions
{
    [Description("Generate Standard PRODA Pricelist")]
    GenerateProdaPricelist = 1,
    [Description("Generate Standard PACE Pricelist")]
    GeneratePacePricelist = 2,
    [Description("Generate Custom Pricelist (Not Implemented Yet)")]
    GenerateCustomPricelist = 3,
    [Description("Preview a Regions Pricelist")]
    PreviewPricelist = 4,
    [Description("Pricelist Analysis")]
    PricelistAnalysis = 5,
    [Description("Exit")]
    Exit = 6
}