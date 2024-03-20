using PricelistGenerator.Models;
using PricelistGenerator.Models.File;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace PricelistGenerator.Helpers;

public class CSVHelper
{
    public string ExportPRODAPricelistToCSV(Pricelist pricelist, SpreadsheetFile spreadsheetFile, string selectedRegion)
    {
        try
        {
            
            var fileName = "Standard - " + getFileName(selectedRegion);
            
            // Remove any existing double quotes from the file path
            spreadsheetFile.Path = spreadsheetFile.Path.Trim('"');
            var filePath = Path.Combine(spreadsheetFile.Path, fileName); 

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write initial header row
                csv.WriteField("Registration Group");
                csv.WriteField("Support Purpose");
                csv.WriteField("External ID");
                csv.WriteField("Support Item");
                csv.WriteField("Support Item Description");
                csv.WriteField("Unit of Measure");
                csv.WriteField("Price");
                csv.WriteField("Price Control");
                csv.WriteField("Support Categories");
                csv.WriteField("Support Purpose");
                csv.NextRecord();

                foreach (var supportItem in pricelist.PricelistSupportItems)
                {
                    csv.WriteField(supportItem.RegistrationGroup);
                    csv.WriteField(supportItem.SupportPurpose);
                    csv.WriteField(supportItem.ExternalID);
                    csv.WriteField(supportItem.SupportItem);
                    csv.WriteField(supportItem.SupportItemDescription);
                    csv.WriteField(supportItem.UnitOfMeasure);
                    csv.WriteField(supportItem.Price);
                    csv.WriteField(supportItem.PriceControl);
                    csv.WriteField(supportItem.SupportCategories);
                    csv.WriteField(supportItem.SupportPurpose);
                    csv.NextRecord();
                }
            }

            return filePath;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var error = "error";
            return error;
        }
    }
    
     public string ExportPACEPricelistToCSV(Pricelist pricelist, SpreadsheetFile spreadsheetFile, string selectedRegion)
    {
        try
        {
            var fileName = "PACE - " + getFileName(selectedRegion); 
            
            // Remove any existing double quotes from the file path
            spreadsheetFile.Path = spreadsheetFile.Path.Trim('"');
            var filePath = Path.Combine(spreadsheetFile.Path, fileName); 

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write initial header row
                csv.WriteField("Registration Group");
                csv.WriteField("Support Purpose");
                csv.WriteField("External ID");
                csv.WriteField("Support Item");
                csv.WriteField("Support Item Description");
                csv.WriteField("Unit of Measure");
                csv.WriteField("Price");
                csv.WriteField("Price Control");
                csv.WriteField("Support Categories");
                csv.WriteField("Support Purpose");
                csv.NextRecord();

                foreach (var supportItem in pricelist.PricelistSupportItems)
                {
                    csv.WriteField(supportItem.RegistrationGroup);
                    csv.WriteField(supportItem.SupportPurpose);
                    csv.WriteField(supportItem.ExternalID);
                    csv.WriteField(supportItem.SupportItem);
                    csv.WriteField(supportItem.SupportItemDescription);
                    csv.WriteField(supportItem.UnitOfMeasure);
                    csv.WriteField(supportItem.Price);
                    csv.WriteField(supportItem.PriceControl);
                    csv.WriteField(supportItem.SupportCategories);
                    csv.WriteField(supportItem.SupportPurpose);
                    csv.NextRecord();
                }
            }

            return filePath;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var error = "error";
            return error;
        }
    }

    public String getFileName(string selectedRegion)
    {
        var regionFileName = "";
        switch (selectedRegion)
        {
            case "ACT":
                regionFileName = "ACT.NSW.QLD.VIC";
                break;
            case "NT":
                regionFileName = "NT.SA.TAS.WA";
                break;
            case "Remote":
                regionFileName = "Remote";
                break;
            case "VeryRemote":
                regionFileName = "Very Remote";
                break;
        }
        
        return $"{regionFileName} {DateTime.Now.ToString("dd-MM-yyyy")}.csv";
    }

    
}