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

public partial class Module_BugTracker_ReportIssues : System.Web.UI.Page
{
    //public string Category;
    public string filePath = "";
    public int UserId = 0;
    public string userMsg = "";
    public int fileUploadCount=0;
    public int Reportissueid;
  
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();


    protected void Page_Load(object sender, EventArgs e)
    {
        Reportissueid = Convert.ToInt32(Request.QueryString["ReportIssueid"]);

        if (Reportissueid > 0)
        {
            btnReset.Text = "Update";
            btnPost.Visible = false;
            btncancel.Visible = true;
        }
        else
        {
            btnReset.Text = "Submit & List";

        }
        lblrecordsaved.Visible = false;
        if (Session["RecordSaved"] != null)
        {

            lblrecordsaved.Visible = true;
            lblrecordsaved.Text = " Record save successfully.";
            Session["RecordSaved"] = null;
        }


        if (!IsPostBack)
        {
           
            //if (Session["DtIsEmail"] != null)
            //{   
            //    var dtIsEmail1 = new DataTable();
            //    dtIsEmail1 = (DataTable)(Session["DtIsEmail"]);
            //    gv.DataSource = dtIsEmail1;
            //    gv.DataBind();
            //}
            if (Reportissueid > 0)
            {

                var objBT2 = new BTprovider();
                var dt1 = new DataTable();

                dt1 = objBT2.GetIssueDetail(Reportissueid);
                var priorityid = dt1.Rows[0]["Priorityid"].ToString();
                (ddlpriority.SelectedValue) = priorityid;
                var Assignedtouserid = dt1.Rows[0]["Assignedtouserid"].ToString();
                //ddlassignedto.SelectedValue = Assignedtouserid;
                txtduedate.Text = dt1.Rows[0]["Duedate"].ToString();
                var statusid = dt1.Rows[0]["Statusid"].ToString();
                ddlstatus.SelectedValue = statusid;
                var Reportedby = dt1.Rows[0]["username"].ToString();
                var CreatedOn = dt1.Rows[0]["PostDate"].ToString();
                ddlproject.SelectedValue = dt1.Rows[0]["Projectid"].ToString();
                //var test = Convert.ToInt32(ddlproject.SelectedItem.Value);
                var objBT = new BTprovider();
                var dtAssignmentNo = new DataTable();
                dtAssignmentNo = objBT.GetAssignmentNo(Convert.ToInt32(dt1.Rows[0]["Projectid"].ToString()));
                txtAssignmentNo.Text = dtAssignmentNo.Rows[0]["AssignmentNo"].ToString();
                rblcategory.SelectedValue = dt1.Rows[0]["Categoryid"].ToString();
                txtsummary.Text = dt1.Rows[0]["Summary"].ToString();
                ddlreproducibilty.SelectedValue = dt1.Rows[0]["Reproducibilityid"].ToString();
                ddlseverity.SelectedValue = dt1.Rows[0]["Severityid"].ToString();
                ddlbrowser.SelectedValue = dt1.Rows[0]["Browserid"].ToString();
                ddlOSVersion.SelectedValue = dt1.Rows[0]["OSVersionid"].ToString();
                //txtreproduce.Text = dt1.Rows[0]["Description"].ToString();
                txtdefectdescription.Text = dt1.Rows[0]["DetectDescription"].ToString();
                txtadditionalinformation.Text = dt1.Rows[0]["AdditionalInformation"].ToString();
                txtexpectedresult.Text = dt1.Rows[0]["ExpectedResult"].ToString();
                txtactualresult.Text = dt1.Rows[0]["ActualResult"].ToString();
                BTprovider objBT1 = new BTprovider();
                var dt = new DataTable();
                dt = objBT1.GetAttachmentDetailOnBasisOfReportIssueId(Reportissueid);
                userMsg = "" + "<table  cellpadding='1' cellspacing='1' border='0' style='width:98%;'>";
                userMsg = userMsg + "<tr><td colspan='2'>" + "<hr/>" + "</td></tr>";
                userMsg = userMsg + "<tr><td colspan='2'>";
                if (dt.Rows.Count > 0)
                {
                    userMsg = userMsg + " <br /> <b>Attachments:-</b> ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        {
                            userMsg = userMsg + "<a href='/SalesReporting/Module/BugTracker/Download.aspx?flag=0&ID=" +
                            dt.Rows[i]["ID"] + "'>" + dt.Rows[i]["filename"] + " </a>&nbsp;&nbsp;&nbsp; ";
                        }

                    }
                }
                userMsg = userMsg + "<hr/>";


                userMsg = userMsg + "</td></tr>" + "</table>";
            }

           //Calling method to bind dropdowns
          
            BindDropDowns();


        }
        //if (Session["RoleTypeID"] != null && Session["RoleTypeID"].ToString() != "0")
        //{
        //    var objBT = new BTprovider();
        //    var dtAssignmentNo = new DataTable();
        //    dtAssignmentNo = objBT.GetAssignmentNo(Convert.ToInt32(ddlproject.SelectedItem.Value));
        //    txtAssignmentNo.Text = dtAssignmentNo.Rows[0]["AssignmentNo"].ToString();
        //}
    }
    public void BindDropDowns()
    {
        //Creating class object
        var objBT = new BT_DropDownHandler();
        objBT.BindReproducibilty(ddlreproducibilty);
        objBT.BindSeverity(ddlseverity);
        objBT.BindPriority(ddlpriority);
        objBT.BindStatusIssue(ddlstatus);
        objBT.BindBrowser(ddlbrowser);
        objBT.BindOS(ddlOSVersion);
        //objBT.BindAssignedto(ddlassignedto);
        if (Session["UserId"] != null)
        {
            UserId = (int)(Session["UserId"]);
        }
        if (Session["RoleTypeID"] != null && Session["RoleTypeID"].ToString() == "0")
        {
            objBT.BindProject(ddlproject);
        }
        else
        {
            objBT.BindQAAssignProject(ddlproject, UserId);
            ddlproject.Items.Insert(0, new ListItem("Select One", "0"));
        }
        rblcategory.DataSource = objBT.BindCategory();
        rblcategory.DataValueField = "Categoryid";
        rblcategory.DataTextField = "Category";
        rblcategory.DataBind();

    }

    protected void btnPost_Click1(object sender, EventArgs e)
    {
        // if page validation is true
        
          if (Reportissueid > 0)
            {
                Updateissue(false);
            }
            else
            {
                Postissue(false);
            }
          
          
    }

    public void Updateissue(bool IsRedirect)
    {
        int UserId = (int)(Session["UserId"]);
     
        //Creating class object
        var objBTprovider = new BTprovider();
        objBTprovider.CategoryID = Convert.ToInt32(rblcategory.SelectedValue);
        objBTprovider.ReproducibilityID = Convert.ToInt32(ddlreproducibilty.SelectedValue);
        objBTprovider.SeverityID = Convert.ToInt32(ddlseverity.SelectedValue);
        objBTprovider.PriorityID = Convert.ToInt32(ddlpriority.SelectedValue);
        objBTprovider.BrowserID = Convert.ToInt32(ddlbrowser.SelectedValue);
        objBTprovider.OSVersionID = Convert.ToInt32(ddlOSVersion.SelectedValue);
        //objBTprovider.Assignedtouserid = Convert.ToInt32(ddlassignedto.SelectedValue);
        //objBTprovider.Description = txtreproduce.Text;
        objBTprovider.DefectDescription = txtdefectdescription.Text;
        objBTprovider.AdditionalInformation = txtadditionalinformation.Text;
        objBTprovider.ExpectedResult = txtexpectedresult.Text;
        objBTprovider.ActualResult = txtactualresult.Text;
        objBTprovider.Duedate = Convert.ToDateTime(txtduedate.Text);
        objBTprovider.ReportIssueid = Reportissueid;
        objBTprovider.Statusid = Convert.ToInt32(ddlstatus.SelectedValue);
        objBTprovider.Projectid = Convert.ToInt32(ddlproject.SelectedValue);
        objBTprovider.Summary = txtsummary.Text;
        objBTprovider.Userid = UserId;
        objBTprovider.UpdateBTProvider(objBTprovider);
        string txtValue = "";
        string FileName = "";
        string FileType = "";
        string saveFile = "";
        var binData = new byte[] { };
        //** retrive files from multiple file uploader & used in insert into datatbase
        var oDt = new DataTable();
        int j = 0;
        fileUploadCount = Request.Files.Count;
        // fileUploadCount = fileUploadCount + 1;
        for (int i = 0; i < fileUploadCount; i++)
        {
            string[] textboxValues = Request.Form.GetValues("textbox" + i);
            txtValue = textboxValues[0].Trim();
            var postedFile = Request.Files[i];
            if (postedFile.ContentLength > 0)
            {
                FileName = System.IO.Path.GetFileName(postedFile.FileName);
                FileType = postedFile.ContentType;
                var b = new BinaryReader(postedFile.InputStream);
                binData = b.ReadBytes(postedFile.ContentLength);
                objBTprovider.ReportIssueid = Reportissueid;
                objBTprovider.FileContent = binData;
                objBTprovider.FileName = FileName;
                objBTprovider.FileType = FileType;
                objBTprovider.FileDetail = txtValue;
                int temp = (binData.Length);
                if (temp != 0 && FileName != "" && FileType != "" && txtValue != "")
                {
                    objBTprovider.SaveAttachmentsdetails(objBTprovider);
                }
            }


        }

      


        if (Reportissueid > 0)
        {
            if (IsRedirect)
            {
                // To add session value
                //Session.Add("S", "1");
                Response.Redirect("Communication.aspx?ReportIssueid="+Reportissueid + " ");
            }
            else
            {
                Response.Redirect(Request.RawUrl);

            }
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = Resources.SalesReporting.ThereIsIssueWhilePosting;

        }

    }

    public void Postissue(bool IsRedirect)
    {
        int UserId = (int)(Session["UserId"]);
        //Creating class object
        var objBTprovider = new BTprovider();
        objBTprovider.CategoryID = Convert.ToInt32(rblcategory.SelectedValue);
        objBTprovider.ReproducibilityID = Convert.ToInt32(ddlreproducibilty.SelectedValue);
        objBTprovider.SeverityID = Convert.ToInt32(ddlseverity.SelectedValue);
        objBTprovider.PriorityID = Convert.ToInt32(ddlpriority.SelectedValue);
        objBTprovider.BrowserID = Convert.ToInt32(ddlbrowser.SelectedValue);
        objBTprovider.OSVersionID = Convert.ToInt32(ddlOSVersion.SelectedValue);
        //objBTprovider.Assignedtouserid = Convert.ToInt32(ddlassignedto.SelectedValue);
        //objBTprovider.Description =  txtreproduce.Text;
        objBTprovider.DefectDescription = txtdefectdescription.Text;
        objBTprovider.AdditionalInformation = txtadditionalinformation.Text;
        objBTprovider.ExpectedResult = txtexpectedresult.Text;
        objBTprovider.ActualResult = txtactualresult.Text;
        objBTprovider.Duedate = Convert.ToDateTime(txtduedate.Text);
        objBTprovider.AssignmentNo = txtAssignmentNo.Text.Trim();
        objBTprovider.Statusid = Convert.ToInt32(ddlstatus.SelectedValue);
        objBTprovider.Projectid = Convert.ToInt32(ddlproject.SelectedValue);
        objBTprovider.Summary = txtsummary.Text;
        objBTprovider.Userid = Convert.ToInt16(UserId);
        var dtIsEmail = new DataTable();
        if (Session["DtIsEmail"] != null)
         {
           dtIsEmail = (DataTable) (Session["DtIsEmail"]);
         }
        objBTprovider.IsEmail = dtIsEmail;
        var Reportissueid = objBTprovider.SaveBTProvider(objBTprovider);
        
        string txtValue = "";
        string FileName = "";
        string FileType = "";
        string saveFile = "";
        byte[] binData = new byte[] { };
        //** retrive files from multiple file uploader & used in insert into datatbase
        DataTable oDt = new DataTable();
        int j = 0;
        fileUploadCount = Request.Files.Count;
        // fileUploadCount = fileUploadCount + 1;
        for (int i = 0; i < fileUploadCount; i++)
        {
            string[] textboxValues = Request.Form.GetValues("textbox" + i);
            txtValue = textboxValues[0].Trim();
            HttpPostedFile PostedFile = Request.Files[i];
            if (PostedFile.ContentLength > 0)
            {
                FileName = System.IO.Path.GetFileName(PostedFile.FileName);
                FileType = PostedFile.ContentType;
                BinaryReader b = new BinaryReader(PostedFile.InputStream);
                binData = b.ReadBytes(PostedFile.ContentLength);
                objBTprovider.ReportIssueid = Reportissueid;
                objBTprovider.FileContent = binData;
                objBTprovider.FileName = FileName;
                objBTprovider.FileType = FileType;
                objBTprovider.FileDetail = txtValue;
                int temp = (binData.Length);
                if (temp != 0 && FileName != "" && FileType != "" && txtValue != "")
                {
                    var file = 0;
                    file = objBTprovider.SaveAttachmentsdetails(objBTprovider);
                }

              
            }

        
        }
        if (Reportissueid > 0)
        {
            var objBP = new BTprovider();
            var dt = objBP.GetReportIssueDetails(Reportissueid);
            var objE = new Emailer();
            var replacementParam = new string[11, 2];
            replacementParam[0, 0] = "@IssueNo";
            replacementParam[0, 1] = dt.Rows[0]["IssueNo"].ToString();
            
            replacementParam[1, 0] = "@Reporter";
            replacementParam[1, 1] = dt .Rows[0]["Name"].ToString();
            replacementParam[2, 0] = "@Summary";
            replacementParam[2, 1] = dt.Rows[0]["Summary"].ToString();
            replacementParam[3, 0] = "@Category";
            replacementParam[3, 1] = dt.Rows[0]["Category"].ToString();
            replacementParam[4, 0] = "@CreatedDate";
            replacementParam[4, 1] = dt.Rows[0]["PostDate"].ToString();
            replacementParam[5, 0] = "@Description";
            replacementParam[5, 1] = dt.Rows[0]["DetectDescription"].ToString();
            replacementParam[6, 0] = "@ProjectName";
            replacementParam[6, 1] = dt.Rows[0]["ProjectName"].ToString();
            replacementParam[7, 0] = "@Priority";
            replacementParam[7, 1] = dt.Rows[0]["Priority"].ToString();
            replacementParam[8, 0] = "@AssignmentNo ";
            replacementParam[8, 1] = dt.Rows[0]["AssignmentNo"].ToString();

            if (Session["DtIsEmail"] != null)
            {
                dtIsEmail = (DataTable)(Session["DtIsEmail"]);
            }
            var tostring = "";
            var ccstring = "";
            var bccstring = "";
            foreach (DataRow dr in dtIsEmail.Rows)
            {

               if(dr["emailName"].ToString() == "To" )
               {
                   var objBP1 = new BTprovider();
                   var dtemail = objBP1.GetEmails(dr["userName"].ToString());
                   if(dtemail != null && dtemail.Rows.Count > 0)
                   {
                       tostring += dtemail.Rows[0]["email"] + ",";
                   }
                   replacementParam[9, 0] = "@ContactName";
                   replacementParam[9, 1] = dr["userName"].ToString();
                   replacementParam[10, 0] = "@AssignUser";
                   replacementParam[10, 1] = dr["userName"].ToString();
               }
               else if (dr["emailName"].ToString() == "Cc")
               {
                   var objBP1 = new BTprovider();
                   var dtemail = objBP1.GetEmails(dr["userName"].ToString());
                   if (dtemail != null && dtemail.Rows.Count > 0)
                   {
                       ccstring += dtemail.Rows[0]["email"] + ",";
                   }
                   replacementParam[9, 0] = "@ContactName";
                   replacementParam[9, 1] = dr["userName"].ToString();
                   replacementParam[10, 0] = "@AssignUser";
                   replacementParam[10, 1] = dr["userName"].ToString();
               }
               else if (dr["emailName"].ToString() == "Bcc")
               {
                   var objBP1 = new BTprovider();
                   var dtemail = objBP1.GetEmails(dr["userName"].ToString());
                   if (dtemail != null && dtemail.Rows.Count > 0)
                   {
                       bccstring += dtemail.Rows[0]["email"] + ",";
                   }
                   replacementParam[9, 0] = "@ContactName";
                   replacementParam[9, 1] = dr["userName"].ToString();
                   replacementParam[10, 0] = "@AssignUser";
                   replacementParam[10, 1] = dr["userName"].ToString();
               }
            }
            var newtostring = "";
            var newccstring = "";
            var newbccstring = "";
            if(tostring != "")
            {
                newtostring = tostring.Remove(tostring.LastIndexOf(','));
            }
            if (ccstring != "")
            {
                newccstring = ccstring.Remove(ccstring.LastIndexOf(','));
            }
            if (bccstring != "")
            {
                newbccstring = bccstring.Remove(bccstring.LastIndexOf(','));
            }
             // bccstring.Remove(bccstring.LastIndexOf(','));
             objE.SendEmailNotification(replacementParam, "ANI", newtostring, newccstring, newbccstring);
           }
           if (IsRedirect)
            {
               Response.Redirect("ViewIssue.aspx");
            }
            else
            {
               Session.Add("RecordSaved","1");
               Response.Redirect(Request.RawUrl);
            }
    

   }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Reportissueid > 0)
            {
                Updateissue(true);

                //Response.Redirect("Communication.aspx");
            }
            else
            {
                Postissue(true);
            }
    
        }
    }
    protected void btnBackToList_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewIssue.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Communication.aspx?Reportissueid=" + Reportissueid + " ");
    }
   
}