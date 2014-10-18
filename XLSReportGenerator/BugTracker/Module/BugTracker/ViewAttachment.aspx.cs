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

public partial class Module_BugTracker_ViewAttachment : System.Web.UI.Page
{
    public string UserMsg = "";
    public string OAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            var reportIssueId = Convert.ToInt32(Request.Params["ReportIssueID"]);
            var objBt = new BTprovider();
            var dt = new DataTable();
            dt = objBt.Getattachmentdetail(reportIssueId);
            if (dt.Rows.Count > 0)
            {
                UserMsg ="<table cellpadding='0' cellspacing='0' border='1' style='width:80%;'>";
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    UserMsg = UserMsg + "<tr>" + "<td>" + dt.Rows[j]["filename"] + "</td>" + "<td align='center'><a href='" + OAppPath + "/Module/BugTracker/Download.aspx?ID=" + dt.Rows[j]["ID"] + "&Flag=0'><img src='" + OAppPath + "/images/images.jpg' /></a></td>" + "</tr>";

                }
                UserMsg = UserMsg + "</table>";
            }
        }
    }
}