using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Data;
using CreateExcelFile;

namespace PreTradeReportGenerator
{
    public class PostTrade
    {
        SheetTableHandler osth = new SheetTableHandler();
         ExcelConfiguration oEc = new ExcelConfiguration();
        WorkbookPart wbPart = null;
        public MemoryStream SpreadsheetStream { get; set; } // The stream that the spreadsheet gets returned on
        public SpreadsheetDocument ospreadSheet;
        public DataTable HeaderRecapHoldings { get; set; }
        public DataSet XmlDiscriptorDataSet { get; set; }
        public DataSet XMLDataSet { get; set; }
        public string SheetTitle = "";


        /// <summary>
        /// Creating a glossary sheet 
        /// </summary>
        /// <param name="wsName">Sheet Name</param>
        /// <param name="worksheet">Passing worksheet object for writing content in it.</param>
        public void GlossarySheet(string wsName, Worksheet worksheet)
        {
            //Creating sheet logo
            oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);
            //Cell Formatting
            var Cell = new ExcelCellFormat();
            Cell.FontStyle = "Tahoma";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "LightGray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 1;
            Cell.FontSize = 14;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.CellFirstName = "A1";
            Cell.CellSecondName = "B1";
            Cell.TextAlignment = "Left";

            oEc.UpdateCell(wbPart, wsName, "A1", "   Post Trade - Glossaries", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //merging cells
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
            }
            int row = 0;
            // Creating  Summary - Glossary table
            row = osth.TableCreation(wsName, "Glossary-Summary", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            int previousRow = 0;
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Glossary-Summary-Execution Performance table
            row = osth.TableCreation(wsName, "Glossary-Summary-Execution Performance ", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Glossary-Summary-Breakdowns table
            row = osth.TableCreation(wsName, "Glossary-Summary-Breakdowns ", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            //// Creating  Summary - Glossary Order Details table
            row = osth.TableCreation(wsName, "Glossary-Order Details", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            //// Creating  Summary - Glossary-Outliers table
            row = osth.TableCreation(wsName, "Glossary-Outliers ", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            //// Creating  Summary - Glossary-Remarks table
            row = osth.TableCreation(wsName, "Glossary-Remarks", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
        }
        /// <summary>
        /// Getting the Last row of generated table
        /// </summary>
        /// <param name="previousRow">Maintain the previous row number</param>
        /// <param name="CurrentRow">Curent generated table row number</param>
        /// <returns></returns>
        public int GetLastRowNumber(int previousRow, int CurrentRow)
        {
            if (CurrentRow > previousRow)
            {
                previousRow = CurrentRow;
            }
            return previousRow;
        }
         /// <summary>
         /// Creating a Outliers sheet
         /// </summary>
         /// <param name="wsName">Sheet Name</param>
         public void OutliersSheet(string wsName, Worksheet worksheet)
         {

             //Creating logo into sheet
             oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);
             //Cell formatting setting
             var Cell = new ExcelCellFormat();
             Cell.FontStyle = "Tahoma";
             Cell.ForeColor = "Black";
             Cell.cellNumberFormat = "";
             Cell.BGColor = "LightGray";
             Cell.cellFormula = "";
             Cell.cellReference = "";
             Cell.IsCellFormat = 1;
             Cell.IsCellBold = 1;
             Cell.FontSize = 14;
             Cell.CellWarping = 0;
             Cell.CellMerge = 1;
             Cell.CellMergeLength = 0;
             Cell.TextAlignment = "Left";
             Cell.CellFirstName = "A1";
             Cell.CellSecondName = "M1";
             //Updating or creating  cell
             oEc.UpdateCell(wbPart, wsName, "A1", "Post Trade - Outliers - Top and Bottom 5 Portfolio Performers", null, Cell, wbPart.WorkbookStylesPart);
             if (Cell.CellMerge > 0)
             {
                 //Merge cell
                 oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
             }
             //Client Header Recap Item
             ClientHeaderRecap(wsName, worksheet,73);
             int row = 0;
             // Creating Details table
            
             row = osth.TableCreation(wsName, "Top_Outliers_LAST", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
             row = osth.TableCreation(wsName, "Bottom_Outliers_LAST", row-1, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
             row = osth.TableCreation(wsName, "Top_Outliers_HST_CLOSE", row, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
             row = osth.TableCreation(wsName, "Bottom_Outliers_HST_CLOSE", row - 1, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
             row = osth.TableCreation(wsName, "Top_Outliers_VWAP", row, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
             row = osth.TableCreation(wsName, "Bottom_Outliers_VWAP", row - 1, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
         }
        /// <summary>
         /// Creating a Order Detail sheet
        /// </summary>
        /// <param name="wsName">Sheet Name</param>
         public void OrderDetailSheet(string wsName, Worksheet worksheet)
        {
            
            //Creating logo into sheet
            oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);
            //Cell formatting setting
            var Cell = new ExcelCellFormat();
            Cell.FontStyle = "Tahoma";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "LightGray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 1;
            Cell.FontSize = 14;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.TextAlignment = "Left";
            Cell.CellFirstName = "A1";
            Cell.CellSecondName = "AM1";
            //Updating or creating  cell
            oEc.UpdateCell(wbPart, wsName, "A1", "Post Trade - Post Trade Analysis", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //Merge cell
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
            }
            //Client Header Recap Item
            ClientHeaderRecap(wsName, worksheet,70);
           // Creating Details table
            osth.TableCreation(wsName, "OrderDetails", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
        }
         /// <summary>
         /// This is for creating the client details in worksheet
         /// </summary>
         /// <param name="wsName">Sheet Name</param>
         /// <param name="worksheet">Worksheet object is passing for creating client details</param>
         public void ClientHeaderRecap(string wsName, Worksheet worksheet, int startingNumber)
         {
             int row = 3;
             var Cell = new ExcelCellFormat();
             //Cell Formatting
             Cell.FontStyle = "";
             Cell.ForeColor = "Black";
             Cell.cellNumberFormat = "";
             Cell.BGColor = "LightGray";
             Cell.cellFormula = "";
             Cell.cellReference = "";
             Cell.IsCellFormat = 1;
            
             Cell.FontSize = 8;
             Cell.CellWarping = 0;
             Cell.CellMerge = 1;
             Cell.CellMergeLength = 0;
             Cell.BorderSize = 0;
             //Client Header Recap Item
             foreach (DataRow item in HeaderRecapHoldings.Rows)
             {
                 Cell.IsCellBold = 0;
                 Cell.TextAlignment = "Left";
                 Cell.cellFormula = "=Summary!" + "I" + row.ToString();
                 oEc.UpdateCell(wbPart, wsName, Convert.ToChar(startingNumber) + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);
                 Cell.cellFormula = "";
                 oEc.UpdateCell(wbPart, wsName, Convert.ToChar(startingNumber+1) + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);

                 Cell.CellFirstName = Convert.ToChar(startingNumber) + row.ToString();
                 Cell.CellSecondName = Convert.ToChar(startingNumber+1) + row.ToString();
                 Cell.CellMerge = 1;
                 if (Cell.CellMerge > 0)
                 {

                     //Merge cell
                     oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
                 }
                 oEc.UpdateCell(wbPart, wsName, Convert.ToChar(startingNumber+2) + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);
                 oEc.UpdateCell(wbPart, wsName, Convert.ToChar(startingNumber+3) + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);
                 Cell.TextAlignment = "right";
                 Cell.IsCellBold = 1;
                 Cell.cellFormula = "=Summary!" + "M" + row.ToString();
                 oEc.UpdateCell(wbPart, wsName, Convert.ToChar(startingNumber+4) + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);
               
                 row++;
             }
         }
        /// <summary>
        /// Creating a summary sheet
        /// </summary>
        /// <param name="wsName">Sheet Name</param>
        /// <param name="worksheet"></param>
        public void SummarySheet(string wsName, Worksheet worksheet)
        {
          
          
            int row = 3;
            //Creating logo into sheet
            oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);

            //Cell formatting setting
            var Cell = new ExcelCellFormat();
            Cell.FontStyle = "Tahoma";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "LightGray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 1;
            Cell.FontSize = 14;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.TextAlignment = "Left";
            Cell.CellFirstName = "A1";
            Cell.CellSecondName = "M1";
            //Updating or creating  cell
            oEc.UpdateCell(wbPart, wsName, "A1", "Post Trade - Summary", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //Merge cell
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
              
                
            }
            Cell.FontStyle = "";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "LightGray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 0;
            Cell.FontSize = 8;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.BorderSize = 0;
            //Client Header Recap Item
            foreach (DataRow item in HeaderRecapHoldings.Rows)
            {
                Cell.IsCellBold = 0;
                Cell.TextAlignment = "Left";
                oEc.UpdateCell(wbPart, wsName, "I" + row.ToString(), item["Label"].ToString(), null, Cell, wbPart.WorkbookStylesPart);

                Cell.CellFirstName = "I" + row.ToString();
                Cell.CellSecondName = "L" + row.ToString();
                if (Cell.CellMerge > 0)
                {
                    
                    //Merge cell
                    oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
                }
                Cell.IsCellBold = 1;
                Cell.TextAlignment = "right";
                oEc.UpdateCell(wbPart, wsName, "M" + row.ToString(), item["Value"].ToString(), null, Cell, wbPart.WorkbookStylesPart);

                
                row++;
            }
            //Cresting Summay-Detail Table
            row = osth.TableCreation(wsName, "Summary", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            int previousRow = 0;
            previousRow = GetLastRowNumber(previousRow, row);
            //Creating Summary_Execution_Performance table
            osth.TableCreation(wsName, "Execution_Performance", previousRow, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            //Creating Summary_Country table

            row = osth.TableCreation(wsName, "Country", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary_Sector table
            row = osth.TableCreation(wsName, "Sector", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating Summary_Region table
            row = osth.TableCreation(wsName, "Region", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary_Currency table
            row = osth.TableCreation(wsName, "Currency", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary_LiquidityRatio table
            row = osth.TableCreation(wsName, "PLR", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary_Capitalisation table
            row = osth.TableCreation(wsName, "Capitalisation", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, false);
          
       }

       /// <summary>
        /// Create a new pretrade template report
       /// </summary>
        /// <param name="XMLDescriptorPath">File path of Pre Trade XML Descriptor</param>
        /// <param name="dsPreTrade">DataSet object of Pre Trade Dataset XML</param>
        /// <returns></returns>
        public byte[] CreateReport(string XMLDescriptorPath, DataSet dsTrade)
        {
            //getting the pre trade xml dataset
            if (dsTrade != null && dsTrade.Tables.Count > 0)
            {
                XMLDataSet = dsTrade;
                //Summary_Currencies_columns
                if (dsTrade.Tables["GlobalInfo"] != null && dsTrade.Tables["GlobalInfo"].Rows.Count > 0)
                {
                    HeaderRecapHoldings = dsTrade.Tables["GlobalInfo"];
                }

            }
            XmlDiscriptorDataSet = oEc.SetupXMLDescriptor(XMLDescriptorPath);
            var dtPreTradeSheet = new DataTable();
            try
            {
                SpreadsheetStream = new MemoryStream();

                // Create the spreadsheet on the MemoryStream
                ospreadSheet =SpreadsheetDocument.Create(SpreadsheetStream, SpreadsheetDocumentType.Workbook);
                WorkbookPart wbp = ospreadSheet.AddWorkbookPart();   // Add workbook part
                Workbook wb = new Workbook(); // Workbook
                FileVersion fv = new FileVersion();
                fv.ApplicationName = "App Name";
               
                // Add stylesheet
                Stylesheet styles = new CreateExcelFile.CustomStylesheet();
                WorkbookStylesPart stylesPart = ospreadSheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = styles;
                stylesPart.Stylesheet.Save();
                //Create Sheets object for adding multiple sheet
                Sheets sheets = new Sheets();
                //Create Sheets of workbook
                if (XmlDiscriptorDataSet.Tables["TradeSheet"] != null && XmlDiscriptorDataSet.Tables["TradeSheet"].Rows.Count > 0)
                {
                    uint sheetId = 1;
                    dtPreTradeSheet = XmlDiscriptorDataSet.Tables["TradeSheet"];

                    if (dtPreTradeSheet != null && dtPreTradeSheet.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtPreTradeSheet.Rows)
                        {
                            //Creating worksheet part object 
                            WorksheetPart wsp = ospreadSheet.WorkbookPart.AddNewPart<WorksheetPart>(); // Add worksheet part
                            wsp.Worksheet = new Worksheet(new SheetData());
                            wsp.Worksheet.Save();
                            //Creating sheet object
                            Sheet sheetSummary = new Sheet();
                            
                            sheetSummary.Name = dr["name"].ToString();
                            sheetSummary.SheetId = sheetId; // Only one sheet per spreadsheet in this class so call it sheet 1
                            sheetSummary.Id = wbp.GetIdOfPart(wsp); // ID of sheet comes from worksheet part
                            sheets.Append(sheetSummary);

                            sheetId = sheetId + 1;
                        }

                    }
                }
                
                wb.Append(fv);
                wb.Append(sheets); // Append sheets to workbook
                ospreadSheet.WorkbookPart.Workbook = wb;
                ospreadSheet.WorkbookPart.Workbook.Save();
                wbPart = ospreadSheet.WorkbookPart;
                //getting Sheet column
                var dtSheetColumns = new DataTable();
                if (XmlDiscriptorDataSet.Tables["Column"] != null && XmlDiscriptorDataSet.Tables["Column"].Rows.Count > 0)
                {
                    dtSheetColumns = XmlDiscriptorDataSet.Tables["Column"];
                }

                //Writing data into sheets
                if (dtPreTradeSheet != null && dtPreTradeSheet.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPreTradeSheet.Rows)
                    {
                        var dv = new DataView(dtSheetColumns);
                        dv.RowFilter = "TradeSheet_id=" + dr["TradeSheet_id"].ToString();
                        ChooseSheet(dr["name"].ToString(), dv.ToTable());

                    }

                }
                // All done! Close and save the document.

            }
            catch
            {

            }
            ospreadSheet.Close();
            return SpreadsheetStream.ToArray();
        }
        //perform operation on the sheets
        public void ChooseSheet(string sheetname, DataTable dtColumn)
        {
            var worksheet = oEc.GetWorksheet(wbPart, sheetname);

            //adjusting the column width
            if (dtColumn != null && dtColumn.Rows.Count > 0)
            {
                foreach (DataRow dr in dtColumn.Rows)
                {
                    oEc.CreateColumnWidth(Convert.ToUInt16(dr["Min"]), Convert.ToUInt16(dr["Max"]), Convert.ToDouble(dr["Width"]), worksheet, ospreadSheet);
                }

            }
            switch (sheetname.ToLower())
            {
                case "summary":
                    SummarySheet("Summary", worksheet);
                    break;
                case "orderdetails":
                    OrderDetailSheet("OrderDetails", worksheet);
                    break;
                case "outliers":
                    OutliersSheet("Outliers", worksheet);
                    break;
                case "glossary":
                    GlossarySheet("Glossary", worksheet);
                    break;
            }
        }
    }
}
