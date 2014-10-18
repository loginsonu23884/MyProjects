using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.IO;



public partial class Module_BugTracker_Home : System.Web.UI.Page
{
    public static string cmnt = "";
    public static int count = 0;
    public string Assignedstring = "";
    public int Userid = 0;
    public string Name = "";
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {   
        if(!IsPostBack)
        {
            GetComments();
            GetAssignedIssues();
            ViewProjectStatusList();
       }
   }

    public void GetComments()
    {
        var objBt = new BTprovider();
        
        dt = objBt.GetTopTenComments();

        if (dt.Rows.Count > 5)
        {
            LinkButton1.Visible = true;
        }
        if (dt.Rows.Count > 0 && dt != null)
        {
            var loopcounter = 0;
            if(dt.Rows.Count < 5)
            {
                loopcounter = dt.Rows.Count;
            }
            else
            {
                loopcounter = 5;
            }
            cmnt = "<table class='summeryTable' cellpadding='1' cellspacing='1' border='0' style='; width:100% '>";

            for (int i = 0; i <loopcounter ; i++)
            {
                cmnt = cmnt + "<tr><td><hr/></td></tr><tr ><td><b>" + dt.Rows[i]["Name"] +
                       "</b> commented on - <a  href='#' onclick='javascript:funComments(\"" +
                       dt.Rows[i]["ReportIssueid"].ToString() + "\");' <b  style='margin-left:5px;'>Issueno - " +
                       dt.Rows[i]["Issueno"] + "</b></a> [ " + dt.Rows[i]["Date"] + "]</td></tr>";
                cmnt = cmnt + "<tr bgcolor='#C2D9EF'><td><br />" + dt.Rows[i]["Comments"] + " </td></tr>";
            }
        }
        cmnt = cmnt + "</table>";
    }


    public void GetAssignedIssues()
    {
        int UserId = (int)(Session["UserId"]);
        var objBt = new BTprovider();
        var dt = new DataTable();
        dt = objBt.GetAssignedIssuesToMe(UserId);
        gvassignedissues.DataSource = dt;
        gvassignedissues.DataBind();


    }
    // Method for showing gridview 
    public void ViewProjectStatusList()
    {
        //creating class object
        var objBT = new BTprovider();
        int UserId = (int)(Session["UserId"]);
        int roleTypeId = (int)(Session["RoleTypeID"]);
        // passing parameter value into method
        var dt = objBT.GetIssueStatus(UserId, roleTypeId);
        //Bind gridview from datatable
        gvProjectStatusList.DataSource = dt;
        gvProjectStatusList.DataBind();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton1.Visible = false;
        var objBt = new BTprovider();
        dt = objBt.GetTopTenComments();
        if (dt.Rows.Count > 0 && dt != null)
        {
            cmnt = cmnt +
                   "<table class='summeryTable' cellpadding='1' cellspacing='1' border='0' style='; width:100% '>";
            for (int i = 5; i < dt.Rows.Count; i++)
            {
                cmnt = cmnt + "<tr><td><hr/></td></tr><tr ><td><b>" + dt.Rows[i]["Name"] +
                       "</b> commented on - <a  href='#' onclick='javascript:funComments(\"" +
                       dt.Rows[i]["ReportIssueid"].ToString() + "\");' <b  style='margin-left:5px;'>Issueno - " +
                       dt.Rows[i]["Issueno"] + "</b></a> [ " + dt.Rows[i]["Date"] + "]</td></tr>";
                cmnt = cmnt + "<tr bgcolor='#C2D9EF'><td><br />" + dt.Rows[i]["Comments"] + " </td></tr>";
            }
        }
        cmnt = cmnt + "</table>";
    }
}