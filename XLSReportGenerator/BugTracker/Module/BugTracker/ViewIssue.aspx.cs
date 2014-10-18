using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class Module_BugTracker_ReportIssueList : System.Web.UI.Page
{
     public int UserId = 0;

    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    // calling class object

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
         {
          Response.Redirect(oAppPath + "/Loginpage.aspx");
         }
         BindDropDowns();
         rblcategory.SelectedValue = "1";
         UserId = (int)(Session["UserId"]);
         // to check session value
         if (Session["RoleTypeID"] != null)
         {
             // to check session value
             if (Session["RoleTypeID"].ToString() == "6")
             {
                 trAssignedTo.Visible = true;
             }
         }
        //txtDate.Text = System.DateTime.Now.AddDays(-18).ToShortDateString();
        //txtDateTo.Text = System.DateTime.Now.ToShortDateString();
    }

    public void BindDropDowns()
    {
        //Creating class object
        var objBt = new BT_DropDownHandler();
        objBt.BindStatuslistbox(lststatus);
        objBt.BindPrioritylistbox(lstpriority);
        objBt.BindAssignedtolistbox(lstassignedto);
        objBt.BindProjectlistbox(lstproject);
        rblcategory.DataSource = objBt.BindCategory();
        rblcategory.DataValueField = "Categoryid";
        rblcategory.DataTextField = "Category";
        rblcategory.DataBind();

    }
}