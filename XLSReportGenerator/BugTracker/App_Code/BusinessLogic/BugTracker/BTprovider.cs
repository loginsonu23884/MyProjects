using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


/// Summary description for BTprovider
/// </summary>
public class BTprovider
{
    #region Property
    public int CategoryID { get; set; }
    public int ReproducibilityID { get; set; }
    public int SeverityID { get; set; }
    public int PriorityID { get; set; }
    public int BrowserID { get; set; }
    public int OSVersionID { get; set; }
    public int AssignedtoID { get; set; }
    public string Description { get; set; }
    public string AdditionalInformation { get; set; }
    public string DefectDescription { get; set; }
    public string ExpectedResult { get; set; }
    public string ActualResult { get; set; }
    public int Userid { get; set; }
    public int Projectid { get; set; }
    public int Statusid { get; set; }
    public string IssueNo { get; set; }
    public string Summary { get; set; }
    public DateTime Duedate { get; set; }
    public int Assignedtouserid { get; set; }
    public int ReportIssueid { get; set; }
    public byte[] FileContent { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string FileDetail { get; set; }
    public string Comments { get; set; }
    public int Communicationid { get; set; }
    public string Reportedby { get; set; }
    public int Reportissueidresolveissue { get; set; }
    public string Statusidreolveissue { get; set; }
     public string ProjectName { get; set; }
     public DataTable IsEmail { get; set; }
     public string AssignmentNo { get; set; }
     public string ConfigureProjectID { get; set; }

	public BTprovider()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int SaveQAAssignProject(BTprovider objBTprovider)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        var param = new SqlParameter[2];
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            param[0] = new SqlParameter { Value = objBTprovider.Userid, ParameterName = "@UserId"};
            param[1] = new SqlParameter { Value = objBTprovider.ConfigureProjectID, ParameterName = "@ProjectId" };
            result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_insertQAAssignedProjects", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        return result;
    }
    // method for deleting the configured projects...
    public void DeleteConfiguredProjects()
    {
        string sql = "delete from ConfiguredProjects";
        SqlHelper.ExecuteScalar(SqlHelper.GetConnectionString("SR"), CommandType.Text,sql);
    }
    // method for deleting the configured projects...
    public void DeleteQAAssignedProjects(int userid)
    {
        string sql = "delete from QAAssignedProjects where UserId=" + userid;
        SqlHelper.ExecuteScalar(SqlHelper.GetConnectionString("SR"), CommandType.Text, sql);
    }
    public int SaveProjectConfigured(BTprovider objBTprovider)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        var param = new SqlParameter[1];
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            param[0] = new SqlParameter { Value = objBTprovider.ConfigureProjectID, ParameterName = "@ProjectId" };
           // param[1] = new SqlParameter { Value = objBTprovider.ProjectName, ParameterName = "@ProjectName" };
            result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_insertConfiguredProjects", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        return result;
    }
    public int SaveBTProvider(BTprovider objBTprovider)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        var param = new SqlParameter[18];
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
         
            param[0] = new SqlParameter { Value = objBTprovider.CategoryID, ParameterName = "@Categoryid" };
            param[1] = new SqlParameter { Value = objBTprovider.ReproducibilityID, ParameterName = "@Reproducibilityid" };
            param[2] = new SqlParameter { Value = objBTprovider.SeverityID, ParameterName = "@Severityid" };
            param[3] = new SqlParameter { Value = objBTprovider.PriorityID, ParameterName = "@Priorityid" };
            param[4] = new SqlParameter { Value = objBTprovider.BrowserID, ParameterName = "@Browserid" };
            param[5] = new SqlParameter { Value = objBTprovider.OSVersionID, ParameterName = "@OSVersionid" };
            param[6] = new SqlParameter { Value = objBTprovider.Assignedtouserid, ParameterName = "@Assignedtouserid" };
            
            param[7] = new SqlParameter { Value = objBTprovider.DefectDescription, ParameterName = "@DefectDescription" };
            param[8] = new SqlParameter { Value = objBTprovider.AdditionalInformation, ParameterName = "@AdditionalInformation" };
            param[9] = new SqlParameter { Value = objBTprovider.ExpectedResult, ParameterName = "@ExpectedResult" };
            param[10] = new SqlParameter { Value = objBTprovider.ActualResult, ParameterName = "@ActualResult" };
            param[11] = new SqlParameter { Value = objBTprovider.Duedate, ParameterName = "@Duedate" };
          
            param[12] = new SqlParameter { Value = objBTprovider.Statusid, ParameterName = "@Statusid" };
            param[13] = new SqlParameter { Value = objBTprovider.Projectid, ParameterName = "@Projectid" };
            param[14] = new SqlParameter { Value = objBTprovider.Summary, ParameterName = "@Summary" };
            param[15] = new SqlParameter { Value = objBTprovider.Userid, ParameterName = "@Userid" };
            param[16] = new SqlParameter { Value = objBTprovider.AssignmentNo, ParameterName = "@AssignmentNo" };
            param[17] = new SqlParameter { ParameterName = "@ReportIssueid", Value = 0, Direction = ParameterDirection.Output };
            param[17].Value = SqlHelper.ExecuteScalar(sqlTrans, "usp_InsertReportIssuesDetail", param);
            result = (int)param[17].Value;

            // To save ShipCompany CoInsured Detail...
            if (objBTprovider.IsEmail != null && objBTprovider.IsEmail.Rows.Count > 0)
            {
                var paramCoInsured = new SqlParameter[5];
                foreach (DataRow dr in objBTprovider.IsEmail.Rows)
                {
                    paramCoInsured[0] = new SqlParameter { ParameterName = "@ReportIssueId", Value = result };
                    paramCoInsured[1] = new SqlParameter { ParameterName = "@UserName", Value = dr["userName"].ToString() };
                    if (dr["chkvalue"].ToString() != "")
                    {
                       paramCoInsured[2] = new SqlParameter { ParameterName = "@Notification", Value = true };
                    }
                    else
                    {
                       paramCoInsured[2] = new SqlParameter { ParameterName = "@Notification", Value = false };
                    }
                    paramCoInsured[3] = new SqlParameter { ParameterName = "@Email", Value = dr["emailName"].ToString()};
                    paramCoInsured[4] = new SqlParameter { ParameterName = "@UserID", Value = dr["userId"].ToString()};
                    SqlHelper.ExecuteScalar(sqlTrans, CommandType.StoredProcedure, "usp_insertSendEmail", paramCoInsured);
                }

            }
            //result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_InsertReportIssuesDetail", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        //return Convert.ToInt32(param[13].Value);
        return result;
    }
    public int UpdateBTProvider(BTprovider objBTprovider)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        var param = new SqlParameter[18];
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();

            param[0] = new SqlParameter { Value = objBTprovider.CategoryID, ParameterName = "@Categoryid" };
            param[1] = new SqlParameter { Value = objBTprovider.ReproducibilityID, ParameterName = "@Reproducibilityid" };
            param[2] = new SqlParameter { Value = objBTprovider.SeverityID, ParameterName = "@Severityid" };
            param[3] = new SqlParameter { Value = objBTprovider.PriorityID, ParameterName = "@Priorityid" };
            param[4] = new SqlParameter { Value = objBTprovider.BrowserID, ParameterName = "@Browserid" };
            param[5] = new SqlParameter { Value = objBTprovider.OSVersionID, ParameterName = "@OSVersionid" };
            param[6] = new SqlParameter { Value = objBTprovider.Assignedtouserid, ParameterName = "@Assignedtouserid" };
            param[7] = new SqlParameter { Value = objBTprovider.Description, ParameterName = "@Description" };
            param[8] = new SqlParameter { Value = objBTprovider.DefectDescription, ParameterName = "@DefectDescription" };
            param[9] = new SqlParameter { Value = objBTprovider.AdditionalInformation, ParameterName = "@AdditionalInformation" };
            param[10] = new SqlParameter { Value = objBTprovider.ExpectedResult, ParameterName = "@ExpectedResult" };
            param[11] = new SqlParameter { Value = objBTprovider.ActualResult, ParameterName = "@ActualResult" };
            param[12] = new SqlParameter { Value = objBTprovider.Duedate, ParameterName = "@Duedate" };

            param[13] = new SqlParameter { Value = objBTprovider.Statusid, ParameterName = "@Statusid" };
            param[14] = new SqlParameter { Value = objBTprovider.Projectid, ParameterName = "@Projectid" };
            param[15] = new SqlParameter { Value = objBTprovider.Summary, ParameterName = "@Summary" };
            param[16] = new SqlParameter { Value = objBTprovider.Userid, ParameterName = "@Userid" };

            param[17] = new SqlParameter { Value = objBTprovider.ReportIssueid,ParameterName ="@Reportissueid" };
            result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_UpdateReportIssueDetail", param);
         
            


            //result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_InsertReportIssuesDetail", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        //return Convert.ToInt32(param[13].Value);
        return result;
    }

    public int UpdateReportissue(BTprovider objBTprovider)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        var param = new SqlParameter[2];
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            param[0] = new SqlParameter { Value = objBTprovider.Assignedtouserid, ParameterName = "@Assignedtouserid" };
             param[1] = new SqlParameter { Value = objBTprovider.ReportIssueid, ParameterName = "@Reportissueid" };
             result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_UpdateAssigned", param);




            //result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_InsertReportIssuesDetail", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        //return Convert.ToInt32(param[13].Value);
        return result;
    }

    public int UpdateStatus(BTprovider objBTprovider)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        var param = new SqlParameter[2];
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            param[0] = new SqlParameter { Value = objBTprovider.ReportIssueid, ParameterName = "@Reportissueid" };
            param[1] = new SqlParameter { Value = objBTprovider.Statusid, ParameterName = "@Statusid" };
            SqlHelper.ExecuteScalar(sqlTrans, "usp_UpdateStatus", param);
            result = 1;
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        //return Convert.ToInt32(param[13].Value);
        return result;
    }


    public int SaveAttachmentsdetails(BTprovider objBt)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null; 
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            var param = new SqlParameter[5];
            param[0] = new SqlParameter { Value = objBt.ReportIssueid, ParameterName = "@ReportIssueid" };
            param[1] = new SqlParameter { Value = objBt.FileContent, ParameterName = "@FileContent" };
            param[2] = new SqlParameter { Value = objBt.FileName, ParameterName = "@FileName" };
            param[3] = new SqlParameter { Value = objBt.FileType, ParameterName = "@FileType" };
            param[4] = new SqlParameter { Value = objBt.FileDetail, ParameterName = "@FileDetail" };
            result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_InsertAttachmentFilesForBugTracker", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        return result;
    }
    public int UpdateCommunicationDetail(BTprovider objBt1)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            var param = new SqlParameter[3];

            param[0] = new SqlParameter { Value = objBt1.Comments, ParameterName = "@Comments" };
            param[1] = new SqlParameter { Value = objBt1.Reportedby, ParameterName = "@Reportedby" };
            param[2] = new SqlParameter { Value = objBt1.Communicationid, ParameterName = "@Communicationid" };
            result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_UpdateCommunicationDetail", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        return result;
    }
    /// <summary>
    /// method to get issue list
    /// </summary>
    /// <param name="Userid"></param>
    /// <param name="searchFilter"></param>
    /// <param name="roleTypeID"></param>
    /// <returns></returns>
    public DataTable GetReportIssueDetail(string Userid,string searchFilter, int roleTypeID)
    {
        string sql = "";
        if(roleTypeID == 6)
        {
           sql = "Select *,(select isnull(count(id),0) from [Bugtracker].[dbo].SaveAttchmentsDetail where ReportIssueid in (select [Bugtracker].[dbo].Vw_ReportIssueList.ReportIssueid from [Bugtracker].[dbo].Vw_ReportIssueList)) as AttachmentCount from [Bugtracker].[dbo].Vw_ReportIssueList where Userid=" + Userid + " and IssueNo<>'' ";
        }
        if (roleTypeID == 7)
        {
            sql = "Select *,(select isnull(count(id),0) from [Bugtracker].[dbo].SaveAttchmentsDetail where ReportIssueid in (select [Bugtracker].[dbo].Vw_ReportIssueList.ReportIssueid from [Bugtracker].[dbo].Vw_ReportIssueList)) as AttachmentCount from [Bugtracker].[dbo].Vw_ReportIssueList where MailUserId=" + Userid + " and IssueNo<>'' ";
        }
        if (roleTypeID == 0)
        {
           sql = "Select *,(select isnull(count(id),0) from [Bugtracker].[dbo].SaveAttchmentsDetail where ReportIssueid in (select [Bugtracker].[dbo].Vw_ReportIssueList.ReportIssueid from [Bugtracker].[dbo].Vw_ReportIssueList)) as AttachmentCount from [Bugtracker].[dbo].Vw_ReportIssueList where IssueNo<>'' ";
        }
        //and ( Convert(datetime,convert(varchar,PostDate,101)) between '" + fromdate + "' and '" + enddate + "' ) ";
        if (searchFilter != "")
        {
            sql = sql + " and " + searchFilter;
        }
        sql = sql + " order by PostDate Desc";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    /// <summary>
    /// method to get issue status
    /// </summary>
    /// <param name="Userid"></param>
    /// <param name="roleTypeID"></param>
    /// <returns></returns>
    public DataTable GetIssueStatus(int Userid,int roleTypeID)
    {
        string sql = "";
        if (roleTypeID == 6)
        {
            sql = "Select * from [Bugtracker].[dbo].Vw_ReportIssueList where Userid=" + Userid + " and Projectid in (select ProjectId from QAAssignedProjects where UserId=" + Userid + ") and IssueNo<>'' ";
        }
        if (roleTypeID == 7)
        {
            sql = "Select * from [Bugtracker].[dbo].Vw_ReportIssueList where MailUserId=" + Userid + " and IssueNo<>'' ";
        }
        if (roleTypeID == 0)
        {
            sql = "Select * from [Bugtracker].[dbo].Vw_ReportIssueList where IssueNo<>'' ";
        }
         return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetDetail(String IssueNo)
    {
        string sql = "select * from Vw_ReportIssueList where IssueNo = '" + IssueNo + "'";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetReportIssueid()
    {
        string sql = "Select ReportIssueid from ReportIssues order by ReportIssueid desc ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetSaveAttchmentsDetail(int rptissueid)
    {
        string sql = "Select ReportIssueid from ReportIssues  where ReportIssueid= " + rptissueid + "  order by ReportIssueid desc ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetCommuncationid()
    {
        string sql = "Select CommunicationID from CommunicationDetail order by CommunicationID desc";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetCommuncationDetail(int reportissueid)
    {
        string sql = "Select * from Vw_CommunicationDetail where ReportIssueid=" + reportissueid + " order by CommunicationID asc";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetCommuncationAttachments(int ID)
    {
        string sql = "Select * from CommunicationAttachments where ID=" + ID + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetSaveAttachmentsDetailOnBasisOfid(int ID)
    {
        string sql = "Select * from SaveAttchmentsDetail where ID=" + ID + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }

    public DataTable GetAttachmentDetailOnBasisOfReportIssueId(int reportissueid)
    {
        string sql = "Select * from SaveAttchmentsDetail where ReportIssueid=" + reportissueid + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetQueryExecute(string sqlQuery)
    {

        return SqlHelper.FillDataTable(sqlQuery, "SR");
    }
   

    public DataTable GetCommentdetail(int Communicationid)
    {
        string sql = "Select * from Vw_CommunicationDetail where CommunicationID=" + Communicationid + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }

    public DataTable Getattachmentdetail(int reportIssueId)
    {
        string sql = "Select filename,ID from SaveAttchmentsDetail where ReportIssueID=" + reportIssueId + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetAssignedUser(int reportIssueId)
    {
        string sql = "Select * from tbl_SendEmail where ReportIssueId=" + reportIssueId + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    public DataTable GetIssueDetail(int reportissueid)
    {
        string sql = "Select * from Vw_ReportIssueList where ReportIssueid=" + reportissueid + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }

    public DataTable GetAssigned(int Assignedtouserid)
    {
        string sql = "Select * from Vw_ReportIssueList where Assignedtouserid=" + Assignedtouserid + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }

   public DataTable GetTopTenComments()
   {
       string sql = "SELECT TOP 10 * FROM Vw_CommunicationDetail ORDER BY Date DESC";
       return SqlHelper.FillDataTable(sql, "SR");
   }

   public DataTable GetAssignedIssuesToMe(int Userid)
    {
        string sql = "select * from Vw_ReportIssueList where Statusid !=" + 2 + " and Assignedtouserid=" + Userid + " ";
        return SqlHelper.FillDataTable(sql, "SR");
    }
   public DataTable GetEmailDetails(int Userid)
   {
       string sql = "select * from SalesDetail where UserId=" + Userid;
       return SqlHelper.FillDataTable(sql, "BT");
   }

   public DataTable GetReportIssueDetails(int ReportIssueId)
   {
       string sql = "select * from Vw_ReportIssueList where ReportIssueid=" + ReportIssueId;
       return SqlHelper.FillDataTable(sql, "BT");
   }
   public DataTable GetEmails(string name)
   {
       string sql = "select email from Vw_ReportIssueList where Name= '"+ name +"' ";
       return SqlHelper.FillDataTable(sql, "BT");
   }
   public DataTable GetAssignmentNo(int projectId)
   {
       string sql = "select AssignmentNo from project where id=" + projectId;
       return SqlHelper.FillDataTable(sql, "TTMS");
   }
    
    public int SaveCommunicationDetails(BTprovider objBt)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            var param = new SqlParameter[4];
            param[0] = new SqlParameter { Value = objBt.ReportIssueid, ParameterName = "@ReportIssueid" };
            param[1] = new SqlParameter { Value = objBt.Userid, ParameterName = "@ReportedBy" };
            param[2] = new SqlParameter { Value = objBt.Comments, ParameterName = "@Comments" };
            param[3] = new SqlParameter { Value = objBt.Statusid, ParameterName = "@Statusid" };
            result = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTrans, "usp_InsertCommunicationDetails", param));
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        return result;
    }
    public int SaveFileDetail(BTprovider objBt)
    {
        int result = 0;
        var conn = new SqlConnection(SqlHelper.GetConnectionString("SR"));
        SqlTransaction sqlTrans = null;
        try
        {
            //Open the connection
            conn.Open();
            sqlTrans = conn.BeginTransaction();
            var param = new SqlParameter[5];
            param[0] = new SqlParameter { Value = objBt.Communicationid, ParameterName = "@CommunicationID" };
            param[1] = new SqlParameter { Value = objBt.FileContent, ParameterName = "@FileContent" };
            param[2] = new SqlParameter { Value = objBt.FileName, ParameterName = "@FileName" };
            param[3] = new SqlParameter { Value = objBt.FileType, ParameterName = "@FileType" };
            param[4] = new SqlParameter { Value = objBt.FileDetail, ParameterName = "@FileDetail" };
            result = SqlHelper.ExecuteNonQuery(sqlTrans, "usp_InsertFileInformation", param);
            sqlTrans.Commit();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }//Error...
        catch (SqlException sqlEx)
        {
            if (sqlTrans.Connection.State == ConnectionState.Open)
            {
                sqlTrans.Rollback();
                conn.Close();
            }
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        //Return the fetched record result
        return result;
    }

    #endregion
   
}