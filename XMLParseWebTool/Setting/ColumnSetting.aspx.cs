using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Reflection;
public partial class Setting_ColumnSetting : System.Web.UI.Page
{
    public string strTableName = "";
    public string strColumnName = "";
    XDocument xmlDoc;
    protected void Page_Load(object sender, EventArgs e)
    {
        xmlDoc = XDocument.Load(oXs.XMLFilePath);
        if (Request.QueryString["TableName"] != null)
        {
            strTableName = Request.QueryString["TableName"].ToString();
            strColumnName = Request.QueryString["ColumnName"].ToString();
        }
        if (!IsPostBack)
        {
            BindColor();
            BindFontStyleTypes();
            BindTextAlignmentTypes();
            if (Request.QueryString["TableName"] != null)
            {
               
                GettableSetting(strTableName, strColumnName);
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
    

    XMLService oXs = new XMLService();
    public void GettableSetting(string tablName, string ColumnName)
    {
        List<XElement> oList = oXs.GetNodeElement(xmlDoc,tablName + "_columns", "ColumnName", ColumnName);
        List<XElement> oList1 = oXs.GetNodeElementMultipleCondition(xmlDoc, "conditionalStyle", "TableRef", tablName, "ColumnName", ColumnName);
        foreach (XElement itemElement in oList)
        {
            txtStextforcellheader.Text = itemElement.Element("text-for-cell-header").Value;


            if (itemElement.Element("IsCellFormat").Value == "1")
            {
                chkIsCellFormat.Checked = true;
            }
          
           
            if (itemElement.Element("IsCellBold").Value == "1")
            {
                chkIsCellBold.Checked = true;
            }


            txtFontSize.Text = itemElement.Element("FontSize").Value;
            ddlFontStyle.SelectedValue = itemElement.Element("FontStyle").Value.ToUpper();
            ddlBGColor.SelectedValue = itemElement.Element("BGColor").Value.ToUpper();
            ddlForeColor.SelectedValue = itemElement.Element("ForeColor").Value.ToUpper();
            txtcellNumberFormat.Text = itemElement.Element("cellNumberFormat").Value;
            txtCellMergeLength.Text = itemElement.Element("CellMergeLength").Value;
            ddlTextAlignment.SelectedValue = itemElement.Element("TextAlignment").Value.Replace("left", "Left").Replace("right", "Right").Replace("center", "Center");
            if (itemElement.Element("Visible").Value == "1")
            {
                chkIsVisible.Checked = true;
            }
            if (itemElement.Element("IsCellItalic").Value == "1")
            {
                chkIsCellItalic.Checked = true;
            }
            if (itemElement.Element("CellWarping").Value == "1")
            {
                chkCellWarping.Checked = true;
            }

            if (itemElement.Element("CellMerge").Value == "1")
            {
                chkCellMerge.Checked = true;
            }

            break;
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
        List<XElement> oList = oXs.GetNodeElement(xmlDoc,  Request.QueryString["TableName"].ToString() + "_columns", "ColumnName", Request.QueryString["ColumnName"].ToString());
        foreach (XElement itemElement in oList)
        {

            itemElement.SetElementValue("text-for-cell-header", txtStextforcellheader.Text);
            itemElement.SetElementValue("IsCellFormat", Convert.ToInt16(chkIsCellFormat.Checked));
            itemElement.SetElementValue("IsCellBold", Convert.ToInt16(chkIsCellBold.Checked));
            itemElement.SetElementValue("FontSize", txtFontSize.Text);
            itemElement.SetElementValue("FontStyle", ddlFontStyle.SelectedValue);
            itemElement.SetElementValue("BGColor", ddlBGColor.SelectedValue);
            itemElement.SetElementValue("ForeColor", ddlForeColor.SelectedValue);
            itemElement.SetElementValue("cellNumberFormat", txtcellNumberFormat.Text);
            itemElement.SetElementValue("CellWarping", Convert.ToInt16(chkCellWarping.Checked));
            itemElement.SetElementValue("CellMerge", Convert.ToInt16(chkCellMerge.Checked));
            itemElement.SetElementValue("CellMergeLength", txtCellMergeLength.Text);
            itemElement.SetElementValue("TextAlignment", ddlTextAlignment.SelectedValue);
          
            itemElement.SetElementValue("Visible", Convert.ToInt16(chkIsVisible.Checked));
            itemElement.SetElementValue("IsCellItalic", chkIsCellItalic.Text);


            break;
        }

        xmlDoc.Save(oXs.XMLFilePath);
        lblMessage.Visible = true;
        lblMessage.Text = "Setting is updated successfully.";

    }
}