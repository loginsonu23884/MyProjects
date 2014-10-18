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

public partial class Module_BugTracker_Communication : System.Web.UI.Page
{
  
    public string Category;
    public string FilePath = "";
    public int UserId = 0;
    public int FileUploadCount;
    public static string IssueNo;
    public string UserMsg="";
    public string Attachmentsstring = "";
    public string str = "";
    public string str1 = "";
    public  string Priority;
    public int StatusId = 0;
    public  string Status;
    public  string Duedate;
    public  int Reportissueid;
    public int Communicationid;
    public  string Assigned;
    public  int Assignedtouserid;
    public  string Summary;
    public  string Reportedby;
    public  string ProjectName;
    public  string CreatedOn;
    public string Statusidforcloseissue;
    public string Description;
    

     DataTable dt = new DataTable();
    public string OAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();

    protected void Page_Load(object sender, EventArgs e)
    {
         Reportissuedetail();
         if (Session["RoleTypeID"].ToString() == "7")
         {
             btnEdit.Visible = false;
         }
       
    }

    public void Reportissuedetail()
    {
        int reportIssueId = 0;
        if (Request.QueryString["ReportIssueid"] != null)
        {
            reportIssueId = Convert.ToInt32(Request.QueryString["ReportIssueid"]);
        }
        var objBt = new BTprovider();
        var dt = new DataTable();
        dt = objBt.GetIssueDetail(reportIssueId);
        if (dt != null && dt.Rows.Count > 0)
        {
            Summary = dt.Rows[0]["Summary"].ToString();
            Priority = dt.Rows[0]["Priority"].ToString();
            Status = dt.Rows[0]["StatusName"].ToString();
            Duedate = dt.Rows[0]["Duedate"].ToString();
            IssueNo = dt.Rows[0]["IssueNo"].ToString();
            Assigned = dt.Rows[0]["MailUserName"].ToString();
            ProjectName = dt.Rows[0]["ProjectName"].ToString();
            Reportedby = dt.Rows[0]["username"].ToString();
            CreatedOn = dt.Rows[0]["PostDate"].ToString();
            Assignedtouserid = Convert.ToInt32(dt.Rows[0]["Assignedtouserid"]);
            StatusId = Convert.ToInt32(dt.Rows[0]["Statusid"]);
            Description = dt.Rows[0]["DetectDescription"].ToString();
        }

        Attachmentsstring = "<table  class='summeryTable' cellpadding='1' cellspacing='1' border='0' style='; width:100% '>";

        Attachmentsstring = Attachmentsstring + "<tr><td>";

        string strDynamicqueryAttachment = "select * from  SaveAttchmentsDetail where ReportIssueid=" + reportIssueId;
                var dtattachments = new DataTable();
                dtattachments = objBt.GetQueryExecute(strDynamicqueryAttachment);
                    if (dtattachments.Rows.Count > 0)
                    {
                        Attachmentsstring = Attachmentsstring + " <br /> <b>Attachments:-</b> ";
                        for (int j = 0; j < dtattachments.Rows.Count; j++)
                        {

                            Attachmentsstring = Attachmentsstring + "<a href='/SalesReporting/Module/BugTracker/Download.aspx?flag=1&ID=" +
                                      dtattachments.Rows[j]["ID"] + "'>" + dtattachments.Rows[j]["filename"] +
                                      " </a>&nbsp;&nbsp;&nbsp;";
                            //UserMsg = UserMsg + "<hr/>";

                        }

                    }
                    Attachmentsstring = Attachmentsstring + "<hr/></td></tr>";
                    Attachmentsstring = Attachmentsstring + "</table>";


                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Statusid"].ToString() == "1")
                        {
                            btnreopen.Visible = false;
                            btncloseissue.Visible = true;
                            btnresolve.Visible = true;
                        }

                        //Resolved
                        if (dt.Rows[0]["Statusid"].ToString() == "2")
                        {
                            btnreopen.Visible = true;
                            btncloseissue.Visible = true;
                            btnresolve.Visible = false;
                        }
                        //Closed
                        if (dt.Rows[0]["Statusid"].ToString() == "3")
                        {
                            btnreopen.Visible = true;
                            btncloseissue.Visible = false;
                            btnresolve.Visible = false;
                        }
                        if (dt.Rows[0]["Statusid"].ToString() == "5")
                        {
                            btnreopen.Visible = false;
                            btncloseissue.Visible = true;
                            btnresolve.Visible = true;
                        }

                    }
        hdnreportissueid.Value = Convert.ToString(reportIssueId);
        hdnassigndetouserid.Value = Convert.ToString(Assignedtouserid);
        hdnissueno.Value = IssueNo.ToString();
        dt = objBt.GetCommuncationDetail(reportIssueId);
        DataTable dtattach;
        int UserId = (int)(Session["UserId"]);
        if (dt != null)
        {
            //string userMsg = "";
            if (dt.Rows.Count > 0)
            {
                if (UserId == (int)(Session["UserId"]))
                {
                    str = "style='display: block; float: right; margin-right: 48px;'";
                    str1 = "style='display: block; float: right; margin-left: 0px; margin-right: -70px;'";
                }
                else
                {
                    str = "style='display:none'";
                    str1 =" style='display:none'";
                }
                UserMsg = "<table class='summeryTable' cellpadding='1' cellspacing='1' border='0' style='; width:100% '>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserMsg = UserMsg + "<tr ><td style='border-left-width: 0px; margin-left: 0px;'><b>" + dt.Rows[i]["Name"] + "</b> added a comments on - " + dt.Rows[i]["Date"] + "<img src='/BugTracker/Images/edit.png' " + str + "  onclick='javascript:funCommentedit(\"" + dt.Rows[i]["CommunicationID"].ToString() + "\");' /><img src='/BugTracker/Images/delete.gif' " + str1 + " onclick='javascript:funCommentdelete(\"" + dt.Rows[i]["CommunicationID"].ToString() + "\");'  /></td></tr>";
                    UserMsg = UserMsg + "<tr><td><br />" + dt.Rows[i]["Comments"] + " </td></tr>";
                    UserMsg = UserMsg + "<tr><td>";

                    string strDynamicqueryAttach = "select * from CommunicationAttachments where communicationid=" + dt.Rows[i]["communicationid"];
                    dtattach = objBt.GetQueryExecute(strDynamicqueryAttach);
                    if (dtattach.Rows.Count > 0)
                    {
                        UserMsg = UserMsg + " <br /> <b>Attachments:-</b> ";
                        for (int j = 0; j < dtattach.Rows.Count; j++)
                        {
                          
                            UserMsg = UserMsg + "<a href='/SalesReporting/Module/BugTracker/Download.aspx?flag=1&ID=" +
                                      dtattach.Rows[j]["ID"] + "'>" + dtattach.Rows[j]["filename"] +
                                      " </a>&nbsp;&nbsp;&nbsp;";
                            //UserMsg = UserMsg + "<hr/>";

                        }

                    }
                    UserMsg = UserMsg + " <hr/></td></tr>";
                   
                }

                UserMsg = UserMsg + "</table>";
            }
        }
        
    }


    protected void btnPost_Click(object sender, EventArgs e)
    {
        int reportIssueId = 0;
        int communcationid = 0;
        if (Request.QueryString["ReportIssueid"] != null)
        {
            reportIssueId = Convert.ToInt32(Request.QueryString["ReportIssueid"]);
        }

        int UserId = (int)(Session["UserId"]);
        //int file;
        var objBt = new BTprovider
            {
                ReportIssueid = reportIssueId,
                Userid = UserId,
                Comments = txtDetail.Text,
                Statusid = StatusId
            };
        communcationid = objBt.SaveCommunicationDetails(objBt);
        string txtValue = "";
        string fileName = "";
        string fileType = "";
        string saveFile = "";
        var binData = new byte[] {};
        //** retrive files from multiple file uploader & used in insert into datatbase
        var oDt = new DataTable();
        int j = 0;
        FileUploadCount = Request.Files.Count;
        // fileUploadCount = fileUploadCount + 1;
        for (int i = 0; i < FileUploadCount; i++)
        {
            string[] textboxValues = Request.Form.GetValues("textbox" + i);
            txtValue = textboxValues[0].Trim();
            HttpPostedFile postedFile = Request.Files[i];
            if (postedFile.ContentLength > 0)
            {
                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileType = postedFile.ContentType;
                var b = new BinaryReader(postedFile.InputStream);
                binData = b.ReadBytes(postedFile.ContentLength);

            }

            objBt.Communicationid = communcationid;
            ;
            objBt.FileContent = binData;
            objBt.FileName = fileName;
            objBt.FileType = fileType;
            objBt.FileDetail = txtValue;
            int temp = (binData.Length);
            if (temp != 0 && fileName != "" && fileType != "" && txtValue != "")
            {
                objBt.SaveFileDetail(objBt);
            }
        }
        txtDetail.Text = "";
        Reportissuedetail();

    }


    protected void Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportIssues.aspx?ReportIssueid=" + Convert.ToInt32(Request.Params["ReportIssueid"]) +"");
    }

    public void UpdateStatus(int statusid)
    {
        var objBTprovider = new BTprovider
            {
                ReportIssueid = Convert.ToInt32(Request.Params["ReportIssueid"]),
                Statusid = statusid
            };
        objBTprovider.UpdateStatus(objBTprovider);
    }

    protected void Reopen_Click(object sender, EventArgs e)
    {
        UpdateStatus(1);
        Reportissuedetail();

    }
    protected void Close_Click(object sender, EventArgs e)
    {

        UpdateStatus(3);
        Reportissuedetail();
        //var objBP = new BTprovider();
        //var dt = objBP.GetReportIssueDetails(Reportissueid);
        //var objE = new Emailer();
        //var replacementParam = new string[11, 2];
        //replacementParam[0, 0] = "@ContactName";
        //replacementParam[0, 1] =  Assigned;
        //replacementParam[1, 0] = "@ProjectName";
        //replacementParam[1, 1] = dt.Rows[0]["Assigned"].ToString();
        //replacementParam[2, 0] = "@Reporter";
        //replacementParam[2, 1] = dt.Rows[0]["Name"].ToString();
        //replacementParam[3, 0] = "@Summary";
        //replacementParam[3, 1] = dt.Rows[0]["Summary"].ToString();
        //replacementParam[4, 0] = "@Category";
        //replacementParam[4, 1] = dt.Rows[0]["Category"].ToString();
        //replacementParam[5, 0] = "@AssignUser";
        //replacementParam[5, 1] = dt.Rows[0]["Assigned"].ToString();
        //replacementParam[6, 0] = "@AttachmentsName";
        //replacementParam[6, 1] = "";
        //replacementParam[7, 0] = "@CreatedDate";
        //replacementParam[7, 1] = dt.Rows[0]["PostDate"].ToString();
        //replacementParam[8, 0] = "@Description";
        //replacementParam[8, 1] = dt.Rows[0]["Description"].ToString();
        //replacementParam[9, 0] = "@ProjectName";
        //replacementParam[9, 1] = dt.Rows[0]["ProjectName"].ToString();
        //replacementParam[10, 0] = "@Priority";
        //replacementParam[10, 1] = dt.Rows[0]["Priority"].ToString();
        //objE.SendEmail(replacementParam, "ANI", dt.Rows[0]["email"].ToString(), false, "");
    }
    protected void Resolve_Click(object sender, EventArgs e)
    {
        var reportissueid = Convert.ToInt32(Request.Params["ReportIssueid"]);
       

        ltresolve.Text = "<script type='text/javascript'>Resolveissue(); </script>";

        // UpdateStatus(2);
        //Reportissuedetail();
    }
}