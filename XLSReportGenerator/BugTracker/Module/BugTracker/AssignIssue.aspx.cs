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

public partial class Module_BugTracker_AssignIssue : System.Web.UI.Page
{
    public int UserId = 0;
    public int flag;
    public int Assignedtouserid ;
    public string Assigned;
    public int Reportissueid;
    public string Issueno;
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack) 
        {
        var objBT = new BT_DropDownHandler();
        objBT.BindAssignedto(ddlassignedto);
        ddlassignedto.SelectedValue = Request.QueryString["Assignedtouserid"];
       
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        var Reportissueid = Convert.ToInt32(Request.QueryString["Reportissueid"]);
        var Assignedtouserid = Convert.ToInt32(ddlassignedto.SelectedValue);
        //Issueno =(Request.QueryString["IssueNo"]);

        //BTprovider objBT1 = new BTprovider();
        //DataTable dt = new DataTable();

        //dt = objBT1.GetIssueDetail(Reportissueid);

      
        var objBP = new BTprovider();
        var dt = objBP.GetEmailDetails(Assignedtouserid);
        //litScript.Text = "<script type='text/javascript'>;parent.$.fancybox.close();</script>";

        //litScript.Text = "<script type='text/javascript'>;parent.location.reload(true); </script>";
        //litScript.Text = "<script type='text/javascript'>parent.$.fancybox.close();parent.location.reload(true); </script>";
        var objE = new Emailer();
        var replacementParam = new string[2, 2];
        replacementParam[0, 0] = "@ContactName";
        replacementParam[0, 1] =  dt.Rows[0]["Name"].ToString();
        replacementParam[1, 0] = "@Body";
        replacementParam[1, 1] = txtDetail.Value;
        objE.SendEmail(replacementParam, "AI",dt.Rows[0]["EmailId"].ToString(), false, "");
        // for unassigned.......
        int olduserId = Convert.ToInt32(Request.QueryString["Assignedtouserid"]);
        var dtold = objBP.GetEmailDetails(olduserId);
        var replacementParamNew = new string[1, 2];
        replacementParamNew[0, 0] = "@ContactName";
        replacementParamNew[0, 1] = dtold.Rows[0]["Name"].ToString();
        objE.SendEmail(replacementParamNew, "UAI", dtold.Rows[0]["EmailId"].ToString(), false, "");
        UpdateReportIssues(Assignedtouserid, Reportissueid);
    }

    public void UpdateReportIssues(int Assignedtouserid, int Reportissueid)
    {

        var objBTprovider = new BTprovider();
        objBTprovider.Assignedtouserid = Assignedtouserid;
        objBTprovider.ReportIssueid = Reportissueid;
        objBTprovider.UpdateReportissue(objBTprovider);

        ltfancycloseparentupload.Text = "<script type='text/javascript'>parent.$.fancybox.close(); parent.window.location.href=parent.window.location.href; </script>";
        //litScript.Text = "<script type='text/javascript'>parent.$.fancybox.close();</script>";
        //Response.Redirect("Communication.aspx?Reportissueid=" + Reportissueid + " ");
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        ltfancycloseparentupload.Text = "<script type='text/javascript'>parent.$.fancybox.close();</script>";
    }
}
