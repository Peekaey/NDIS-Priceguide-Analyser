using System.ComponentModel;

namespace PricelistGenerator.Models.Menu;

public enum RegionMenuOptions
{
    [Description("ACT.NSW.QLD.VIC")]
    Act = 1,
    [Description("NT.SA.TAS.WA")]
    Nt = 2,
    [Description("Remote")]
    Remote = 3,
    [Description("Very Remote")]
    VeryRemote = 4,
    [Description("Exit")]
    Exit = 5
}