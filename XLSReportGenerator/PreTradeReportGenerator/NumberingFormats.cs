using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;

namespace PreTradeReportGenerator
{
    public static class ExcelNumberingFormats 
    {

        /// <summary>
        /// Creating type of number formatting for cell
        /// </summary>
        /// <param name="formatCode"></param>
        /// <returns></returns>
        public static UInt32Value NumberFormatId(string formatCode)
        {
            UInt32Value NumberFormatId = 0;

            switch (formatCode.Trim())
            {
                case "General": NumberFormatId = 0;
                    break;
                case "0": NumberFormatId = 1;
                    break;
                case "0.00": NumberFormatId = 2;
                    break;
                case "#,##0": NumberFormatId = 3;
                    break;
                case "#,##0.00": NumberFormatId = 4;
                    break;
                case "0%": NumberFormatId = 9;
                    break;
                case "0.00%": NumberFormatId = 10;
                    break;
                case "0.00E+00": NumberFormatId =11;
                    break;
                case "# ?/?": NumberFormatId = 12;
                    break;
                case "# ??/??": NumberFormatId = 13;
                    break;
                case "d/m/yyyy": NumberFormatId = 14;
                    break;
                case "d-mmm-yy": NumberFormatId = 15;
                    break;
                case "d-mmm": NumberFormatId = 16;
                    break;
                case "mmm-yy": NumberFormatId = 17;
                    break;
                case "h:mm tt": NumberFormatId = 18;
                    break;
                case "h:mm:ss tt": NumberFormatId = 19;
                    break;
                case "H:mm": NumberFormatId = 20;
                    break;
                case "H:mm:ss": NumberFormatId = 21;
                    break;
                case "m/d/yyyy H:mm": NumberFormatId = 22;
                    break;
                case "#,##0 ;(#,##0)": NumberFormatId = 37;
                    break;
                case "#,##0 ;[Red](#,##0)": NumberFormatId = 38;
                    break;
                case "#,##0.00;(#,##0.00)": NumberFormatId = 39;
                    break;
                case "#,##0.00;[Red](#,##0.00)": NumberFormatId = 40;
                    break;
                case "mm:ss": NumberFormatId = 45;
                    break;
                case "[h]:mm:ss": NumberFormatId = 46;
                    break;
                case "mmss.0": NumberFormatId = 47;
                    break;
                case "##0.0E+0": NumberFormatId = 48;
                    break;
                case "@": NumberFormatId = 49;
                    break;
                case "0.0%": NumberFormatId = 168;
                    break;
                case "### ###": NumberFormatId = 169;
                    break;
                case "###": NumberFormatId = 170;
                    break;
                case "0.000": NumberFormatId = 171;
                    break;
                case "0.0000": NumberFormatId = 172;
                    break;
                case "# ### ### ##0": NumberFormatId = 173;
                    break;
                case "#0.0%": NumberFormatId = 174;
                    break;
                case "# ### ### ##0.000": NumberFormatId = 175;
                    break;
                case "# ### ### ##0.0": NumberFormatId = 176;
                    break;
                case "# ### ### ##0.00": NumberFormatId = 177;
                    break;
                case "# ### ##0": NumberFormatId = 178;
                    break;
                case "#0.00": NumberFormatId = 179;
                    break;
                case "# ### ##0.00": NumberFormatId = 180;
                    break;
                case "# ### ### ##0.0%": NumberFormatId = 181;
                    break;
                case "# ### ### ##0.00%": NumberFormatId = 182;
                    break;
                  case @"mm\/dd\/yy": NumberFormatId = 183;
                    break;
                  case "hh:mm:ss": NumberFormatId = 184;
                    break;
              
            }
            return NumberFormatId;

        }
    }
}
