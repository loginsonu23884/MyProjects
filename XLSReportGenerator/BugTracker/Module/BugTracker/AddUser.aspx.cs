using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_BugTracker_AddUser : System.Web.UI.Page
{ 
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            var objBT = new BT_DropDownHandler();
            objBT.BindAssignedto(ddlassignedto);
            if (Session["DtIsEmail"] == null)
            {
                CreateDataTable();
            }

        }
        
    }
    public void CreateDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("userId");
        dt.Columns.Add("userName");
        dt.Columns.Add("chkvalue");
        dt.Columns.Add("emailName");
        Session.Add("DtIsEmail", dt);
    }
}