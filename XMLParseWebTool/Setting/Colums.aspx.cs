using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Setting_Colums : System.Web.UI.Page
{
    public DataSet dsXML = new DataSet();
    XMLService oXs = new XMLService();
    public string OAppPath = ConfigurationManager.AppSettings["ProjectName"].Trim();
    public string strTableName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        dsXML = oXs.LoadXml();

        if (Request.QueryString["TableName"] != null)
        {
            strTableName = Request.QueryString["TableName"].ToString();
            GetColumns(Request.QueryString["TableName"].ToString());
        }
    }
    public void GetColumns(string tablName)
    {
        var dt = dsXML.Tables[tablName + "_columns"];
        gvXMLColumns.DataSource = dt;
        gvXMLColumns.DataBind();
    }
}