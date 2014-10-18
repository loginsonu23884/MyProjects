using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;

namespace PreTradeReportGenerator
{
    public class ExcelCellFormat
    {
        public string ForeColor { get; set; }
        public string BGColor { get; set; }
        public string FontStyle { get; set; }
        public string cellNumberFormat { get; set; }
        public string cellFormula { get; set; }
        public string cellReference { get; set; }
        public string CellFirstName { get; set; }
        public string CellSecondName { get; set; }
        public string TextAlignment { get; set; }

        public int IsCellItalic { get; set; }
        public int IsCellFormat { get; set; }
        public int IsCellBold { get; set; }
        public int FontSize { get; set; }
        public int CellWarping { get; set; }
      
        public int CellMerge { get; set; }
        public int CellMergeLength { get; set; }
        public UInt32Value BorderSize { get; set; }
    }
}
