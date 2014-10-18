using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using CreateExcelFile;
using System.IO;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Text.RegularExpressions;
namespace PreTradeReportGenerator
{
    public class ExcelConfiguration
    {
       
       /// <summary>
       /// This is creating or updating the sheet cell value/style/formatting etc.
       /// </summary>
       /// <param name="wbPart"></param>
       /// <param name="sheetName"></param>
       /// <param name="addressName">Cell address</param>
       /// <param name="value">Value which we want to writing</param>
       /// <param name="dc">Cell Datatype</param>
       /// <param name="Ecf">Cell formatting object</param>
       /// <param name="stylesPart"></param>
       /// <returns></returns>
        public bool UpdateCell(WorkbookPart wbPart, string sheetName, string addressName, string value, DataColumn dc,ExcelCellFormat Ecf, WorkbookStylesPart stylesPart)
        {
            // Assume failure.
            bool updated = false;

            Sheet sheet = wbPart.Workbook.Descendants<Sheet>().Where((s) => s.Name == sheetName).FirstOrDefault();
            Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
            if (sheet != null)
            {
              
                Cell cell = InsertCellInWorksheet(ws, addressName);
                if (dc == null)
                {
                    cell.CellValue = new CellValue(value);
                    cell.DataType = new EnumValue<CellValues>(CellValues.String);
                }
                else
                    switch (dc.DataType.ToString().ToLower())
                    {
                        case "system.string":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                            break;
                        case "system.int":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            break;
                        case "system.int32":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            break;
                        case "system.int16":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            break;   
                        case "system.int64":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            break;
                        case "system.decimal":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            break;
                        case "system.boolean":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Boolean);
                            break;
                       
                        case "system.double":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            break;
                        case "system.datetime":
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                         
                            break;
                        default:
                            cell.CellValue = new CellValue(value);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);

                            break;
                    }

             

                if (Ecf.IsCellFormat > 0)
                {
                    Ecf.FontStyle = Ecf.FontStyle == "" ? "TAHOMA" : Ecf.FontStyle;
                    Ecf.FontSize = Ecf.FontSize == 0 ? 9 : Ecf.FontSize;
                    Ecf.BGColor = Ecf.BGColor == "" ? "White" : Ecf.BGColor;
                    Ecf.ForeColor = Ecf.ForeColor == "" ? "Black" : Ecf.ForeColor;
                    bool IsBold = false;
                    if (Ecf.IsCellBold > 0)
                        IsBold = true;


                    bool IsItalic = false;
                    if (Ecf.IsCellItalic > 0)
                        IsItalic = true;
                    

                    UInt32Value fontId = CreateFont(stylesPart.Stylesheet, Ecf.FontStyle, Ecf.FontSize, IsBold,IsItalic, System.Drawing.Color.FromName(Ecf.ForeColor));
                    UInt32Value fillId = CreateFill(stylesPart.Stylesheet, System.Drawing.Color.FromName(Ecf.BGColor));
                    UInt32Value formatId = CreateCellFormat(stylesPart.Stylesheet, fontId, fillId, 0, Ecf);
                    cell.StyleIndex = formatId;
                    
                }
                if (Ecf.cellFormula != "")
                {
                    var CellFormula = new CellFormula { CalculateCell = true, Text = Ecf.cellFormula };
                    cell.CellFormula = CellFormula;
                   
                }
                
               // ws.Save();
                // Save the worksheet.
                updated = true;
            }

            return updated;
        }

        /// <summary>
        /// Cell formatting
        /// </summary>
        /// <param name="styleSheet"></param>
        /// <param name="fontIndex"></param>
        /// <param name="fillIndex"></param>
        /// <param name="numberFormatId"></param>
        /// <param name="Ecf"></param>
        /// <returns></returns>
        private static UInt32Value CreateCellFormat(
          Stylesheet styleSheet,
          UInt32Value fontIndex,
          UInt32Value fillIndex,
          UInt32Value numberFormatId, ExcelCellFormat Ecf)
        {
            CellFormat cellFormat = new CellFormat();

            if (fontIndex != null)
                cellFormat.FontId = fontIndex;

            if (fillIndex != null)
                cellFormat.FillId = fillIndex;

            cellFormat.BorderId = Ecf.BorderSize;
            Alignment alignment1 = new Alignment();
            bool IsSetAlignment = false;
            if (Ecf.CellWarping>0)
            {
                IsSetAlignment = true;
                alignment1.WrapText=true;
               
            }
            if (Ecf.TextAlignment != null)
            {
                IsSetAlignment = true;
                if (Ecf.TextAlignment.ToLower() == "center")
                    alignment1.Horizontal = HorizontalAlignmentValues.Center;
                if (Ecf.TextAlignment.ToLower() == "left")
                    alignment1.Horizontal = HorizontalAlignmentValues.Left;
                if (Ecf.TextAlignment.ToLower() == "right")
                    alignment1.Horizontal = HorizontalAlignmentValues.Right;
            }
          

            if(IsSetAlignment)
            cellFormat.Append(alignment1);
            if (Ecf.cellNumberFormat !="")
            {
                cellFormat.NumberFormatId = ExcelNumberingFormats.NumberFormatId(Ecf.cellNumberFormat);
                cellFormat.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            }
            styleSheet.CellFormats.Append(cellFormat);
            UInt32Value resultCell = styleSheet.CellFormats.Count;
            styleSheet.CellFormats.Count++;
            return resultCell;
        }
        /// <summary>
        /// Cell BG color setting
        /// </summary>
        /// <param name="styleSheet"></param>
        /// <param name="fillColor"></param>
        /// <returns></returns>
        private UInt32Value CreateFill(
            Stylesheet styleSheet,
            System.Drawing.Color fillColor)
        {



            PatternFill patternFill =
                new PatternFill(
                    new ForegroundColor()
                    {
                        Rgb = new HexBinaryValue()
                        {
                            Value =
                            System.Drawing.ColorTranslator.ToHtml(
                                System.Drawing.Color.FromArgb(
                                    fillColor.A,
                                    fillColor.R,
                                    fillColor.G,
                                    fillColor.B)).Replace("#", "")
                        }
                    });

            patternFill.PatternType = PatternValues.Solid;

            patternFill.BackgroundColor = new BackgroundColor();
            patternFill.BackgroundColor.Rgb = patternFill.ForegroundColor.Rgb;

            Fill fill = new Fill(patternFill);

            styleSheet.Fills.Append(fill);

            UInt32Value result = styleSheet.Fills.Count;
            styleSheet.Fills.Count++;
            return result;
        }
        /// <summary>
        /// Cell font formatting
        /// </summary>
        /// <param name="styleSheet"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="isBold"></param>
        /// <param name="IsItalic"></param>
        /// <param name="foreColor"></param>
        /// <returns></returns>
        private UInt32Value CreateFont(
            Stylesheet styleSheet,
            string fontName,
            double? fontSize,
            bool isBold,bool IsItalic,
            System.Drawing.Color foreColor)
        {

            Font font = new Font();

            if (!string.IsNullOrEmpty(fontName))
            {
                FontName name = new FontName()
                {
                    Val = fontName
                };
                font.Append(name);
            }

            if (fontSize.HasValue)
            {
                FontSize size = new FontSize()
                {
                    Val = fontSize.Value
                };
                font.Append(size);
            }

            if (isBold == true)
            {
                Bold bold = new Bold();
                font.Append(bold);
            }
            if (IsItalic == true)
            {
                Italic it = new Italic();
                font.Append(it);
            }
            Color color = new Color()
            {
                Rgb = new HexBinaryValue()
                {
                    Value =
                        System.Drawing.ColorTranslator.ToHtml(
                            System.Drawing.Color.FromArgb(
                                foreColor.A,
                                foreColor.R,
                                foreColor.G,
                                foreColor.B)).Replace("#", "")
                }
            };
            font.Append(color);
           
            styleSheet.Fonts.Append(font);
            UInt32Value result = styleSheet.Fonts.Count;
            styleSheet.Fonts.Count++;
            return result;
        }
        /// <summary>
        /// Getting the Descriptor data set based on xml file path.
        /// </summary>
        /// <param name="XMLDescriptorPath"></param>
        /// <returns></returns>
        public DataSet SetupXMLDescriptor(string XMLDescriptorPath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(XMLDescriptorPath);
            return ds;
        }
             
        // Given a Worksheet and an address (like "AZ254"), either return a cell reference, or 
        // create the cell reference and return it.
        private Cell InsertCellInWorksheet(Worksheet ws, string addressName)
        {
            SheetData sheetData = ws.GetFirstChild<SheetData>();
            Cell cell = null;

            UInt32 rowNumber = GetRowIndex(addressName);
            Row row = GetRow(sheetData, rowNumber);

            // If the cell you need already exists, return it.
            // If there is not a cell with the specified column name, insert one.  
            Cell refCell = row.Elements<Cell>().
                Where(c => c.CellReference.Value == addressName).FirstOrDefault();
            if (refCell != null)
            {
                cell = refCell;
            }
            else
            {
                cell = CreateCell(row, addressName);
            }

            return cell;
        }
        /// <summary>
        /// Creating new cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private Cell CreateCell(Row row, String address)
        {
            Cell cellResult;
            Cell refCell = null;
            cellResult = new Cell();
            cellResult.CellReference = address;
            row.InsertBefore(cellResult, refCell);
            return cellResult;
        }

        /// <summary>
        /// This is for getting row from excel sheet based on rowIndex.
        /// </summary>
        /// <param name="wsData">This is object of  sheetData class.</param>
        /// <param name="rowIndex">This is rowindex of a row.</param>
        /// <returns>it will return object of excel sheet row </returns>
        private Row GetRow(SheetData wsData, UInt32 rowIndex)
        {
           
            var row = wsData.Elements<Row>().
            Where(r => r.RowIndex.Value == rowIndex).FirstOrDefault();
            if (row == null)
            {
                row = new Row();
                row.RowIndex = rowIndex;
                wsData.Append(row);
            }
            return row;
        }
        /// <summary>
        /// Getting the row index
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private UInt32 GetRowIndex(string address)
        {
            string rowPart;
            UInt32 l;
            UInt32 result = 0;

            for (int i = 0; i < address.Length; i++)
            {
                if (UInt32.TryParse(address.Substring(i, 1), out l))
                {
                    rowPart = address.Substring(i, address.Length - i);
                    if (UInt32.TryParse(rowPart, out l))
                    {
                        result = l;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Creating the Sheet Logo
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="XmlDiscriptorDataSet"></param>
        /// <param name="document"></param>
        /// <param name="wbPart"></param>
        public void CreateLogo(string sheetName, DataSet XmlDiscriptorDataSet, SpreadsheetDocument document, WorkbookPart wbPart)
        {
          
            long width = 9525 * 240;
            long height = 9525 * 90;
            int XPosition = 0;
            int YPosition = 0;
            string sImagePath = "";
            if (XmlDiscriptorDataSet != null && XmlDiscriptorDataSet.Tables["Logo-Of-Report"].Rows.Count > 0)
            {
                sImagePath = XmlDiscriptorDataSet.Tables["Logo-Of-Report"].Rows[0]["ImagePath"].ToString();
                XPosition = Convert.ToInt32(XmlDiscriptorDataSet.Tables["Logo-Of-Report"].Rows[0]["XPosition"]);
                YPosition = Convert.ToInt32(XmlDiscriptorDataSet.Tables["Logo-Of-Report"].Rows[0]["YPosition"]);
                width = 9525 * Convert.ToInt32(XmlDiscriptorDataSet.Tables["Logo-Of-Report"].Rows[0]["ImageWidth"]);
                height = 9525 * Convert.ToInt32(XmlDiscriptorDataSet.Tables["Logo-Of-Report"].Rows[0]["ImageHeight"]);
            }

           

            var sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().ToList<DocumentFormat.OpenXml.Spreadsheet.Sheet>().SingleOrDefault(s => s.Name == sheetName);
            var relationshipId = sheets.Id.Value;
            var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
            var ws = worksheetPart.Worksheet;

            try
            {
                var wsp = ws.WorksheetPart;
                var dp = default(DrawingsPart);
                var imgp = default(ImagePart);
                var wsd = default(DocumentFormat.OpenXml.Drawing.Spreadsheet.WorksheetDrawing);

                var ipt = default(ImagePartType);
                switch (sImagePath.Substring(sImagePath.LastIndexOf('.') + 1).ToLower())
                {
                    case "png":
                        ipt = ImagePartType.Png;
                        break; // TODO: might not be correct. Was : Exit Select

                      
                    case "jpg":
                    case "jpeg":
                        ipt = ImagePartType.Jpeg;
                        break; // TODO: might not be correct. Was : Exit Select

                     
                    case "gif":
                        ipt = ImagePartType.Gif;
                        break; // TODO: might not be correct. Was : Exit Select

                       
                    default:
                        return;
                }

                if (wsp.DrawingsPart == null)
                {
                    //----- no drawing part exists, add a new one

                    dp = wsp.AddNewPart<DrawingsPart>();
                    imgp = dp.AddImagePart(ipt, wsp.GetIdOfPart(dp));
                    wsd = new DocumentFormat.OpenXml.Drawing.Spreadsheet.WorksheetDrawing();
                }
                else
                {
                    //----- use existing drawing part

                    dp = wsp.DrawingsPart;
                    imgp = dp.AddImagePart(ipt);
                    dp.CreateRelationshipToPart(imgp);
                    wsd = dp.WorksheetDrawing;
                }

                using (var fs = new FileStream(sImagePath, FileMode.Open))
                {
                    imgp.FeedData(fs);
                }

                var imageNumber = 1;
                if (imageNumber == 1)
                {
                    var drawing = new Drawing();
                    drawing.Id = dp.GetIdOfPart(imgp);
                    ws.Append(drawing);
                }

                var nvdp = new NonVisualDrawingProperties();
                nvdp.Id = new UInt32Value(Convert.ToUInt32(1024 + imageNumber));
                nvdp.Name = "Picture " + imageNumber.ToString();
                nvdp.Description = "Picture";
                var picLocks = new DocumentFormat.OpenXml.Drawing.PictureLocks();
                picLocks.NoChangeAspect = true;
                picLocks.NoChangeArrowheads = true;
                var nvpdp = new NonVisualPictureDrawingProperties();
                nvpdp.PictureLocks = picLocks;
                var nvpp = new NonVisualPictureProperties();
                nvpp.NonVisualDrawingProperties = nvdp;
                nvpp.NonVisualPictureDrawingProperties = nvpdp;

                var stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
                stretch.FillRectangle = new DocumentFormat.OpenXml.Drawing.FillRectangle();

                var blipFill = new BlipFill();
                var blip = new DocumentFormat.OpenXml.Drawing.Blip();
                blip.Embed = dp.GetIdOfPart(imgp);
                blip.CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print;
                blipFill.Blip = blip;
                blipFill.SourceRectangle = new DocumentFormat.OpenXml.Drawing.SourceRectangle();
                blipFill.Append(stretch);

                var t2d = new DocumentFormat.OpenXml.Drawing.Transform2D();
                var offset = new DocumentFormat.OpenXml.Drawing.Offset();
                offset.X = 0;
                offset.Y = 0;
                t2d.Offset = offset;
                var bm = new System.Drawing.Bitmap(sImagePath);

                var extents = new DocumentFormat.OpenXml.Drawing.Extents();

                if (width == null)
                {
                    extents.Cx = Convert.ToInt64(bm.Width) *
                                 Convert.ToInt64(Math.Truncate(Convert.ToSingle(914400) / bm.HorizontalResolution));
                }
                else
                {
                    extents.Cx = width;
                }

                if (height == null)
                {
                    extents.Cy = Convert.ToInt64(bm.Height) *
                                 Convert.ToInt64(Math.Truncate(Convert.ToSingle(914400) / bm.VerticalResolution));
                }
                else
                {
                    extents.Cy = height;
                }

                bm.Dispose();
                t2d.Extents = extents;
                var sp = new ShapeProperties();
                sp.BlackWhiteMode = DocumentFormat.OpenXml.Drawing.BlackWhiteModeValues.Auto;
                sp.Transform2D = t2d;
                var prstGeom = new DocumentFormat.OpenXml.Drawing.PresetGeometry();
                prstGeom.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle;
                prstGeom.AdjustValueList = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
                sp.Append(prstGeom);
                sp.Append(new DocumentFormat.OpenXml.Drawing.NoFill());

                var picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
                picture.NonVisualPictureProperties = nvpp;
                picture.BlipFill = blipFill;
                picture.ShapeProperties = sp;

                var pos = new Position();

                pos.X = XPosition;
                pos.Y = YPosition;
                var ext = new Extent();
                ext.Cx = extents.Cx;
                ext.Cy = extents.Cy;
                var anchor = new AbsoluteAnchor();
                anchor.Position = pos;
                anchor.Extent = ext;
                anchor.Append(picture);
                anchor.Append(new ClientData());
                wsd.Append(anchor);
                wsd.Save(dp);
            }
            catch (Exception ex)
            {
                // or do something more interesting if you want
                throw ex;
            }
        }
        
        /// <summary>
        /// Resizing the sheet columns 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="width"></param>
        /// <param name="currentWorkSheet"></param>
        /// <param name="ospreadSheet"></param>
        public void CreateColumnWidth(uint startIndex, uint endIndex, double width, Worksheet currentWorkSheet, SpreadsheetDocument ospreadSheet)
        {
             Columns colsNew = new Columns();
            // Create the column
            Column column = new Column();
            column.Min = startIndex;
            column.Max = endIndex;
            column.Width = width;
            column.CustomWidth = true;
            colsNew.Append(column); // Add it to the list of columns

             currentWorkSheet.InsertBefore<Columns>(colsNew, currentWorkSheet.Where(x => x.LocalName == "sheetData").First());
            currentWorkSheet.Save();
            ospreadSheet.WorkbookPart.Workbook.Save();
        }
        // Given a document name, a worksheet name, and the names of two adjacent cells, merges the two cells.
        // When two cells are merged, only the content from one cell is preserved:
        // the upper-left cell for left-to-right languages or the upper-right cell for right-to-left languages.
        public void MergeTwoCells(Worksheet worksheet, string cell1Name, string cell2Name)
        {
            MergeCells mergeCells;
            if (worksheet.Elements<MergeCells>().Count() > 0)
            {
                mergeCells = worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();

                // Insert a MergeCells object into the specified position.
                if (worksheet.Elements<CustomSheetView>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                }
                else if (worksheet.Elements<DataConsolidate>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                }
                else if (worksheet.Elements<SortState>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                }
                else if (worksheet.Elements<AutoFilter>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                }
                else if (worksheet.Elements<Scenarios>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                }
                else if (worksheet.Elements<ProtectedRanges>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                }
                else if (worksheet.Elements<SheetProtection>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                }
                else if (worksheet.Elements<SheetCalculationProperties>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                }
                else
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                }
            }

            // Create the merged cell and append it to the MergeCells collection.
            MergeCell mergeCell = new MergeCell() { Reference = new StringValue(cell1Name + ":" + cell2Name) };
           
            mergeCells.Append(mergeCell);

            worksheet.Save();
        }
     
        // Given a SpreadsheetDocument and a worksheet name, get the specified worksheet.
        public Worksheet GetWorksheet(WorkbookPart workPart, string worksheetName)
        {
            IEnumerable<Sheet> sheets = workPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
            WorksheetPart worksheetPart = (WorksheetPart)workPart.GetPartById(sheets.First().Id);
            if (sheets.Count() == 0)
                return null;
            else
                return worksheetPart.Worksheet;
        }
    
    }
}
