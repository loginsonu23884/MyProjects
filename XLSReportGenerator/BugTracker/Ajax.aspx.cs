using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Ajax : System.Web.UI.Page
{   
    //Give path for project
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    public int RoleTypeID = 0;
     
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Request.QueryString["Method"] == "DeleteAssignDeveloper")
        {
            DeleteNatureOfInterest();
        }//end if
        else if (Request.QueryString["Method"] == "GetAssignmentNo")
        {
            int ProjectId = Convert.ToInt32(Request.QueryString["projectId"]);
            var objBT = new BTprovider();
            var dt = objBT.GetAssignmentNo(ProjectId);
            var str = string.Empty;
            str = dt.Rows[0]["AssignmentNo"].ToString();
            Response.Clear();
            Response.ContentType = "text/json";
            Response.Write(str);
            Response.End();
            //End
        }//end if
        else if (Request.QueryString["Method"] == "addAssignDeveloper")
        {
            AddNatureOfInterestName();
        }//end if
        // To load mail notification table
        else if (Request.QueryString["Method"] == "loadMailNotification")
        {
           // Calling method
            var str = String.Empty;
            var dtItems = new DataTable();
            if (Session["DtIsEmail"] != null)
            {
                dtItems = (DataTable)Session["DtIsEmail"];
            }
            if (dtItems != null && dtItems.Rows.Count > 0)
            {
                str = "[";
                foreach (DataRow dr in dtItems.Rows)
                {
                    str += "{\"User\":\"" + dr["userName"] + "\",\"Notifications\":\"" + dr["chkvalue"] + "\",\"Email\":\"" +
                           dr["emailName"] + "\"},";
                }
                str = str.Substring(0, str.Length - 1);
                str += "]";
            }
            Response.Clear();
            Response.ContentType = "text/json";
            Response.Write(str);
            Response.End();
            //End
        }

        // To request for view posting list
        if (Request.QueryString["Type"] == "VeiwIssue")
        {
            string strFilter = "";
            // passing filter value
            strFilter = BuildViewIssueFilter(Convert.ToString(Request.Params["Query"]), Convert.ToBoolean(Request.Params["Summary"]), Convert.ToBoolean(Request.Params["Description"]), Convert.ToString(Request.Params["Assignedtouserid"]), Convert.ToString(Request.Params["Priority"]), Convert.ToString(Request.Params["Status"]), Convert.ToString(Request.Params["Project"]), Convert.ToString(Request.Params["Category"]), Convert.ToBoolean(Request.Params["IsDate"]), Convert.ToString(Request.Params["FromDate"]), Convert.ToString(Request.Params["EndDate"]));
            // creating class object
            var objBt = new BTprovider();
            // calling method for view posting list detail
            //To check session value
           
            if (Session["RoleTypeID"] != null)
            {
                RoleTypeID = Convert.ToInt32(Session["RoleTypeID"]);
            }
            PrepareVeiwIssueList(objBt.GetReportIssueDetail(Request.Params["Userid"], strFilter,RoleTypeID));
        }// end if

        if (Request.QueryString["Type"] == "Veiwpopup")
        {
            string userMsg = "";
            DataTable strreportissue = new DataTable();
            strreportissue = BuildFileDetailPopUp(Convert.ToInt32(Request.Params["ReportIssueid"]));

            userMsg = "" +
            "<table class='summeryTable' cellpadding='1' cellspacing='1' border='0' style='background: #ccc; width:100% '>";
            for (int i = 0; i < strreportissue.Rows.Count; i++)
            {
                userMsg = userMsg + "<tr>" +
                 "<th style='background: #FAFE00;  padding: 4px 8px'>File Name</th>" +
                 "<td style='background: #FAFE00;  padding: 4px 8px'>" + strreportissue.Rows[i]["FileName"] + "</td>" +
                 "</tr>" +
                 "<tr>" +
                 "<th style='background: #FAFE00;  padding: 4px 8px'>File Type</th>" +
                 "<td style='background: #FAFE00;  padding: 4px 8px'>" + strreportissue.Rows[i]["FileType"] + "</td>" +
                 "</tr>" +
                 "<tr>" +
                 "<th style='background: #FAFE00;  padding: 4px 8px'></th>" +
                 "<td style='background: #FAFE00;  padding: 4px 8px'><a  href='/SalesReporting/Module/BugTracker/Download.aspx?reportissueid=" + Convert.ToInt32(Request.Params["ReportIssueid"]) + "'>Preview</a>;</td>" +
                 "</tr>";

            }
            userMsg = userMsg + "</table>";

            Response.Clear();
            ////Response.ContentType = "text/xml";
            Response.Write(userMsg);
            Response.End();
        }
    
        if (Request.QueryString["Type"] == "AttachmentandComment")
        {
            int communicationid = Convert.ToInt32(Request.Params["CommnicationID"]);
          DeleteAttachmentAndComments(communicationid);
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write("<success>1</success>");
            Response.End();
        }
        if (Request.QueryString["Type"] == "Attachment")
        {
           DeleteAttachment(Convert.ToInt32(Request.Params["ID"]));
           Response.Clear();
           Response.ContentType = "text/xml";
           Response.Write("<success>1</success>");
           Response.End();
        }

       
    }

    public string DeleteNatureOfInterest()
    {

        var dtItems = (DataTable)Session["DtIsEmail"];
        var str = "";

        if (dtItems != null && dtItems.Rows.Count > 0)
        {
            dtItems.Rows.RemoveAt(Convert.ToInt32(Request.Params["rowNumber"]));
            dtItems.AcceptChanges();
            Session.Add("DtIsEmail", dtItems);
            str = "[";

            foreach (DataRow dr in dtItems.Rows)
            {
                str += "{\"User\":\"" + dr["userName"].ToString()+ "\",\"Notifications\":\"" + dr["chkvalue"] + "\",\"Email\":\"" +
                       dr["emailName"].ToString() + "\"},";
            }
            str = str.Substring(0, str.Length - 1);
            str += "]";
            

        }
        Response.Clear();
        Response.ContentType = "text/json";
        Response.Write(str);
        Response.End();
        return str;
    }

    public string AddNatureOfInterestName()
    {
        var dtItems = (DataTable) Session["DtIsEmail"];
        // Create a DataColumn instances
        var myrow = dtItems.NewRow();
        myrow["userId"] = Request.Params["userId"];
        myrow["userName"] =  Request.Params["userName"];
        if (Request.Params["chkvalue"] == "true")
        {
            myrow["chkvalue"] = "<img src='" + oAppPath + "/images/complete-icon.png'/>";
            myrow["emailName"] = Request.Params["emailName"];
        }
        else
        {
            myrow["chkvalue"] = "";
            myrow["emailName"] = "";
        }
        //myrow["chkvalue"] =  Request.Params["chkvalue"];
       
        // Add the row into the table
        dtItems.Rows.Add(myrow);
        dtItems.AcceptChanges();
        Session.Add("DtIsEmail", dtItems);
        var str = "";

        if (dtItems != null && dtItems.Rows.Count > 0)
        {
            str = "[";

            foreach (DataRow dr in dtItems.Rows)
            {
                str += "{\"User\":\"" + dr["userName"] + "\",\"Notifications\":\"" + dr["chkvalue"] + "\",\"Email\":\"" +
                       dr["emailName"] + "\"},";
            }
            str = str.Substring(0, str.Length - 1);
            str += "]";
        }
        Response.Clear();
        Response.ContentType = "text/json";
        Response.Write(str);
        Response.End();
        return str;
    

}
    public void DeleteAttachment(int communicationid)
    {
       
        string sql = "Delete  from CommunicationAttachments where ID= " + communicationid + " ";
        SqlHelper.ExecuteScalar(SqlHelper.GetConnectionString("SR"), CommandType.Text, sql);
    }

    public void DeleteAttachmentAndComments(int communicationid)
    {
     
        string sql = "Delete  from CommunicationAttachments where Communicationid= " + communicationid + " ";
        SqlHelper.ExecuteScalar(SqlHelper.GetConnectionString("SR"), CommandType.Text, sql);
        sql = "Delete  From CommunicationDetail where CommunicationID= " + communicationid + " ";
        SqlHelper.ExecuteScalar(SqlHelper.GetConnectionString("SR"), CommandType.Text, sql);

    }

    public DataTable BuildFileDetailPopUp(int reportIssueid)
    {

        string sql = "Select ID,ReportIssueid,FileContent,FileName,FileType,FileDetail from SaveAttchmentsDetail where ReportIssueid= " + reportIssueid + "";
        return SqlHelper.FillDataTable(sql, "SR");
       
    }
    //Function for title filter
   
    public string BuildViewIssueFilter(string Query,Boolean Summary, Boolean Description,string assignedtouserid, string priorityid, string statusid,string projectid,string categoryid, bool isDate, string fromdate, string enddate)

    {
        string strFilter = "";
        // To check projecttype
         if (Query != "") 
            {
                if (Summary == true && Description == true) 
                {
                    strFilter = strFilter + " Summary like '%" + Query + "%' and Description like '%" + Query + "%' ";
                }
                else 
                {
                    if (Summary == true)
                    {
                        strFilter = strFilter + " Summary like '%" + Query + "%' ";
                    }
                    else
                    {

                        if (Description == true) 
                        {
                            strFilter = strFilter + " Description like '%" + Query + "%'  ";
                        }

                    }
                }
            }
        if (isDate)
        {
            if (fromdate != "" && enddate != "")
            {
                if (strFilter == string.Empty)
                {
                    strFilter = strFilter + "  (Convert(datetime,convert(varchar,PostDate,101)) between '" + fromdate + "' and '" + enddate + "' ) ";
                }
                else
                {
                    strFilter = strFilter + " and (Convert(datetime,convert(varchar,PostDate,101)) between '" + fromdate + "' and '" + enddate + "') ";
                }
            }
        }
        if (priorityid != "0" && priorityid != "")
        {
            if (strFilter == string.Empty)
            {
                strFilter = strFilter + " Priorityid in (" + priorityid + ") ";
            }
            else {
                strFilter = strFilter + " and " + " Priorityid in (" + priorityid + ") ";
            }
        }

        if (assignedtouserid != "0" && assignedtouserid != "")
        {
            if (strFilter == string.Empty)
            {
                strFilter = strFilter + " MailUserId in (" + assignedtouserid + ")";
            }
            else
            {
                strFilter = strFilter + " and " + "MailUserId in (" + assignedtouserid + ")";
            }
        }
        if (statusid != "0" && statusid != "")
        {

            if (strFilter == string.Empty)
            {
                strFilter = strFilter + "Statusid in (" + statusid + ")";
            }
            else
            {
                strFilter = strFilter + " and " + "Statusid in (" + statusid + ")";
            }
        }
        if (projectid != "0" && projectid != "")
        {

            if (strFilter == string.Empty)
            {
                strFilter = strFilter + "Projectid in (" + projectid + ")";
            }
            else
            {
                strFilter = strFilter + " and " + "Projectid in (" + projectid + ")";
            }
        }
        if (categoryid != "0" && categoryid != "")
        {

            if (strFilter == string.Empty)
            {
                strFilter = strFilter + "Categoryid in (" + categoryid + ")";
            }
            else
            {
                strFilter = strFilter + " and " + "Categoryid in (" + categoryid + ")";
            }
        }
       return strFilter;

    }
    
// function for searchable list filter
   
    public void PrepareVeiwIssueList(DataTable dt)
    {
        // Define variable
        string xmlString = "";
        int page = Convert.ToInt32(Request.Params["page"]);
        int rows = Convert.ToInt32(Request.Params["rows"]);
        int pageIndex = Convert.ToInt32(page) - 1;
        int pageSize = rows;
        //check if datatable has rows
        if (dt.Rows.Count > 0)
        {
            var dv = new DataView(dt);
            dv.Sort = " " + Request.Params["sidx"].ToString() + " " + Request.Params["sord"].ToString() + " ";
            IEnumerable<DataRow> veiwissuelist = dv.ToTable().AsEnumerable().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((float)dt.Rows.Count / (float)pageSize);
            if (veiwissuelist.Count() > 0)
            {
                xmlString += "<rows>";
                xmlString += "<page>" + page + "</page>";
                xmlString += "<total>" + totalPages + "</total>";
                xmlString += "<records>" + veiwissuelist.Count() + "</records>";
               
                // define variable
                string strContact = "";
                var flag = 0;

                foreach (var dr in veiwissuelist)
                {
                    
                  
                    // creating column of jqgrid
                    xmlString += "<row id='" + dr["ReportIssueid"].ToString() + "'>";
                    xmlString += "<cell><![CDATA[<a  href='#' onclick='javascript:funComments(\"" + dr["ReportIssueid"].ToString() + "\");' </a>]]>" + dr["IssueNo"].ToString() + "</cell>";
                    xmlString += "<cell><![CDATA[" + dr["Summary"].ToString() + " <br />" + "]]></cell>";
                    xmlString += "<cell><![CDATA[ <a onclick='javascript:ViewAssignedUser(\"" + dr["ReportIssueID"].ToString() + "\");'  title='View assign user' style='cursor:pointer;'><img src='" + oAppPath + "/images/viewcontent.png'  /> </a>]]></cell>";
                    xmlString += "<cell><![CDATA[" + dr["username"].ToString() + "]]></cell>";
                    xmlString += "<cell><![CDATA[" + dr["Priority"].ToString() + "]]></cell>";
                    xmlString += "<cell><![CDATA[" + dr["StatusName"].ToString() + "]]></cell>";
                    xmlString += "<cell><![CDATA[" + dr["PostDate"].ToString() + "]]></cell>";
                    xmlString += "<cell><![CDATA[" + dr["Updated"].ToString() + "]]></cell>";
                    xmlString += "<cell><![CDATA[" + dr["Duedate"].ToString() + "]]></cell>";
                    if (Convert.ToInt32(dr["AttachmentCount"]) > 0)
                    {
                        xmlString += "<cell><![CDATA[ <a onclick='javascript:OpenAttachment(\"" + dr["ReportIssueID"].ToString() + "\");'  style='cursor:pointer;'><img src='" + oAppPath + "/images/icon.png'  /> </a>]]></cell>";
                    }
                    else
                    {
                        xmlString += "<cell><![CDATA[]]></cell>";
                    }
                    //xmlString += "<cell><![CDATA[" + dr["Comments"].ToString() + "]]></cell>";
                   

                    // creating contact column

                    xmlString += "</row>";

                }
                xmlString += "</rows>";
            }
        }
        else
        {
            xmlString += "<rows>";
            xmlString += "<page>0</page>";
            xmlString += "<total>0</total>";
            xmlString += "<records>0</records></rows>";
        }

        Response.Clear();
        Response.ContentType = "text/xml";
        Response.Write(xmlString);
        Response.End();
    }// end of function

   
}






