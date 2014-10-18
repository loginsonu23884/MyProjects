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
    public class PreTrade
    {
        public MemoryStream SpreadsheetStream { get; set; } // The stream that the spreadsheet gets returned on
        public SpreadsheetDocument ospreadSheet;
        ExcelConfiguration oEc = new ExcelConfiguration();
        WorkbookPart wbPart = null;
        SheetTableHandler osth = new SheetTableHandler();
        public DataTable HeaderRecapHoldings { get; set; }
        public DataSet XmlDiscriptorDataSet { get; set; }
        public DataSet XMLDataSet { get; set; }

        /// <summary>
        /// Creating a Corporate Action Sheet
        /// </summary>
        /// <param name="wsName">Sheet Name</param>
        /// <param name="worksheet">Passing Sheet object for writing data</param>
        public void CorporateActionSheet(string wsName, Worksheet worksheet)
        {
            //Creating sheet logo
            oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);
            //Header Title
            var Cell = new ExcelCellFormat();
            Cell.FontStyle = "Tahoma";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "Gray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 1;
            Cell.FontSize = 14;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.CellFirstName = "A1";
            Cell.CellSecondName = "P1";
            Cell.TextAlignment = "Left";

            oEc.UpdateCell(wbPart, wsName, "A1", "Pre Trade Analysis - Corporate Actions", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //merging cells
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
            }
            //Client Header Recap Item
            ClientHeaderRecap(wsName, worksheet);
          
            // Creating Corporate Action table
           // TableCreation(wsName, "Detail", 0, false);
        }


        /// <summary>
        /// This is for creating the client details in worksheet
        /// </summary>
        /// <param name="wsName">Sheet Name</param>
        /// <param name="worksheet">Worksheet object is passing for creating client details</param>
        public void ClientHeaderRecap(string wsName, Worksheet worksheet)
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
            Cell.IsCellBold = 1;
            Cell.FontSize = 8;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.BorderSize = 0;
            //Client Header Recap Item
            foreach (DataRow item in HeaderRecapHoldings.Rows)
            {
                Cell.TextAlignment = "Left";
                Cell.cellFormula = "=Summary!" + "F" + row.ToString();
                oEc.UpdateCell(wbPart, wsName, "F" + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);

                Cell.CellFirstName = "F" + row.ToString();
                Cell.CellSecondName = "G" + row.ToString();

                if (Cell.CellMerge > 0)
                {
                    //merging cells
                    oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
                }
                Cell.TextAlignment = "right";

                Cell.cellFormula = "=Summary!" + "H" + row.ToString();
                oEc.UpdateCell(wbPart, wsName, "H" + row.ToString(), "", null, Cell, wbPart.WorkbookStylesPart);
                Cell.CellFirstName = "H" + row.ToString();
                Cell.CellSecondName = "I" + row.ToString();

                if (Cell.CellMerge > 0)
                {
                    //merging cells
                    oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
                }
                row++;
            }
        }
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
            Cell.BGColor = "Gray";
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

            oEc.UpdateCell(wbPart, wsName, "A1", "Pre Trade Analysis - Glossaries", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //merging cells
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
            }
            int row = 0;
            // Creating  Summary - Glossary table
            row = osth.TableCreation(wsName, "Glossary_Summary_", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet,true);
            int previousRow = 0;
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Glossary Stock Details table
            row = osth.TableCreation(wsName, "Glossary_Stock_Details", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Glossary Graphics table
            row = osth.TableCreation(wsName, "Glossary_Graphics", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Glossary Corporate Actions table
            osth.TableCreation(wsName, "Glossary_Corporate_Actions", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
        }
        /// <summary>
        /// Creating a Detail sheet 
        /// </summary>
        /// <param name="wsName">Sheet name</param>
        /// <param name="worksheet">Passing worksheet object for writing content in it.</param>
        public void DetailSheet(string wsName, Worksheet worksheet)
        {
            //Creating sheet logo
             oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);
            //Header Title
            var Cell = new ExcelCellFormat();
            Cell.FontStyle = "Tahoma";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "Gray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 1;
            Cell.FontSize = 14;
            Cell.CellWarping = 0;
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.CellFirstName = "A1";
            Cell.CellSecondName = "AU1";
            Cell.TextAlignment = "Left";

            oEc.UpdateCell(wbPart, wsName, "A1", "Pre Trade Analysis - Details", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //merging cells
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
            }
            //Client Header Recap Item
            ClientHeaderRecap(wsName, worksheet);
           // Creating Details table
            osth.TableCreation(wsName, "Detail", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
        }
        /// <summary>
        /// Creating a summary sheet.
        /// </summary>
        /// <param name="wsName">Sheet name</param>
        /// <param name="worksheet">Passing a worksheet for writing content in it.</param>
        public void SummarySheet(string wsName, Worksheet worksheet)
        {
    
            int row = 3;
            //Creating sheet logo
            oEc.CreateLogo(wsName, XmlDiscriptorDataSet, ospreadSheet, wbPart);
            //Cell Formatting
            var Cell = new ExcelCellFormat();
            Cell.FontStyle = "Tahoma";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "Gray";
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
            Cell.CellSecondName = "L1";
            //writing the sheet title in A1-L1 columns
            oEc.UpdateCell(wbPart, wsName, "A1", "Pre Trade Analysis - Summary", null, Cell, wbPart.WorkbookStylesPart);
            if (Cell.CellMerge > 0)
            {
                //merging cells
                oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
            }
            //Cell Formatting
            Cell.FontStyle = "";
            Cell.ForeColor = "Black";
            Cell.cellNumberFormat = "";
            Cell.BGColor = "LightGray";
            Cell.cellFormula = "";
            Cell.cellReference = "";
            Cell.IsCellFormat = 1;
            Cell.IsCellBold = 1;
            Cell.FontSize = 8;
            Cell.CellWarping = 0;
           
            Cell.CellMerge = 1;
            Cell.CellMergeLength = 0;
            Cell.BorderSize = 0;
            //Writing the Client details in F,G,H,I columns
            foreach (DataRow item in HeaderRecapHoldings.Rows)
            {
                Cell.TextAlignment = "Left";
                oEc.UpdateCell(wbPart, wsName, "F" + row.ToString(), item["Key"].ToString(), null, Cell, wbPart.WorkbookStylesPart);
              
                Cell.CellFirstName = "F" + row.ToString();
                Cell.CellSecondName = "G" + row.ToString();
                if (Cell.CellMerge > 0)
                {
                    //merging cells
                    oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
                }
                Cell.TextAlignment = "right";
                oEc.UpdateCell(wbPart, wsName, "H" + row.ToString(), item["Value"].ToString(), null, Cell, wbPart.WorkbookStylesPart);
               
                Cell.CellFirstName = "H" + row.ToString();
                Cell.CellSecondName = "I" + row.ToString();
                if (Cell.CellMerge > 0)
                {
                    //merging cells
                    oEc.MergeTwoCells(worksheet, Cell.CellFirstName, Cell.CellSecondName);
                }
                row++;
            }
           
            //Cresting Summay-Detail Table
            row = osth.TableCreation(wsName, "Summary_Summary", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            int previousRow = 0;
            previousRow = GetLastRowNumber(previousRow, row);
            //Creating Summary-Largest_PLR table
            row = osth.TableCreation(wsName, "Summary_Summary_Largest_PLR", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            //Creating Summary-Largest_Value table
            row = osth.TableCreation(wsName, "Summary_Largest_Value", 0, false, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Country table
            row = osth.TableCreation(wsName, "Summary_Country", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            //// Creating  Summary - Sector table
            row = osth.TableCreation(wsName, "Summary_Sector", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Region table
            row = osth.TableCreation(wsName, "Summary_Region", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Currencies table
            row = osth.TableCreation(wsName, "Summary_Currencies", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Liquidity table
            row = osth.TableCreation(wsName, "Summary_Liquidity_Ratio", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
            previousRow = GetLastRowNumber(previousRow, row);
            // Creating  Summary - Capitalisation table
            row = osth.TableCreation(wsName, "Summary_Capitalisation", previousRow, true, XmlDiscriptorDataSet, wbPart, XMLDataSet, true);
  
        }
        /// <summary>
        /// Getting the Last row of generated table
        /// </summary>
        /// <param name="previousRow">Maintain the previous row number</param>
        /// <param name="CurrentRow">Curent generated table row number</param>
        /// <returns></returns>
        public int GetLastRowNumber(int previousRow,int CurrentRow)
        {
            if (CurrentRow > previousRow)
            {
                previousRow = CurrentRow;
            }
            return previousRow;
        }
             
        /// <summary>
        /// Create a new pretrade template report
        /// </summary>
        /// <param name="XMLDescriptorPath">File path of Pre Trade XML Descriptor</param>
        /// <param name="dsPreTrade">DataSet object of Pre Trade Dataset XML</param>
        /// <returns></returns>
        public byte[] CreateReport(string XMLDescriptorPath, DataSet dsPreTrade)
        {
            //getting the pre trade xml dataset
            if (dsPreTrade != null && dsPreTrade.Tables.Count > 0)
            {
                XMLDataSet = dsPreTrade;
                //getting the client details 
                if (dsPreTrade.Tables["HeaderRecap"] != null && dsPreTrade.Tables["HeaderRecap"].Rows.Count > 0)
                {
                    HeaderRecapHoldings = dsPreTrade.Tables["HeaderRecap"];
                }

            }
            XmlDiscriptorDataSet = oEc.SetupXMLDescriptor(XMLDescriptorPath);
            var dtPreTradeSheet = new DataTable();
            try
            {
                SpreadsheetStream = new MemoryStream();
                // Create the spreadsheet on the MemoryStream
                ospreadSheet = SpreadsheetDocument.Create(SpreadsheetStream, SpreadsheetDocumentType.Workbook);
                WorkbookPart wbp = ospreadSheet.AddWorkbookPart();   // Add workbook part
                Workbook wb = new Workbook(); // Workbook
                FileVersion fv = new FileVersion();
                fv.ApplicationName = "App Name";
                // Add custom stylesheet
                Stylesheet styles = new CreateExcelFile.CustomStylesheet();
                WorkbookStylesPart stylesPart = ospreadSheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = styles;
                stylesPart.Stylesheet.Save();
                wb.Append(fv);
                ospreadSheet.WorkbookPart.Workbook = wb;
                ospreadSheet.WorkbookPart.Workbook.Save();
                if (XmlDiscriptorDataSet.Tables["TradeSheet"] != null && XmlDiscriptorDataSet.Tables["TradeSheet"].Rows.Count > 0)
                {

                    dtPreTradeSheet = XmlDiscriptorDataSet.Tables["TradeSheet"];
                    if (dtPreTradeSheet != null && dtPreTradeSheet.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtPreTradeSheet.Rows)
                        {
                            AddSheet(dr["name"].ToString());
                        }

                    }
                }

                wbPart = ospreadSheet.WorkbookPart;
                //getting Sheet columns
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


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                // All done! Close and save the document.
                ospreadSheet.Close();
            }
            //Returning memory stream byte array
            return SpreadsheetStream.ToArray();
        }
        /// <summary>
        /// This is for adding sheet into workSheetPart
        /// </summary>
        /// <param name="Name">Sheet name</param>
        /// <returns></returns>
        public void AddSheet(string Name)
        {
            WorksheetPart wsp = ospreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
            wsp.Worksheet = new Worksheet();

           
            wsp.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());
            wsp.Worksheet.Save();
    
            UInt32 sheetId;

            // If this is the first sheet, the ID will be 1. If this is not the first sheet, we calculate the ID based on the number of existing
            // sheets + 1.
            if (ospreadSheet.WorkbookPart.Workbook.Sheets == null)
            {
                ospreadSheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                sheetId = 1;
            }
            else
            {
                sheetId = Convert.ToUInt32(ospreadSheet.WorkbookPart.Workbook.Sheets.Count() + 1);
            }

            // Create the new sheet and add it to the workbookpart
            ospreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
            {
                Id = ospreadSheet.WorkbookPart.GetIdOfPart(wsp),
                SheetId = sheetId,
                Name = Name
            }
            );
            // Save our changes
            ospreadSheet.WorkbookPart.Workbook.Save();
           
        }
 
        /// <summary>
        /// perform operation on the sheets
        /// </summary>
        /// <param name="sheetname">Sheet Name</param>
        /// <param name="dtColumn">Sheet Column list for resizing sheet columns</param>
        public void ChooseSheet(string sheetname,DataTable dtColumn)
        {
            var worksheet=oEc.GetWorksheet(wbPart, sheetname);
           //adjusting the column width
            if (dtColumn != null && dtColumn.Rows.Count > 0)
            {
                foreach (DataRow dr in dtColumn.Rows)
                {
                    //Resizing the column width of sheets
                    oEc.CreateColumnWidth(Convert.ToUInt16(dr["Min"]), Convert.ToUInt16(dr["Max"]), Convert.ToDouble(dr["Width"]), worksheet,ospreadSheet);
                }

            }
            switch (sheetname.ToLower())
            {
                case "summary":
                    SummarySheet("Summary", worksheet);
                    break;
                case "details":
                    DetailSheet("Details", worksheet);
                    break;
                case "glossary":
                    GlossarySheet("Glossary", worksheet);
                    break;
            }
        }
      
    }
}
