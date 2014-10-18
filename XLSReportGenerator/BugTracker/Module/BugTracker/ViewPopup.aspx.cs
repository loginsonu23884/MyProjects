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

public partial class Module_BugTracker_VeiwPopup : System.Web.UI.Page
{
    public static int Reportedby = 0;
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    public string userMsg = "";
    public string filePath = "";
    public int j;
    public int fileUploadCount;
    public DataTable dtattach;
    public int Reportissueid;
    public string IssueNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            var communicationid = Convert.ToInt32(Request.Params["CommnicationID"]);
           // var objBT = new BT_DropDownHandler();
            //objBT.BindStatusIssue(ddlStatus);
            var objBt = new BTprovider();
            var dt = new DataTable();
            dt = objBt.GetCommentdetail(communicationid);
            if (dt != null)
            { 
                //string userMsg = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Reportedby = Convert.ToInt32(dt.Rows[i]["Reportedby"]);
                        txtDetail.Value = dt.Rows[i]["Comments"].ToString();
                        Reportissueid = Convert.ToInt16(dt.Rows[i]["ReportIssueid"]);
                        DataTable dtattach;
                        string strDynamicqueryAttach = "";

                        
                        var dt1 = new DataTable();
                        strDynamicqueryAttach = "select * from CommunicationAttachments where communicationid=" + communicationid+"  and FileContent  != 0x" ;
                        dtattach = objBt.GetQueryExecute(strDynamicqueryAttach);
                        if (dtattach.Rows.Count > 0)
                        {
                            userMsg = "" +
                                      "<table cellpadding='1' cellspacing='1' border='0' style='width:100%;'>";
                            for (int j = 0; j < dtattach.Rows.Count; j++)
                            {

                                userMsg = userMsg + "<tr>" + "" +
                                          "<td><a href='/SalesReporting/Module/BugTracker/Download.aspx?flag=1&ID=" +
                                          dtattach.Rows[j]["ID"] + "'>" + dtattach.Rows[j]["filename"] +
                                          " </a>&nbsp;&nbsp;&nbsp<a href='#' onclick='javascript:funattachmentdelete(\"" +
                                          dtattach.Rows[j]["ID"].ToString() + "\",\"" + IssueNo + "\",\"" +
                                          Reportissueid + "\");'  style='cursor:pointer;'>Delete</a> </td>" + "</tr>";

                            }
                            userMsg = userMsg + "</table>";
                        }

                    }
                             
                 }
            }
       }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        var communicationid = Convert.ToInt32(Request.Params["CommnicationID"]);
        var objBt1 = new BTprovider
            {
                Comments = txtDetail.Value,
                Reportedby = Reportedby.ToString(),
                Communicationid = communicationid
            };
        //int statusid = Convert.ToInt32(ddlStatus.SelectedValue);
        var objBTprovider = new BTprovider();
        string txtValue = "";
        string FileName = "";
        string FileType = "";
        string saveFile = "";
        var binData = new byte[] { };
        //** retrive files from multiple file uploader & used in insert into datatbase
        var oDt = new DataTable();
        int j = 0;
        fileUploadCount = Request.Files.Count;
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
                objBTprovider.Communicationid = communicationid;
                objBTprovider.FileContent = binData;
                objBTprovider.FileName = FileName;
                objBTprovider.FileType = FileType;
                objBTprovider.FileDetail = txtValue;
                objBTprovider.SaveFileDetail(objBTprovider);
            }


        }
        objBt1.UpdateCommunicationDetail(objBt1);
        ltfancycloseparentupload.Text = "<script type='text/javascript'>parent.$.fancybox.close(); parent.window.location.href=parent.window.location.href; </script>";
    }
   
    
}