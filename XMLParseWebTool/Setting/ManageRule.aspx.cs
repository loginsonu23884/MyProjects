using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Xml.Linq;
using System.Reflection;
public partial class Setting_ManageRule : System.Web.UI.Page
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
            BindColor();
            BindDataType();
            BindConditionType();
            if (Request.QueryString["TableName"] != null)
            {
                
                GetRule();
            }
        }
    }

    public void BindColor()
    {

        foreach (PropertyInfo prop in typeof(System.Drawing.Color).GetProperties())
        {
            if (prop.PropertyType.FullName == "System.Drawing.Color")
            {

                ddlForeColor.Items.Add(new ListItem(prop.Name, prop.Name));
                ddlBGColor.Items.Add(new ListItem(prop.Name, prop.Name));
            }
        }
    }
    public void BindConditionType()
    {
        ddlCondition.DataSource = Enum.GetNames(typeof(Config.ConditionType));
        ddlCondition.DataBind();
    }
    public void BindDataType()
    {
        ddlType.DataSource = Enum.GetNames(typeof(Config.RuleDataType));
        ddlType.DataBind();
    }
    /// <summary>
    /// Binding the Conditional List based on column & table
    /// </summary>
    public void GetRule()
    {
        DataSet dsXML = new DataSet();
        dsXML = oXs.LoadXml();
        int RowId = 0;
        if (Request.QueryString["rowId"] != null)
        {
            RowId = Convert.ToInt16(Request.QueryString["rowId"]);
        }
        if (dsXML != null && dsXML.Tables.Count > 0)
        {
            if (dsXML.Tables["conditionalStyle"] != null && dsXML.Tables["conditionalStyle"].Rows.Count > 0)
            {
                DataView dv = new DataView(dsXML.Tables["conditionalStyle"]);
                dv.RowFilter = " TableRef='" + strTableName + "' and ColumnName='" + strColumnName + "'";
                var dtTable = dv.ToTable();
                if (dtTable != null && dtTable.Rows.Count >= RowId)
                {
                    ddlType.SelectedValue = dtTable.Rows[RowId]["Type"].ToString();
                    ddlCondition.SelectedValue = dtTable.Rows[RowId]["Condition"].ToString();
                    txtValue.Text = dtTable.Rows[RowId]["Value"].ToString();
                    ddlBGColor.SelectedValue = dtTable.Rows[RowId]["BGColor"].ToString();
                    ddlForeColor.SelectedValue = dtTable.Rows[RowId]["ForeColor"].ToString();
                }
           }

        }
   }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Update();
    }
    /// <summary>
    /// This is for updating the node element value.
    /// </summary>
    public void Update()
    {
        XDocument xmlDoc = XDocument.Load(oXs.XMLFilePath);
        List<XElement> oList = oXs.GetNodeElementMultipleCondition(xmlDoc, "conditionalStyle", "TableRef", strTableName, "ColumnName", strColumnName);
        int RowId = 0;
        if (Request.QueryString["rowId"] != null)
        {
            RowId = Convert.ToInt16(Request.QueryString["rowId"]);
        }
        oList[RowId].SetElementValue("Type", ddlType.SelectedValue);
        oList[RowId].SetElementValue("Condition", ddlCondition.SelectedValue);
        oList[RowId].SetElementValue("Value", txtValue.Text);
        oList[RowId].SetElementValue("BGColor", ddlBGColor.SelectedValue);
        oList[RowId].SetElementValue("ForeColor", ddlForeColor.SelectedValue);

        xmlDoc.Save(oXs.XMLFilePath);
        lblMessage.Visible = true;
        lblMessage.Text = "Setting is updated successfully.";

    }
}