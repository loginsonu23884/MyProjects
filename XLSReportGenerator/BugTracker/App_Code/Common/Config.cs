using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for Config
/// </summary>
public class Config
{
    public static int port = 587;
    public static string Host = "smtp.gmail.com";
    public static string AdminEmail = "";
    public static string FromEmail = "hr.sipl1@gmail.com";
    public static string FromName = "info";
    public static string UserName = "hr.sipl1@gmail.com";
    public static string Password = "hrsupport1212";

    // Return current domain url set in web.config file.
    public static string GetUrl(string jqueryJsJqueryMinJs)
    {
        var _url = ConfigurationManager.AppSettings["ApplicationURL"];
        if (_url.EndsWith("/") || _url.EndsWith(@"\"))
            return _url;
        return _url + "/";
    }
    public static string Todaydate()
    {
        return DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
    }
    public static string ApplyMenuActiveClass(string currentUrl, string MatchedPage)
    {
        return currentUrl.ToLower().Contains(MatchedPage.ToLower()) ? " class='active' " : " ";
    }

    public static string PPRLogo()
    {
        var OAppPath = ConfigurationManager.AppSettings["ProjectName"].Trim();
        OAppPath = ConfigurationManager.AppSettings["ApplicationURL"] + OAppPath;
        return "<img src='" + OAppPath + "/Global/Images/ppr-logo-email.png' alt='logo' />";
    }
    public static void SelectDropDownItemByText(DropDownList ddl, string text)
    {
        if (ddl.Items.Count > 0)
        {
            var index = 0;
            foreach (ListItem list in ddl.Items)
            {
                if (list.Text.Trim() == text.Trim())
                {
                    ddl.Items[index].Selected = true;

                    break;
                }
                index++;
            }
        }
    }
    public static string DecryptString(string encrString)
    {
        try
        {
            var b = Convert.FromBase64String(encrString);
            return System.Text.ASCIIEncoding.ASCII.GetString(b); ;
        }
        catch
        {
            return encrString;
        }
    }


    public static string EnryptString(string strEncrypted)
    {
        try
        {
            var b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            return Convert.ToBase64String(b);
        }
        catch
        {
            return strEncrypted;
        }
    }
}