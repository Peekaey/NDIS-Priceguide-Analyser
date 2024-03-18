using System.ComponentModel;

namespace PricelistGenerator.Models.Mappings;

public enum PricelistRegions
{
    [Description("ACT, NSW, QLD, VIC")]
    NSW = 1,
    [Description("NT, SA, TAS, WA")]
    NT = 2,
    [Description("Remote")]
    Remote = 3,
    [Description("Very Remote")]
    VeryRemote = 4
}