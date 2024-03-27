namespace PricelistGenerator.Service;
using PricelistGenerator.Helpers;
using PricelistGenerator.Models;
using Spectre.Console;
public class PreviewService

{
    public void ScaffoldPreviewTable(Pricelist pricelist)
    {
        var table = new Table();
        table.AddColumn("Row");
        table.AddColumn("Registration Group");
        table.AddColumn("Support Purpose");
        table.AddColumn("External ID");
        table.AddColumn("Support Item");
        table.AddColumn("Description");
        table.AddColumn("Unit of Measure");
        table.AddColumn("Price");
        table.AddColumn("Price Control");
        table.AddColumn("Support Categories");
        table.AddColumn("Support Purpose");
        
        var index = 0;
        foreach (var supportItem in pricelist.PricelistSupportItems)
        {
            var price = "";
            if (!string.IsNullOrEmpty((supportItem.Price)))
            {
                price = supportItem.Price;
            }
            else
            {
                price = "";
            }
            
            table.AddRow(
                index.ToString(),
                supportItem.RegistrationGroup,
                supportItem.SupportPurpose,
                supportItem.ExternalId,
                supportItem.SupportItem,
                "null",
                supportItem.UnitOfMeasure,
                price,
                supportItem.PriceControl,
                supportItem.SupportCategories,
                supportItem.SupportPurpose
            );
            index++;
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Table finished rendering");
    }
}