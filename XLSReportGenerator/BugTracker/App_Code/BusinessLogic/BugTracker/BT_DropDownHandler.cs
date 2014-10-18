using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
/// Summary description for BT_DropDownHandler
/// </summary>
public class BT_DropDownHandler
{
	public BT_DropDownHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public  DataTable BindCategory()
    {
       return SqlHelper.FillDataTable("select * from Category","SR");
    }
    public void BindReproducibilty(DropDownList ddlreproducibilty)
    {
        string sql = "Select 0 as Reproducibilityid ,' Select One' as Reproducibility union all Select Reproducibilityid,Reproducibility from Reproducibility";
        SqlHelper.FillCombo(ddlreproducibilty, "Reproducibility", "Reproducibilityid", "SR", sql);
    }
    //Method for binding the Technology
    public void BindSeverity(DropDownList ddlseverity)
    {
        string sql = " Select 0 as Severityid ,' Select One' as Severity union all Select Severityid,Severity from Severity";
        SqlHelper.FillCombo(ddlseverity, "Severity", "Severityid", "SR", sql);
    }
    //Method for binding the Profile
    public void BindPriority(DropDownList ddlpriority)
    {
        string sql = "Select 0 as Priorityid ,' Select One' as Priority union all Select Priorityid,Priority from Priority";
        SqlHelper.FillCombo(ddlpriority, "Priority", "Priorityid", "SR", sql);
    }
    //Method for binding the Priority
    public void BindBrowser(DropDownList ddlbrowser)
    {
        string sql = "Select 0 as Browserid ,' Select One' as Browser union all Select Browserid,Browser from Browser";
        SqlHelper.FillCombo(ddlbrowser, "Browser", "Browserid", "SR", sql);
    }
    //Method for binding the status
    public void BindOS(DropDownList ddlOSVersion)
    {
        string sql = "Select 0 as OSVersionid ,' Select One' as OperatingSystem union all Select OSVersionid,OperatingSystem from OSVersion";
        SqlHelper.FillCombo(ddlOSVersion, "OperatingSystem", "OSVersionid", "SR", sql);
    }
    public void BindAssignedto(DropDownList ddlassignedto)
    {
        string sql = "Select 0 as id ,' Select One' as Name union all Select  id,first_name +' '+ last_name as Name  from at_user order by Name asc";
        SqlHelper.FillCombo(ddlassignedto, "Name", "id", "TTMS", sql);
    }
    public void BindStatusIssue(DropDownList ddlstatus)
    {
        string sql = "Select 0 as Statusid ,'Select one' as StatusName union all Select Statusid,StatusName from Issue_Status";
        SqlHelper.FillCombo(ddlstatus, "StatusName", "Statusid", "SR", sql);
    }
    public void BindProject(DropDownList ddlproject)
    {
        string sql = "select 0 as id ,' Select One' as name union all Select id,name from project";
        SqlHelper.FillCombo(ddlproject, "name", "id", "TTMS", sql);
    }
    public void BindQAAssignProject(DropDownList ddlproject, int userId)
    {
        string sql = " exec usp_GetQAAssignProject " + userId;
        SqlHelper.FillCombo(ddlproject, "name", "ProjectId", "SR", sql);
    }
    public DataTable BindUSerQAAssignProject (int userId)
    {
        string sql = " exec usp_GetQAAssignProject " + userId;
        return SqlHelper.FillDataTable(sql,"SR");
    }
    public void BindQA(DropDownList ddlQA)
    {
        string sql = "select 0 as UserId ,' Select One' as Name union all Select distinct UserId,Name from SalesDetail where RoleTypeId=6 order by Name asc ";
        
        SqlHelper.FillCombo(ddlQA, "Name", "UserId", "SR", sql);
    }
    // Method to bind checkbox list
    public DataTable BindProjectName()
    {
        string sql = " Select ID,Name from project order by Name asc";
        return SqlHelper.FillDataTable(sql, "TTMS");
    }
    // Method to bind checkbox list
    public DataTable ChkConfiguredProject()
    {
        string sql = " Select ProjectId from ConfiguredProjects";
        return SqlHelper.FillDataTable(sql, "SR");
    }
    // Method to bind checkbox list
    public DataTable ChkQAAssignProject(int UserId)
    {
        string sql = " Select ProjectId from QAAssignedProjects where UserId=" + UserId;
        return SqlHelper.FillDataTable(sql, "SR");
    }
    // Method to bind checkbox list
    public DataTable BindConfiguredProject()
    {
        string sql = "exec usp_GetConfiguredProject";
        return SqlHelper.FillDataTable(sql, "SR");
        //DataTable fillDataTable = SqlHelper.FillDataTable(sql, "SR");

        //fillDataTable.Rows.Add()

        //return fillDataTable;
    }
    public void BindStatuslistbox(ListBox lststatus)
    {
        string sql = "select 0 as Statusid , 'All' as StatusName union all Select Statusid,StatusName from Issue_Status";
        SqlHelper.FillListbox(lststatus, "StatusName", "Statusid", "SR", sql);
    }
    public void BindPrioritylistbox(ListBox lstpriority)
    {
        string sql = "select 0 as Priorityid ,'All' as Priority union all Select Priorityid,Priority from Priority";
        SqlHelper.FillListbox(lstpriority, "Priority", "Priorityid", "SR", sql);
    }
    public void BindAssignedtolistbox(ListBox lstassignedto)
    {
        string sql = "Select 0 as id ,'All' as Name union all Select  id,first_name +' '+ last_name as Name  from at_user  order by id asc ";
        SqlHelper.FillListbox(lstassignedto, "Name", "id", "TTMS", sql);
    }
    public void BindProjectlistbox(ListBox lstproject)
    {
        string sql = "select 0 as id ,'All' as name union all Select id,name from project";
        SqlHelper.FillListbox(lstproject, "name", "id", "TTMS", sql);
    }
}