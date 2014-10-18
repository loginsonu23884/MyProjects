using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Xml;
using System.IO;
public partial class Setting_conditionalStyle : System.Web.UI.Page
{
    public string strTableName = "";
    public string strColumnName = "";
    XMLService oXs = new XMLService();
    public string OAppPath = ConfigurationManager.AppSettings["ProjectName"].Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request.QueryString["TableName"] != null)
        {
            strTableName = Request.QueryString["TableName"].ToString();
            strColumnName = Request.QueryString["ColumnName"].ToString();
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["TableName"] != null)
            {

                GetConditionalSetting();
            }
        }
    }
    /// <summary>
    /// Binding the Conditional List based on column & table
    /// </summary>
    public void GetConditionalSetting()
    {
        DataSet dsXML = new DataSet();
        dsXML = oXs.LoadXml();
        if (dsXML != null && dsXML.Tables.Count > 0)
        {
            if (dsXML.Tables["conditionalStyle"] != null && dsXML.Tables["conditionalStyle"].Rows.Count > 0)
            {
                DataView dv = new DataView(dsXML.Tables["conditionalStyle"]);
                dv.RowFilter = " TableRef='" + strTableName + "' and ColumnName='" + strColumnName + "'";
                var dtTable = dv.ToTable();
                DataColumn column = new DataColumn("Id");
                column.DataType = System.Type.GetType("System.Int32");
                
                dtTable.Columns.Add(column);
                int RowCounter=0;
                foreach (DataRow dr in dtTable.Rows)
                {
                    dr["Id"] = RowCounter;
                    RowCounter = RowCounter + 1;
                }
                dtTable.AcceptChanges();


                gvXMLConditionalStyle.DataSource = dtTable;
                gvXMLConditionalStyle.DataBind();
            }

        }
     
      
    }
   
}
    
