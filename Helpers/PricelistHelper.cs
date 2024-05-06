using System.ComponentModel;
using PricelistGenerator.Interfaces.Helpers;
using PricelistGenerator.Models.Mappings;
using PricelistGenerator.Models.Menu;
namespace PricelistGenerator.Helpers;

public class PricelistHelper: IPricelistHelper
{
    public RegionMenuOptions GetSelectedRegion(int region)
    {
        RegionMenuOptions regionDescription = (RegionMenuOptions)region;
        return regionDescription;
    }
    
    public string GetRegionDescription<TEnum>(TEnum value) where TEnum : Enum
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var descriptionAttribute =
            (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
        return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
    }
    
}