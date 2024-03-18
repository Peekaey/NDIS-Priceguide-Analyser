using System.ComponentModel;

namespace PricelistGenerator.Models.Mappings;

public enum SupportPurpose
{
    Core,
    Capital,
    [Description("Capacity Building")]
    CapacityBuilding
}