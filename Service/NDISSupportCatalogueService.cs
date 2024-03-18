using ClosedXML.Excel;
using PricelistGenerator.Models;
using PricelistGenerator.Models.File;

namespace PricelistGenerator.Service;

public class NDISSupportCatalogueService
{
    public static NDISSupportCatalogue importNDISupportCatalogue(SpreadsheetFile spreadsheetFile,
        NDISSupportCatalogue ndisSupportCatalogue)
    {

        // Importing the Excel File
        var workbook = new XLWorkbook(spreadsheetFile.FullFilePath);

        // Getting the first worksheet of the Excel File
        var worksheet = workbook.Worksheet(1);

        // Skip the first row and start from the second row
        var rows = worksheet.RowsUsed().Skip(1);
        
        foreach (var row in rows)
        {
            NDISSupportCatalogueSupportItem ndisSupportCatalogueSupportItem = new NDISSupportCatalogueSupportItem();

            int columnIndex = 1; // Skip the first column and start from the second column
            
            foreach (var cell in row.CellsUsed())
            {

                switch (columnIndex)
                {
                    case 1:ndisSupportCatalogueSupportItem.SupportItemNumber = cell.Value.ToString();
                        break;
                    case 2:ndisSupportCatalogueSupportItem.SupportItemName = cell.Value.ToString();
                        break;
                    case 3:ndisSupportCatalogueSupportItem.RegistrationGroupNumber = cell.Value.ToString();
                        break;
                    case 4:ndisSupportCatalogueSupportItem.RegistrationGroupName = cell.Value.ToString();
                        break;
                    case 5:ndisSupportCatalogueSupportItem.PRODASupportCategoryNumber = cell.Value.ToString();
                        break;
                    case 6:ndisSupportCatalogueSupportItem.PACESupportCategoryNumber = cell.Value.ToString();
                        break;
                    case 7:ndisSupportCatalogueSupportItem.PRODASupportCategoryName = cell.Value.ToString();
                        break;
                    case 8:ndisSupportCatalogueSupportItem.PACESupportCategoryName = cell.Value.ToString();
                        break;
                    case 9:ndisSupportCatalogueSupportItem.Unit = cell.Value.ToString();
                        break;
                    case 10:ndisSupportCatalogueSupportItem.ACTPrice = cell.Value.ToString();
                        break;
                    case 11:ndisSupportCatalogueSupportItem.NSWPrice = cell.Value.ToString();
                        break;
                    case 12:ndisSupportCatalogueSupportItem.NTPrice = cell.Value.ToString();
                        break;
                    case 13:ndisSupportCatalogueSupportItem.QLDPrice = cell.Value.ToString();
                        break;
                    case 14:ndisSupportCatalogueSupportItem.SAPrice = cell.Value.ToString();
                        break;
                    case 15:ndisSupportCatalogueSupportItem.TASPrice = cell.Value.ToString();
                        break;
                    case 16:ndisSupportCatalogueSupportItem.VICPrice = cell.Value.ToString();
                        break;
                    case 17:ndisSupportCatalogueSupportItem.WAPrice = cell.Value.ToString();
                        break;
                    case 18:ndisSupportCatalogueSupportItem.RemotePrice = cell.Value.ToString();
                        break;
                    case 19:ndisSupportCatalogueSupportItem.VeryRemotePrice = cell.Value.ToString();
                        break;
                }
                columnIndex++; // Move to the next column index
            }

            ndisSupportCatalogue.NDISSupportCatalogueSupportItems.Add(ndisSupportCatalogueSupportItem);
        }
        return ndisSupportCatalogue;
    }
}