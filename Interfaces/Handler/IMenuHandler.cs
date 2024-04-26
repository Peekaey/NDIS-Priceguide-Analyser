using PricelistGenerator.Models;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Interfaces.Handler;

public interface IMenuHandler
{
    public void DisplayMainMenu(NdisSupportCatalogue ndisSupportCatalogue, SpreadsheetFile catalogFile);
}