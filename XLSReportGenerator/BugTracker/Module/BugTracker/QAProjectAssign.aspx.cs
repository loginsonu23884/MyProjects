using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_BugTracker_QAProjectAssign : System.Web.UI.Page
{
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    public int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        lblMessage.Text = "";
        if(!IsPostBack)
        {
            //Creating class object
            var objBT = new BT_DropDownHandler();
            objBT.BindQA(ddlQA);
            BindProjectName();
            if (Session["UserId"] != null)
            {
                UserId = (int)(Session["UserId"]);
            }
            ddlQA.SelectedIndex = UserId;
            SetSelectedCheckBox();
        }
    }
    public void SetSelectedCheckBox()
    {
        var objBT = new BT_DropDownHandler();
        var dt = objBT.ChkQAAssignProject(Convert.ToInt32(ddlQA.SelectedItem.Value)); 
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
        var dt = objBT.BindConfiguredProject();
        if (dt != null)
        {
            chkProjectName.DataValueField = "ProjectId";
            chkProjectName.DataTextField = "name";
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
            var objBTprovider = new BTprovider();
            objBTprovider.Userid = Convert.ToInt32(ddlQA.SelectedItem.Value);
            objBTprovider.ConfigureProjectID = idList;
            var flag = objBTprovider.SaveQAAssignProject(objBTprovider);
            if (flag > 0)
            {
                SetSelectedCheckBox();
                lblMessage.Visible = true;
                lblMessage.Text = "Project Assign sucessfully";
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "There is an some issue while assigning";
            }
        }
        else
        {
            var objBT = new BTprovider();
            objBT.Userid = Convert.ToInt32(ddlQA.SelectedItem.Value);
            objBT.ConfigureProjectID = idList;
            var result = objBT.SaveQAAssignProject(objBT);
            lblMessage.Visible = true;
            lblMessage.Text = "You have not selected project for assign list.";
        }
    }
    protected void ddlQA_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProjectName();
        SetSelectedCheckBox();
    }
}