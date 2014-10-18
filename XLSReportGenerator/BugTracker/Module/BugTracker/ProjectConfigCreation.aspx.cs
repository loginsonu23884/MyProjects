using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_ProjectConfigCreation : System.Web.UI.Page
{
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            // call method for checkbox list
              BindProjectName();
              SetSelectedCheckBox();
            
             
        }
        
    }
    public void SetSelectedCheckBox()
    {
        var objBT = new BT_DropDownHandler();
        var dt = objBT.ChkConfiguredProject();
        foreach (DataRow dr1 in dt.Rows)
        {
            foreach (ListItem item in chkProjectName.Items)
            {

                if (item.Value == dr1["ProjectId"].ToString())
                {
                    item.Selected = true;
                    item.Attributes.Add("style", "color:blue");
                    break;
                }
            }
        }
    }
    //Method for bind checkbox list
    public void BindProjectName()
    {
        //creating class object...
        var objBT = new BT_DropDownHandler();
        //passing parameter value into method
        var dt = objBT.BindProjectName();
        if (dt != null)
        {
            chkProjectName.DataValueField = "ID";
            chkProjectName.DataTextField = "Name";
            chkProjectName.DataSource = dt;
            chkProjectName.DataBind();
        }
    }

    // function for checkbox list Id
    public string GetIdList(CheckBoxList chkList)
    {
        string strParameter = "";

        if (chkList != null)
        {
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        if (strParameter == "")
                        {
                            strParameter = list.Value;
                        }
                        else
                        {
                            strParameter = strParameter + "," + list.Value + "";
                        }

                    }
                }
            }
        }
        return strParameter;
    }

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
              string idList = GetIdList(chkProjectName);
              if (idList != "")
             {  
                var objBT = new BTprovider();
                objBT.ConfigureProjectID = idList;
                var flag = objBT.SaveProjectConfigured(objBT);
                if(flag > 0)
                {
                    SetSelectedCheckBox();
                    lblMessage.Visible = true;
                    lblMessage.Text = "Project Configured sucessfully";
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "There is an some issue while configuring";
                }
             }
              else
              {
                  var objBT = new BTprovider();
                  objBT.ConfigureProjectID = idList;
                  var result = objBT.SaveProjectConfigured(objBT);
                  lblMessage.Visible = true;
                  lblMessage.Text = "You have not selected project for configure list.";
              }

    }
}