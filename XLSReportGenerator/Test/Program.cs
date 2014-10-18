using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PreTradeReportGenerator;
using System.Data;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PreTrade oPreTrade = new PreTrade();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\PreTradeXMLDescriptor1.xml";
            byte[] fileContent = oPreTrade.CreateReport(path, GetPOstTradeDataSet());
            System.IO.FileStream oFileStream = default(System.IO.FileStream);
            oFileStream = new System.IO.FileStream("d:\\T\\PreTradeTemplate.xlsx", System.IO.FileMode.Create);
            oFileStream.Write(fileContent, 0, fileContent.Length);
            oFileStream.Close();
            oFileStream.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //PostTrade oPreTrade = new PostTrade();
           //string path = AppDomain.CurrentDomain.BaseDirectory + @"\PostTradeXMLDescriptor.xml";
            //byte[] fileContent = oPreTrade.CreateReport(path, GetPOstTradeDataSet());
            //System.IO.FileStream oFileStream = default(System.IO.FileStream);
            //oFileStream = new System.IO.FileStream("d:\\T\\PostTradeTemplate.xlsx", System.IO.FileMode.Create);
            //oFileStream.Write(fileContent, 0, fileContent.Length);
            //oFileStream.Close();
            //oFileStream.Dispose();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        public static DataSet GetPOstTradeDataSet()
        {

            //string PreTradeXMLDataSet = AppDomain.CurrentDomain.BaseDirectory + @"\PostTradeInput.xml";
            string PreTradeXMLDataSet = AppDomain.CurrentDomain.BaseDirectory + @"\PreTradeInput1.xml";
            DataSet myDataSetForPreTrade = new DataSet();
            myDataSetForPreTrade.ReadXml(PreTradeXMLDataSet);
            return myDataSetForPreTrade;
        }
    }
}
