using PricelistGenerator.Models.Menu;

namespace PricelistGenerator.Interfaces.Helpers;

public interface IPricelistHelper
{
    public RegionMenuOptions GetSelectedRegion(int region);
    public string GetRegionDescription<TEnum>(TEnum value) where TEnum : Enum;

}