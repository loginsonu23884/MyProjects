using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public string OAppPath = ConfigurationManager.AppSettings["ProjectName"].Trim();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
