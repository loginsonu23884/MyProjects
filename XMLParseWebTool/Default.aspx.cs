using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    public DataSet dsXML = new DataSet();
    XMLService oXs = new XMLService();
    public string OAppPath = ConfigurationManager.AppSettings["ProjectName"].Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
        dsXML = oXs.LoadXml();
        if (!IsPostBack)
        {
            BindSheets();
            LoadXmlTables();
        }
       
    }
    public void BindSheets()
    {
        if (dsXML != null && dsXML.Tables.Count > 0)
        {
            if (dsXML.Tables["PreTradeSheet"] != null && dsXML.Tables["PreTradeSheet"].Rows.Count > 0)
            {
                ddlSheets.DataSource = dsXML.Tables["PreTradeSheet"];
                ddlSheets.DataTextField = "name";
                ddlSheets.DataValueField = "PreTradeSheet_id";
                ddlSheets.DataBind();
            }
            
        }
    }
   
    public void LoadXmlTables()
    {
        if (ddlSheets.SelectedItem.Text != "")
        {
            var dttables = dsXML.Tables["Tables"];
           
            if (dttables != null && dttables.Rows.Count > 0)
            {
                
                DataView dv = new DataView(dttables);
                dv.RowFilter = "PreTradeSheet_id="+ddlSheets.SelectedValue;
                var dtTable = dv.ToTable();
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    dv = new DataView(dsXML.Tables["Table"]);
                    dv.RowFilter = "Tables_id=" + dtTable.Rows[0]["Tables_id"].ToString();
                 
                    gvXMLTables.DataSource = dv.ToTable();
                    gvXMLTables.DataBind();
                }
               
            }
        }
    }
    protected void ddlSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadXmlTables();
    }
}
