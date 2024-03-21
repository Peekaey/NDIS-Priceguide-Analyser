using PricelistGenerator.Models.Mappings;

namespace PricelistGenerator.Helpers;

public class PricelistHelper
{
    public String GetMenuChoiceRegionSelection(int menuChoice)
    {
        switch ((PricelistRegions)menuChoice)
        {
            case PricelistRegions.Nsw:
                return "ACT";
            case PricelistRegions.Nt:
                return "NT";
            case PricelistRegions.Remote:
                return "Remote";
            case PricelistRegions.VeryRemote:
                return "VeryRemote";
            default:
                return "Invalid Choice";
        }
    }
    
}