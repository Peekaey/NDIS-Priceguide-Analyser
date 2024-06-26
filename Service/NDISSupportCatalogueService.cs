using System.Runtime.InteropServices.JavaScript;
using ClosedXML.Excel;
using PricelistGenerator.Interfaces;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using Spectre.Console;

namespace PricelistGenerator.Service;

public class NdisSupportCatalogueService: INDISSupportCatalogueService
{
    public NdisSupportCatalogue ImportNdiSupportCatalogue(SpreadsheetFile spreadsheetFile,
        NdisSupportCatalogue ndisSupportCatalogue)
    {
        var workbook = new XLWorkbook(spreadsheetFile.FullFilePath);
        var worksheet = workbook.Worksheet(1);

        // Skip the first row and start from the second row
        var rows = worksheet.RowsUsed().Skip(1);
        try
        {
            foreach (var row in rows)
            {
                NdisSupportCatalogueSupportItem ndisSupportCatalogueSupportItem = new NdisSupportCatalogueSupportItem();

                int columnIndex = 1;

                foreach (var cell in row.CellsUsed())
                {
                    switch (columnIndex)
                    {
                        case 1:
                            ndisSupportCatalogueSupportItem.SupportItemNumber = cell.Value.ToString();
                            break;
                        case 2:
                            ndisSupportCatalogueSupportItem.SupportItemName = cell.Value.ToString();
                            break;
                        case 3:
                            ndisSupportCatalogueSupportItem.RegistrationGroupNumber = cell.Value.ToString();
                            break;
                        case 4:
                            ndisSupportCatalogueSupportItem.RegistrationGroupName = cell.Value.ToString();
                            break;
                        case 5:
                            ndisSupportCatalogueSupportItem.ProdaSupportCategoryNumber = cell.Value.ToString();
                            break;
                        case 6:
                            ndisSupportCatalogueSupportItem.PaceSupportCategoryNumber = cell.Value.ToString();
                            break;
                        case 7:
                            ndisSupportCatalogueSupportItem.ProdaSupportCategoryName = cell.Value.ToString();
                            break;
                        case 8:
                            ndisSupportCatalogueSupportItem.PaceSupportCategoryName = cell.Value.ToString();
                            break;
                        case 9:
                            ndisSupportCatalogueSupportItem.Unit = cell.Value.ToString();
                            break;
                        case 10:
                            ndisSupportCatalogueSupportItem.ActPrice = cell.Value.ToString();
                            break;
                        case 11:
                            ndisSupportCatalogueSupportItem.NswPrice = cell.Value.ToString();
                            break;
                        case 12:
                            ndisSupportCatalogueSupportItem.NtPrice = cell.Value.ToString();
                            break;
                        case 13:
                            ndisSupportCatalogueSupportItem.QldPrice = cell.Value.ToString();
                            break;
                        case 14:
                            ndisSupportCatalogueSupportItem.SaPrice = cell.Value.ToString();
                            break;
                        case 15:
                            ndisSupportCatalogueSupportItem.TasPrice = cell.Value.ToString();
                            break;
                        case 16:
                            ndisSupportCatalogueSupportItem.VicPrice = cell.Value.ToString();
                            break;
                        case 17:
                            ndisSupportCatalogueSupportItem.WaPrice = cell.Value.ToString();
                            break;
                        case 18:
                            ndisSupportCatalogueSupportItem.RemotePrice = cell.Value.ToString();
                            break;
                        case 19:
                            ndisSupportCatalogueSupportItem.VeryRemotePrice = cell.Value.ToString();
                            break;
                    }

                    columnIndex++;
                }

                ndisSupportCatalogue.NdisSupportCatalogueSupportItems.Add(ndisSupportCatalogueSupportItem);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine("Error - " + e.Message);
        }
        return ndisSupportCatalogue;
    }
}