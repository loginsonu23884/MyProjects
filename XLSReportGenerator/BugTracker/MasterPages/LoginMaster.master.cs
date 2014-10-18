using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_LoginMaster : System.Web.UI.MasterPage
{
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
