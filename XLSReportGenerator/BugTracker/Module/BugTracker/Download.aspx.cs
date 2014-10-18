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


public partial class Module_BugTracker_Download : System.Web.UI.Page
{
    private int flag;
    protected void Page_Load(object sender, EventArgs e)
    {
        int flag = Convert.ToInt32(Request.QueryString["flag"]);
        if (flag == 0)
        {

            //var rptissueid = Convert.ToInt16(Request.Params["reportissueid"]);
            var ID = Convert.ToInt16(Request.Params["ID"]);
            BTprovider objBT1 = new BTprovider();
            var dt = new DataTable();
            dt = objBT1.GetSaveAttachmentsDetailOnBasisOfid(ID);
            if (dt != null)
            {
                if (dt.Rows.Count >= 1)
                {
                    download(dt);
                }
            }
        }
        else if (flag == 1)
        {

                var ID = Convert.ToInt16(Request.Params["ID"]);
                BTprovider objBT1 = new BTprovider();
                var dt = new DataTable();
                dt = objBT1.GetCommuncationAttachments(ID);
                if (dt != null)
                {
                    if (dt.Rows.Count >= 1)
                    {
                        download(dt);
                    }
                }
            
        }
    }
    public void download(DataTable objDtable)
    {
        Byte[] bytes = (Byte[])objDtable.Rows[0]["FileContent"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = (string)objDtable.Rows[0]["FileType"];
        Response.AddHeader("content-disposition", "attachment;filename=" + objDtable.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}