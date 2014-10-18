using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_BT_MasterPage : System.Web.UI.MasterPage
{
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();

    public string Name = "";


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count == 0)
        {
            Response.Redirect(oAppPath + "/Loginpage.aspx");
        }
        int RoleTypeID = 0;
        if (Session["RoleTypeID"] != null)
        {
            RoleTypeID = Convert.ToInt16(Session["RoleTypeID"]);

        }
        if (RoleTypeID == 0)
            MenuAdmin.Visible = true;
        if (RoleTypeID == 6)
            MenuQA.Visible = true;
        if (RoleTypeID == 7)
            MenuDeveloper.Visible = true;
         Name = (string)(Session["SalesPersonName"]);
    }
}
