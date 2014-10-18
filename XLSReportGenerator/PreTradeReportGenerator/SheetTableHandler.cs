using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace PreTradeReportGenerator
{
    public class SheetTableHandler
    {

        /// <summary>
        /// Creating  Sheet table Data
        /// </summary>
        /// <param name="wsName">Sheet Name</param>
        /// <param name="TableName">Table name</param>
        /// <param name="previousRowNumber">Last row number </param>
        /// <param name="IsSeqnceTable">do we want the sequenced table?</param>
        /// <param name="XmlDiscriptorDataSet">XML Descriptor Dataset</param>
        /// <param name="wbPart">Worksheet Part where we have to write</param>
        /// <param name="XmlDataSet">Template DataSet</param>
        /// <param name="IsPreTradeData">Table is writing on Pre or Post Trade?</param>
        /// <returns></returns>
        public int TableCreation(string wsName, string TableName, int previousRowNumber, bool IsSeqnceTable, DataSet XmlDiscriptorDataSet,WorkbookPart wbPart,DataSet XmlDataSet,bool IsPreTradeData)
        {
            //Creating object of ExcelConfiguration
            ExcelConfiguration oEc = new ExcelConfiguration();
            int row = 0;
            int CharacterCounter = 0;
            string AlterNativeRowColor = "";
            //is tables in XML Descriptor ?
            if (XmlDiscriptorDataSet.Tables.Count > 0)
            {
                if (XmlDiscriptorDataSet.Tables[TableName] != null && XmlDiscriptorDataSet.Tables[TableName].Rows.Count > 0)
                {
                    var dtTableDetail = XmlDiscriptorDataSet.Tables[TableName];
                    if (dtTableDetail != null && dtTableDetail.Rows.Count > 0)
                    {
                        int IncrementCounter = 0;
                        
                        //Table Starting Column Posting
                        row = Convert.ToInt32(dtTableDetail.Rows[0]["Starting-Position-Of-A-Table-Row"]);
                        //Maintaining the table row sequence
                        if (IsSeqnceTable)
                        {
                           // if(previousRowNumber>=row)
                            row = previousRowNumber + 1;

                        }
                        bool IsLastRowColored = false;
                        //Is Rowstyle 
                        if (dtTableDetail.Rows[0]["IsLastRowColored"].ToString() == "1")
                        {
                            IsLastRowColored = true;
                        }

                        UInt32Value BorderSize = 0;
                        //Table Starting Column Posting
                        int startingNumber = Convert.ToInt32(Convert.ToChar(dtTableDetail.Rows[0]["Starting-Position-Of-A-Table"]));
                        //table alternative color
                        AlterNativeRowColor = dtTableDetail.Rows[0]["AlterNativeRowColor"].ToString();
                        //table border size
                        BorderSize = Convert.ToUInt32(dtTableDetail.Rows[0]["BorderSize"]);
                       
                        //table Rowstyle 
                        bool IsRowStyle = false;
                        if (dtTableDetail.Rows[0]["IsRowStyle"].ToString() == "1")
                        {
                            IsRowStyle = true;
                        }
                        bool IsconditionalStyle = false;
                       
                        //Is Table contain conditional style
                        var dtconditionalStyle = new DataTable();
                        if (dtTableDetail.Rows[0]["conditionalStyle"].ToString() == "1")
                        {
                            IsconditionalStyle = true;
                            if (XmlDiscriptorDataSet.Tables["conditionalStyle"] != null && XmlDiscriptorDataSet.Tables["conditionalStyle"].Rows.Count > 0)
                            {
                                dtconditionalStyle = XmlDiscriptorDataSet.Tables["conditionalStyle"];

                            }
                        }
                        bool IsDoubleColumn = false;
                        string cellAddress = "";
                        char CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                        //Table has double header 
                        if (dtTableDetail.Rows[0]["IsDobleHeader"].ToString() == "1")
                        {
                            if (XmlDiscriptorDataSet.Tables["DoubleHeader"] != null && XmlDiscriptorDataSet.Tables["DoubleHeader"].Rows.Count > 0)
                            {
                                var dv = new DataView(XmlDiscriptorDataSet.Tables["DoubleHeader"]);
                                dv.RowFilter = "TableRef='" + TableName + "'";
                                var dtHeader = dv.ToTable();
                                var Cell = new ExcelCellFormat();

                                if (dtHeader != null && dtHeader.Rows.Count > 0)
                                {
                                   
                                    foreach (DataRow dr in dtHeader.Rows)
                                    {
                                        //Cell formatting
                                        Cell.FontStyle = dr["FontStyle"].ToString();
                                        Cell.ForeColor = dr["ForeColor"].ToString();
                                        Cell.cellNumberFormat = "";
                                        Cell.BGColor = dr["BGColor"].ToString();
                                        Cell.cellFormula = "";
                                        Cell.cellReference = "";
                                        Cell.cellNumberFormat = "";
                                        Cell.IsCellFormat = Convert.ToInt16(dr["IsCellFormat"].ToString());
                                        Cell.IsCellBold = Convert.ToInt16(dr["IsCellBold"].ToString());
                                        Cell.FontSize = Convert.ToInt16(dr["FontSize"].ToString());
                                        Cell.CellWarping = 0;
                                        Cell.CellMerge = Convert.ToInt16(dr["CellMerge"].ToString());
                                        Cell.CellMergeLength = Convert.ToInt16(dr["CellMergeLength"].ToString());
                                        Cell.TextAlignment = dr["TextAlignment"].ToString();
                                        CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                                        if (startingNumber + CharacterCounter > 90)
                                        {
                                            CharacterCounter = 0;
                                            CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                                            IsDoubleColumn = true;
                                        }
                                        cellAddress = "";
                                        if (IsDoubleColumn)
                                        {
                                            cellAddress = "A" + CharCount.ToString() + row.ToString();
                                        }
                                        else
                                        {
                                            cellAddress = CharCount.ToString() + row.ToString();
                                        }
                                        
                                        //Update or Create the Cell value
                                        oEc.UpdateCell(wbPart, wsName, cellAddress, dr["HeaderText"].ToString(), dtHeader.Columns["HeaderText"], Cell, wbPart.WorkbookStylesPart);
                                        //CellMergeLength define the no of column we want to merge
                                        if (Cell.CellMerge > 0 && Cell.CellMergeLength > 0)
                                        {
                                            Cell.CellFirstName = cellAddress;
                                           
                                            CharCount = Convert.ToChar(startingNumber + CharacterCounter + Cell.CellMergeLength);
                                            if (startingNumber + CharacterCounter + Cell.CellMergeLength > 90)
                                            {
                                                CharacterCounter = 0;
                                                CharCount = Convert.ToChar(startingNumber + CharacterCounter + Cell.CellMergeLength);
                                                IsDoubleColumn = true;
                                            }
                                           
                                            if (IsDoubleColumn)
                                            {
                                                Cell.CellSecondName = "A" + CharCount.ToString() + row.ToString();
                                            }
                                            else
                                            {
                                                Cell.CellSecondName = CharCount.ToString() + row.ToString();
                                            }
                                            //Mergeing cell
                                            oEc.MergeTwoCells(oEc.GetWorksheet(wbPart, wsName), Cell.CellFirstName, Cell.CellSecondName);
                                            IncrementCounter = 1 + Cell.CellMergeLength;
                                        }
                                        else
                                        {
                                            IncrementCounter = 1;
                                        }
                                        CharacterCounter = CharacterCounter + IncrementCounter;
                                    }
                                    row = row + 1;
                                }
                            }
                        }
                        CharacterCounter = 0;
                        IncrementCounter = 1;
                        IsDoubleColumn = false;
                        cellAddress = "";
                        CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                        //Getting the Column formatting from XML descriptor 
                        if (XmlDiscriptorDataSet.Tables[TableName + "_columns"] != null && XmlDiscriptorDataSet.Tables[TableName + "_columns"].Rows.Count > 0)
                        {
                            var dtColumn = XmlDiscriptorDataSet.Tables[TableName + "_columns"];
                            if (dtColumn != null && dtColumn.Rows.Count > 0)
                            {
                                int index = 0;
                                //Cell Formatting
                                var Cell = new ExcelCellFormat();
                                Cell.FontStyle = dtTableDetail.Rows[0]["FontStyle"].ToString();
                                Cell.ForeColor = dtTableDetail.Rows[0]["ForeColor"].ToString();
                                Cell.cellNumberFormat = "";
                                Cell.BGColor = dtTableDetail.Rows[0]["BGColor"].ToString();
                                Cell.cellFormula = "";
                                Cell.cellReference = "";
                                Cell.IsCellFormat = Convert.ToInt16(dtTableDetail.Rows[0]["IsCellFormat"]);
                                Cell.IsCellBold = Convert.ToInt16(dtTableDetail.Rows[0]["IsCellBold"]);
                                Cell.FontSize = Convert.ToInt32(dtTableDetail.Rows[0]["FontSize"]);
                                Cell.CellWarping = 0;
                                Cell.CellMerge = 0;
                                Cell.CellMergeLength = 0;
                                Cell.TextAlignment = dtTableDetail.Rows[0]["TextAlignment"].ToString();
                                Cell.BorderSize = BorderSize;
                                //loop of columns based on passed tableanem
                                foreach (DataRow dr in dtColumn.Rows)
                                {
                                    if (Convert.ToInt16(dtColumn.Rows[index]["Visible"]) > 0)
                                    {
                                        Cell.CellWarping = Convert.ToInt16(dr["CellWarping"].ToString());
                                        if (dr["CellMerge"].ToString() != "")
                                        {
                                            Cell.CellMerge = Convert.ToInt16(dr["CellMerge"].ToString());
                                        }
                                        else
                                        {
                                            Cell.CellMerge = 0;
                                        }

                                        if (dr["CellMergeLength"].ToString() != "")
                                        {
                                            Cell.CellMergeLength = Convert.ToInt16(dr["CellMergeLength"].ToString());
                                        }
                                        else
                                        {
                                            Cell.CellMergeLength = 0;
                                        }

                                        IncrementCounter = 1;
                                        CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                                        if (Cell.CellMerge > 0 && Cell.CellMergeLength > 0)
                                        {
                                            Cell.CellFirstName = CharCount.ToString() + row.ToString();
                                            Cell.CellSecondName = Convert.ToChar(startingNumber + CharacterCounter + Cell.CellMergeLength).ToString() + row.ToString(); ;
                                            //Mergeing cell
                                            oEc.MergeTwoCells(oEc.GetWorksheet(wbPart, wsName), Cell.CellFirstName, Cell.CellSecondName);
                                            IncrementCounter = 1 + Cell.CellMergeLength;
                                        }
                                        //Resequencing the columns of excel if we have passed the A..Z ,then coulmn will start with AA..AZ
                                        if (startingNumber + CharacterCounter > 90)
                                        {
                                            IsDoubleColumn = true;
                                            CharacterCounter = 0;
                                            CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                                          
                                        }
                                        if (IsDoubleColumn)
                                        {
                                            cellAddress = "A" + CharCount.ToString() + row.ToString();
                                        }
                                        else
                                        {
                                            cellAddress = CharCount.ToString() + row.ToString();
                                           
                                        }
                                        Cell.IsCellFormat = Convert.ToInt16(dr["IsCellFormat"].ToString());
                                        Cell.CellWarping = Convert.ToInt16(dr["CellWarping"].ToString());
                                        //Update or Create the Cell value
                                        oEc.UpdateCell(wbPart, wsName, cellAddress, dr["text-for-cell-header"].ToString(), dtColumn.Columns["text-for-cell-header"], Cell, wbPart.WorkbookStylesPart);
                                        
                                        CharacterCounter = CharacterCounter + IncrementCounter;
                                    }
                                    index = index + 1;
                                }
                                IsDoubleColumn = false;
                                row = row + 1;
                                CharacterCounter = 0;
                                var dtHoldings =new DataTable();
                                if (IsPreTradeData)
                                {
                                    dtHoldings = getPreTable(TableName, XmlDataSet);
                                }
                                else
                                {
                                    dtHoldings = getPostTable(TableName, XmlDataSet);
                                }
                                string BGColor = "";
                                int Counter = 1;

                                if (dtHoldings != null && dtHoldings.Rows.Count > 0)
                                {
                                    var dtRowStyle = new DataTable();
                                    if (IsRowStyle)
                                    {
                                        if (XmlDiscriptorDataSet.Tables[TableName + "_RowStyle"] != null && XmlDiscriptorDataSet.Tables[TableName + "_RowStyle"].Rows.Count > 0)
                                        {
                                            dtRowStyle = XmlDiscriptorDataSet.Tables[TableName + "_RowStyle"];

                                        }
                                    }
                                    string LastRowColor = "";
                                    if (IsLastRowColored)
                                    {
                                        if (XmlDiscriptorDataSet.Tables["LastRowColored"] != null && XmlDiscriptorDataSet.Tables["LastRowColored"].Rows.Count > 0)
                                        {
                                            var dtLastRowColored = new DataTable();
                                            dtLastRowColored = XmlDiscriptorDataSet.Tables["LastRowColored"];
                                            var Dv = new DataView(dtLastRowColored);
                                            Dv.RowFilter = " TableRef='" + TableName + "' ";
                                            if (Dv.ToTable() != null && Dv.ToTable().Rows.Count > 0)
                                            {
                                                LastRowColor = Dv.ToTable().Rows[0]["BGColor"].ToString();
                                            }
                                        }
                                    }
                                    IsDoubleColumn = false;
                                    string BColor = "";
                                    int CellMergeLength = 0;
                                    foreach (DataRow item in dtHoldings.Rows)
                                    {
                                        CellMergeLength = 0;
                                        BColor = "";
                                        CharacterCounter = 0;
                                        foreach (DataRow dr in dtColumn.Rows)
                                        {
                                            //Cell Formatting
                                            Cell.FontStyle = dr["FontStyle"].ToString();
                                            Cell.ForeColor = dr["ForeColor"].ToString();
                                            Cell.cellNumberFormat = "";
                                            Cell.BGColor = dr["BGColor"].ToString();
                                            Cell.cellFormula = "";
                                            Cell.cellReference = "";
                                            Cell.cellNumberFormat = dr["cellNumberFormat"].ToString();
                                            Cell.IsCellFormat = Convert.ToInt16(dr["IsCellFormat"].ToString());
                                            Cell.IsCellBold = Convert.ToInt16(dr["IsCellBold"].ToString());
                                            Cell.FontSize = Convert.ToInt16(dr["FontSize"].ToString());
                                            Cell.CellWarping = Convert.ToInt16(dr["CellWarping"].ToString());
                                            Cell.CellMerge = Convert.ToInt16(dr["CellMerge"].ToString());
                                            Cell.CellMergeLength = Convert.ToInt16(dr["CellMergeLength"].ToString());
                                            Cell.TextAlignment = dr["TextAlignment"].ToString();
                                            Cell.IsCellItalic = Convert.ToInt16(dr["IsCellItalic"].ToString());
                                            if (Convert.ToInt16(dr["Visible"]) > 0)
                                            {

                                                
                                              
                                               
                                                if (IsRowStyle)
                                                {
                                                    if (dtRowStyle != null && dtRowStyle.Rows.Count > 0)
                                                    {
                                                        var Dv = new DataView(dtRowStyle);
                                                        Dv.RowFilter = " CellId='" + dr["ColumnName"].ToString() + "' and rowId=" + (Counter - 1).ToString();
                                                        if (Dv.ToTable() != null && Dv.ToTable().Rows.Count > 0)
                                                        {
                                                            Cell.FontStyle = Dv.ToTable().Rows[0]["FontStyle"].ToString();
                                                            Cell.ForeColor = Dv.ToTable().Rows[0]["ForeColor"].ToString();
                                                            Cell.cellNumberFormat = Dv.ToTable().Rows[0]["cellNumberFormat"].ToString();
                                                            Cell.BGColor = Dv.ToTable().Rows[0]["BGColor"].ToString();
                                                            Cell.cellFormula = "";
                                                            Cell.cellReference = "";
                                                            Cell.IsCellFormat = Convert.ToInt16(Dv.ToTable().Rows[0]["IsCellFormat"]);
                                                            Cell.IsCellBold = Convert.ToInt16(Dv.ToTable().Rows[0]["IsCellBold"]);
                                                            Cell.FontSize = Convert.ToInt16(Dv.ToTable().Rows[0]["FontSize"]);
                                                            Cell.CellWarping = Convert.ToInt16(Dv.ToTable().Rows[0]["CellWarping"]);
                                                            Cell.CellMerge = Convert.ToInt16(Dv.ToTable().Rows[0]["CellMerge"]);
                                                            Cell.CellMergeLength = 0;
                                                        }

                                                    }
                                                }
                                                if (Counter % 2 != 0 && AlterNativeRowColor != "")
                                                {
                                                    BGColor = AlterNativeRowColor;
                                                    Cell.BGColor = BGColor;
                                                }

                                                IncrementCounter = 1;
                                                CharCount = Convert.ToChar(startingNumber + CharacterCounter);
                                                if (Cell.CellMerge > 0 && Cell.CellMergeLength > 0)
                                                {
                                                  
                                                    Cell.CellFirstName = CharCount.ToString() + row.ToString();
                                                    Cell.CellSecondName = Convert.ToChar(startingNumber + CharacterCounter + Cell.CellMergeLength).ToString() + row.ToString(); ;
                                                    oEc.MergeTwoCells(oEc.GetWorksheet(wbPart, wsName), Cell.CellFirstName, Cell.CellSecondName);
                                                    IncrementCounter = 1 + Cell.CellMergeLength;
                                                }
                                                if (IsconditionalStyle)
                                                {
                                                    if (dtconditionalStyle != null && dtconditionalStyle.Rows.Count > 0)
                                                    {
                                                        BColor = GetConditionalColor(dtconditionalStyle, item[dr["ColumnName"].ToString()].ToString(), dr["ColumnName"].ToString(),TableName);
                                                        if (BColor != "")
                                                        {
                                                            string[] str = BColor.Split(',');

                                                            if (str[0].ToString() != "")
                                                            {
                                                                Cell.BGColor = str[0].ToString();
                                                            }
                                                            if (str[1].ToString() != "")
                                                            {
                                                                Cell.ForeColor = str[1].ToString();
                                                            }
                                                        }
                                                    }
                                                }

                                                if (IsLastRowColored && LastRowColor != "" && Counter == dtHoldings.Rows.Count)
                                                {
                                                    Cell.BGColor = LastRowColor;
                                                }
                                                if (startingNumber + CharacterCounter > 90)
                                                {
                                                    IsDoubleColumn = true;
                                                    CharacterCounter = 0;
                                                    CharCount = Convert.ToChar(startingNumber + CharacterCounter);

                                                }
                                                if (IsDoubleColumn)
                                                {
                                                    cellAddress = "A" + CharCount.ToString() + row.ToString();
                                                }
                                                else
                                                {
                                                    cellAddress = CharCount.ToString() + row.ToString();
                                                  
                                                }
                                                //Update or Create the Cell value
                                                oEc.UpdateCell(wbPart, wsName, cellAddress, item[dr["ColumnName"].ToString()].ToString(), dtHoldings.Columns[dr["ColumnName"].ToString()], Cell, wbPart.WorkbookStylesPart);
                                               

                                             
                                                if (Cell.CellMerge> 0 && Cell.CellMergeLength> 0)
                                                {
                                                   
                                                    while (CellMergeLength != Cell.CellMergeLength)
                                                    {
                                                        Cell.CellSecondName = Convert.ToChar(startingNumber + CharacterCounter + CellMergeLength+1).ToString() + row.ToString(); ;
                                                        //Update or Create the Cell value
                                                        oEc.UpdateCell(wbPart, wsName, Cell.CellSecondName, item[dr["ColumnName"].ToString()].ToString(), dtHoldings.Columns[dr["ColumnName"].ToString()], Cell, wbPart.WorkbookStylesPart);

                                                        CellMergeLength = CellMergeLength + 1;
                                                    }
                                              
                                                }
                                                CharacterCounter = CharacterCounter + IncrementCounter;
                                             }

                                        }
                                        IsDoubleColumn = false;
                                        Counter = Counter + 1;
                                        row++;
                                    }
                                }

                            }
                        }

                    }
                }
            }
            return row;

        }
        /// <summary>
        /// Applying the Coditional Color into Cell
        /// </summary>
        /// <param name="dtConditional"></param>
        /// <param name="ColumnValue"></param>
        /// <param name="ColumnName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public string GetConditionalColor(DataTable dtConditional, string ColumnValue, string ColumnName,string TableName)
        {
            string BGColor = "";
            string ForeColor = "";
            var Dv = new DataView(dtConditional);
            Dv.RowFilter = " TableRef='" + TableName + "' and ColumnName='" + ColumnName + "' ";
            var dtFilterData = Dv.ToTable();
            if (dtFilterData != null && dtFilterData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFilterData.Rows)
                {
                    switch (dr["Type"].ToString().ToLower().Trim())
                    {
                        case "text":
                            switch (dr["Condition"].ToString().ToLower().Trim())
                            {
                                case "equals":
                                    if (ColumnValue == dr["Value"].ToString().Trim())
                                    {
                                        BGColor = dr["BGColor"].ToString();
                                        ForeColor = dr["ForeColor"].ToString();
                                    }
                                    break;
                            }
                        break;
                        case "percent":
                        if (ColumnValue.Trim() == "")
                        {
                            ColumnValue = "0";
                        }
                        switch (dr["Condition"].ToString().ToLower().Trim())
                        {
                            case "equals":
                                if (Convert.ToDouble(ColumnValue) == Convert.ToDouble(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "lowerthan":
                                if (Convert.ToDouble(ColumnValue) < Convert.ToDouble(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "greatherthan":
                                if (Convert.ToDouble(ColumnValue) > Convert.ToDouble(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "lowerorequal":
                                if (Convert.ToDouble(ColumnValue) <= Convert.ToDouble(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "greatherorequal":
                                if (Convert.ToDouble(ColumnValue) >= Convert.ToDouble(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                        }
                        break;
                        case "int":
                        if (ColumnValue.Trim() == "")
                        {
                            ColumnValue = "0";
                        }
                        switch (dr["Condition"].ToString().ToLower().Trim())
                        {
                            case "equals":
                                if (Convert.ToInt32(ColumnValue) == Convert.ToInt32(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "lowerthan":
                                if (Convert.ToInt32(ColumnValue) < Convert.ToInt32(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "greatherthan":
                                if (Convert.ToInt32(ColumnValue) > Convert.ToInt32(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "lowerorequal":
                                if (Convert.ToInt32(ColumnValue) <= Convert.ToInt32(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                            case "greatherorequal":
                                if (Convert.ToInt32(ColumnValue) >= Convert.ToInt32(dr["Value"]))
                                {
                                    BGColor = dr["BGColor"].ToString();
                                    ForeColor = dr["ForeColor"].ToString();
                                }
                                break;
                        }
                        break;
                    }
                }
              
            }
            if (BGColor == "" && ForeColor == "")
            {
                return "";
            }
            else
            return BGColor + "," + ForeColor;
        }
        /// <summary>
        /// Getting the Table name & data from XML DataSet
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataTable getPostTable(string TableName, DataSet ds)
        {
            var dtHoldings = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                switch (TableName)
                {
                    case "Summary":
                        dtHoldings = ds.Tables["Summary"];
                        break;
                    case "Execution_Performance":
                        dtHoldings = ds.Tables["Execution_Performance"];
                        break;
                    case "Country":
                        dtHoldings = ds.Tables["Country"];
                        break;
                    case "Sector":
                        dtHoldings = ds.Tables["Sector"];
                        break;
                    case "Region":
                        dtHoldings = ds.Tables["Region"];
                        break;
                    case "Currency":
                        dtHoldings = ds.Tables["Currency"];
                        break;
                    case "PLR":
                        dtHoldings = ds.Tables["PLR"];
                        break;
                    case "Capitalisation":
                        dtHoldings = ds.Tables["Capitalisation"];
                        break;
                    case "OrderDetails":
                        dtHoldings = ds.Tables["OrderDetails"];
                        break;
                    case "Top_Outliers_LAST":
                        dtHoldings = ds.Tables["Top_Outliers_LAST"];
                        break;
                    case "Bottom_Outliers_LAST":
                        dtHoldings = ds.Tables["Bottom_Outliers_LAST"];
                        break;
                    case "Top_Outliers_HST_CLOSE":
                        dtHoldings = ds.Tables["Top_Outliers_HST_CLOSE"];
                        break;
                    case "Bottom_Outliers_HST_CLOSE":
                        dtHoldings = ds.Tables["Bottom_Outliers_HST_CLOSE"];
                        break;
                    case "Top_Outliers_VWAP":
                        dtHoldings = ds.Tables["Top_Outliers_VWAP"];
                        break;
                    case "Bottom_Outliers_VWAP":
                        dtHoldings = ds.Tables["Bottom_Outliers_VWAP"];
                        break;

                    case "Glossary-Summary":
                        dtHoldings = ds.Tables["Glossary-Summary "];
                        break;
                    case "Glossary-Summary-Breakdowns":
                        dtHoldings = ds.Tables["Glossary-Summary-Breakdowns"];
                        break;
                    case "Glossary-Summary-Execution Performance ":
                        dtHoldings = ds.Tables["Glossary-Summary-Execution Performance "];
                        break;
                    case "Glossary-Summary-Breakdowns ":
                        dtHoldings = ds.Tables["Glossary-Summary-Breakdowns "];
                        break;
                    case "Glossary-Order Details":
                        dtHoldings = ds.Tables["Glossary-Order Details"];
                        break;
                    case "Glossary-Outliers ":
                        dtHoldings = ds.Tables["Glossary-Outliers "];
                        break;
                    case "Glossary-Remarks":
                        dtHoldings = ds.Tables["Glossary-Remarks"];
                        break;
                        
                }
            }
            return dtHoldings;
        }
       /// <summary>
        /// Getting the Table name & data from XML DataSet
       /// </summary>
       /// <param name="TableName"></param>
       /// <param name="ds"></param>
       /// <returns></returns>
        public DataTable getPreTable(string TableName, DataSet ds)
        {
            var dtHoldings = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                switch (TableName)
                {
                    case "Summary_Summary":
                        dtHoldings = ds.Tables["Summary_Summary"];
                        break;
                    case "Summary_Summary_Largest_PLR":
                        dtHoldings = ds.Tables["Summary_Largest_PLR"];
                        break;
                    case "Summary_Largest_Value":
                        dtHoldings = ds.Tables["Summary_Largest_Value"];
                        break;
                    case "Summary_Country":
                        dtHoldings = ds.Tables["Summary_Country"];
                        break;
                    case "Summary_Sector":
                        dtHoldings = ds.Tables["Summary_Sector"];
                        break;
                    case "Summary_Region":
                        dtHoldings = ds.Tables["Summary_Region"];
                        break;
                    case "Summary_Currencies":
                        dtHoldings = ds.Tables["Summary_Currencies"];
                        break;
                    case "Summary_Liquidity_Ratio":
                        dtHoldings = ds.Tables["Summary_Liquidity_Ratio"];
                        break;
                    case "Summary_Capitalisation":
                        dtHoldings = ds.Tables["Summary_Capitalisation"];
                        break;
                    case "Detail":
                        dtHoldings = ds.Tables["Details"];
                        break;
                    case "Glossary_Summary_":
                        dtHoldings = ds.Tables["Glossary_Summary_"];
                        break;
                    case "Glossary_Stock_Details":
                        dtHoldings = ds.Tables["Glossary_Stock_Details"];
                        break;
                    case "Glossary_Graphics":
                        dtHoldings = ds.Tables["Glossary_Graphics_"];
                        break;
                    case "Glossary_Corporate_Actions":
                        dtHoldings = ds.Tables["Glossary_Corporate_Actions"];
                        break;
                }
            }

            return dtHoldings;
        }
    }
}
