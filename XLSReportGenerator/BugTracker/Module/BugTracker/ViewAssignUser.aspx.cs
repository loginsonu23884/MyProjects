using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_BugTracker_ViewAssignUser : System.Web.UI.Page
{
    public string OAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    public string username = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            var reportIssueId = Convert.ToInt32(Request.Params["ReportIssueID"]);
            var objBt = new BTprovider();
            var dt = new DataTable();
            dt = objBt.GetAssignedUser(reportIssueId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    username += dr["UserName"].ToString() + "<br />";
                }
            }
        }
    }
}