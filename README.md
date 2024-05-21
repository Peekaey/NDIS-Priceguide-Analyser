# NDIS Priceguide Analysis

A console application to analysis the differences between NDIS priceguides and render them in an easy to read format with the ability to export the analysis or create simplified region specific pricelists from the latest priceguide

See [NDIS Pricing Arrangements](https://www.ndis.gov.au/providers/pricing-arrangements)

The application is designed to read the NDIS XLSX priceguides directly, however with the removal of certain unecessary columns (example demo files have been included in the project).
This is in the column format of   

Support Item Name | Registration Group Number | Registration Group Name | Support Category Number (Proda) | Support Category Number (PACE) | Support Category Name (Proda) | Support Category Name (PACE) | Unit | ACT Price | NSW Price | NT Price | QLD Price | SA Price | TAS Price | VIC Price | WA Price | Remote Price | Very Remote Price

This application will only work with Price guides from Jan 2024 Onwards due to the introduction of PACE with PACE/Proda specific Support Category Name/Numbers

## How to use
1. Get a copy of the latest Price Guide from the NDIS (optional - obtain an older copy of the price guide)
2. Remove unneeded columns and for the excel to in the table format such as in the above
3. Run the application locally (Although it can be easily be built to a .exe file)
4. Follow prompts of the application

This will be updated as needed depending on how NDIS handles their pricing going forward
