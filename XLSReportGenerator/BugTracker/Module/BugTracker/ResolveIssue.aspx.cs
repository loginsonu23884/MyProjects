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


public partial class Module_BugTracker_ResolveIssue : System.Web.UI.Page
{

 
    public int FileUploadCount;
    public int UserId;
    public int Reportissueid;
    public int Statusid;
   
    public string OAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    protected void Page_Load(object sender, EventArgs e)
    {

        Reportissueid = Convert.ToInt32(Request.QueryString["Reportissueid"]);
       
    }

    public void btnresolve_Click(object sender, EventArgs e)
    {

        int UserId = (int)(Session["UserId"]);
        int Communcationid = 0;
        var objBt = new BTprovider
            {
                ReportIssueid = Reportissueid,
                Userid = UserId,
                Statusid = 2,
                Comments = txtDetail.Value
            };
      
        Communcationid = objBt.SaveCommunicationDetails(objBt);
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
            var postedFile = Request.Files[i];
            if (postedFile.ContentLength > 0)
            {
                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileType = postedFile.ContentType;
                var b = new BinaryReader(postedFile.InputStream);
                binData = b.ReadBytes(postedFile.ContentLength);
               
            }

            objBt.Communicationid = Communcationid;
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

        ltfancycloseparentupload.Text = "<script type='text/javascript'>parent.$.fancybox.close(); parent.window.location.href=parent.window.location.href; </script>";

    }

   
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ltfancycloseparentupload.Text = "<script type='text/javascript'>parent.$.fancybox.close();</script>"; 

    }
}
