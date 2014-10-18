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

public partial class Setting_DoubleHeaderSetting : System.Web.UI.Page
{
    public string strTableName = "";
    XMLService oXs = new XMLService();
    public string OAppPath = ConfigurationManager.AppSettings["ProjectName"].Trim();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["TableName"] != null)
        {
            strTableName = Request.QueryString["TableName"].ToString();
            
        }
        if (!IsPostBack)
        {
            BindColor();
            BindFontStyleTypes();
            BindTextAlignmentTypes();
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

                ddlForeColor.Items.Add(new ListItem(prop.Name.ToUpper(), prop.Name.ToUpper()));
                ddlBGColor.Items.Add(new ListItem(prop.Name.ToUpper(), prop.Name.ToUpper()));
            }
        }
    }
    public void BindFontStyleTypes()
    {
        System.Drawing.Text.InstalledFontCollection col = new System.Drawing.Text.InstalledFontCollection();
        foreach (System.Drawing.FontFamily family in col.Families)
        {
            ddlFontStyle.Items.Add(new ListItem(family.Name.ToUpper(), family.Name.ToUpper()));
        }

    }

    public void BindTextAlignmentTypes()
    {
        ddlTextAlignment.DataSource = Enum.GetNames(typeof(Config.TextAlignmentType));
        ddlTextAlignment.DataBind();
    }
  
    /// <summary>
    /// Binding the DoubleHeader List based on table
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
            if (dsXML.Tables["DoubleHeader"] != null && dsXML.Tables["DoubleHeader"].Rows.Count > 0)
            {
                DataView dv = new DataView(dsXML.Tables["DoubleHeader"]);
                dv.RowFilter = " TableRef='" + strTableName + "'";
                var dtTable = dv.ToTable();
                if (dtTable != null && dtTable.Rows.Count >= RowId)
                {
                    txtHeaderText.Text = dtTable.Rows[RowId]["HeaderText"].ToString();
                    if (dtTable.Rows[RowId]["CellMerge"].ToString() == "1" || dtTable.Rows[RowId]["CellMerge"].ToString() == "True")
                    {
                        chkCellMerge.Checked = true;
                    }
                   
                    txtCellMergeLength.Text = dtTable.Rows[RowId]["CellMergeLength"].ToString();
                    if (dtTable.Rows[RowId]["IsCellFormat"].ToString() == "1" || dtTable.Rows[RowId]["IsCellFormat"].ToString() == "True")
                    {
                        chkIsCellFormat.Checked = true;
                    }
                   
                    txtAlterNativeRowColor.Text = dtTable.Rows[RowId]["AlterNativeRowColor"].ToString();
                    if (dtTable.Rows[RowId]["IsCellBold"].ToString() == "1" || dtTable.Rows[RowId]["IsCellBold"].ToString() == "True")
                    {
                        chkIsCellBold.Checked = true;
                    }
                  
                    txtFontSize.Text = dtTable.Rows[RowId]["FontSize"].ToString();
                    ddlFontStyle.SelectedValue = dtTable.Rows[RowId]["FontStyle"].ToString().ToUpper();
                    ddlBGColor.SelectedValue = dtTable.Rows[RowId]["BGColor"].ToString().ToUpper();
                    ddlForeColor.SelectedValue = dtTable.Rows[RowId]["ForeColor"].ToString().ToUpper();
                    ddlTextAlignment.SelectedValue = dtTable.Rows[RowId]["TextAlignment"].ToString().Replace("left", "Left").Replace("right", "Right").Replace("center", "Center");;
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
        List<XElement> oList = oXs.GetNodeElement(xmlDoc, "DoubleHeader", "TableRef", strTableName);
        int RowId = 0;
        if (Request.QueryString["rowId"] != null)
        {
            RowId = Convert.ToInt16(Request.QueryString["rowId"]);
        }
        oList[RowId].SetElementValue("HeaderText", txtHeaderText.Text);
        oList[RowId].SetElementValue("CellMerge", Convert.ToInt16(chkCellMerge.Checked));
        oList[RowId].SetElementValue("CellMergeLength", txtCellMergeLength.Text);
        oList[RowId].SetElementValue("IsCellFormat", Convert.ToInt16(chkIsCellFormat.Checked));
        oList[RowId].SetElementValue("AlterNativeRowColor", txtAlterNativeRowColor.Text);
        oList[RowId].SetElementValue("IsCellBold", Convert.ToInt16(chkIsCellBold.Checked));
        oList[RowId].SetElementValue("IsCellFormat", Convert.ToInt16(chkIsCellFormat.Checked));
        oList[RowId].SetElementValue("FontSize", txtFontSize.Text);
        oList[RowId].SetElementValue("FontStyle", ddlFontStyle.SelectedValue);
        oList[RowId].SetElementValue("BGColor", ddlBGColor.SelectedValue);
        oList[RowId].SetElementValue("ForeColor", ddlForeColor.SelectedValue);
        oList[RowId].SetElementValue("TextAlignment", ddlTextAlignment.SelectedValue);


        xmlDoc.Save(oXs.XMLFilePath);
        lblMessage.Visible = true;
        lblMessage.Text = "Setting is updated successfully.";

    }
}