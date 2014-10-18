using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Reflection;

public partial class Setting_TableSetting : System.Web.UI.Page
{
    XMLService oXs = new XMLService();
    public string strTableName = "";
    XDocument xmlDoc;
    protected void Page_Load(object sender, EventArgs e)
    {
        xmlDoc = XDocument.Load(oXs.XMLFilePath);
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
               
                GettableSetting(Request.QueryString["TableName"].ToString());
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
    public void GettableSetting(string tablName)
    {
        List<XElement> oList = oXs.GetNodeElement(xmlDoc,"Table", "Name", tablName);
        foreach (XElement itemElement in oList)
        {
            txtStartingPosition.Text = itemElement.Element("Starting-Position-Of-A-Table").Value;
            txtStartingPositionRow.Text = itemElement.Element("Starting-Position-Of-A-Table-Row").Value;
            if (itemElement.Element("IsDobleHeader").Value == "1")
            {
                chkIsDobleHeader.Checked = true;
            }
            if (itemElement.Element("IsCellFormat").Value == "1")
            {
                chkIsCellFormat.Checked = true;
            }
            if (itemElement.Element("IsRowStyle").Value == "1")
            {
                chkIsRowStyle.Checked = true;
            }
           
           
            txtAlterNativeRowColor.Text = itemElement.Element("AlterNativeRowColor").Value;
            if (itemElement.Element("IsCellBold").Value == "1")
            {
                chkIsCellBold.Checked = true;
            }


            txtFontSize.Text = itemElement.Element("FontSize").Value;
            ddlFontStyle.SelectedValue = itemElement.Element("FontStyle").Value.ToUpper();
            ddlBGColor.SelectedValue = itemElement.Element("BGColor").Value.ToUpper();
            ddlForeColor.SelectedValue = itemElement.Element("ForeColor").Value.ToUpper();
            ddlTextAlignment.SelectedValue = itemElement.Element("TextAlignment").Value.Replace("left", "Left").Replace("right", "Right").Replace("center", "Center"); ;
            ddlBorderSize.SelectedValue = itemElement.Element("BorderSize").Value;
            if (itemElement.Element("IsconditionalStyle").Value == "1")
            {
                chkConditionalStyle.Checked = true;
            }
            
            if (itemElement.Element("IsLastRowColored").Value == "1")
            {
                chkIsLastRowColored.Checked = true;
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
        List<XElement> oList = oXs.GetNodeElement(xmlDoc, "Table", "Name", Request.QueryString["TableName"].ToString());
        foreach (XElement itemElement in oList)
        {
           
            itemElement.SetElementValue("Starting-Position-Of-A-Table", txtStartingPosition.Text);
            itemElement.SetElementValue("Starting-Position-Of-A-Table-Row", txtStartingPositionRow.Text);
            itemElement.SetElementValue("IsDobleHeader",Convert.ToInt16(chkIsDobleHeader.Checked));
            itemElement.SetElementValue("IsCellFormat", Convert.ToInt16(chkIsCellFormat.Checked));
            itemElement.SetElementValue("IsRowStyle", Convert.ToInt16(chkIsRowStyle.Checked));
            itemElement.SetElementValue("IsCellBold", Convert.ToInt16(chkIsCellBold.Checked));
            itemElement.SetElementValue("FontSize",txtFontSize.Text);
            itemElement.SetElementValue("FontStyle", ddlFontStyle.SelectedValue);
            itemElement.SetElementValue("BGColor", ddlBGColor.SelectedValue);
            itemElement.SetElementValue("ForeColor", ddlForeColor.SelectedValue);
            itemElement.SetElementValue("TextAlignment", ddlTextAlignment.SelectedValue);
            itemElement.SetElementValue("BorderSize", ddlBorderSize.SelectedValue);
            itemElement.SetElementValue("IsconditionalStyle", Convert.ToInt16(chkConditionalStyle.Checked));
            itemElement.SetElementValue("IsLastRowColored", Convert.ToInt16(chkIsLastRowColored.Checked));
            itemElement.SetElementValue("AlterNativeRowColor", txtAlterNativeRowColor.Text);
            

            break;
        }
        
        xmlDoc.Save(oXs.XMLFilePath);
        lblMessage.Visible = true;
        lblMessage.Text = "Setting is updated successfully.";
        
    }
}