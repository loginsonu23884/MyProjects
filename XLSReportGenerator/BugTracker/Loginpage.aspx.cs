using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class Loginpage : System.Web.UI.Page
{   
    // Give path for project
    public string oAppPath = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString().Trim();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //for showing messages
        lblMessage.Visible = false;
        lblMessage.Text = "";
        // To remove all session value
        Session.RemoveAll();
        
     }
    string encryptPassword = "";
    char fetchChar;
    string strAscii;
    int intCounterForChar = 0, intCounterForAscii = 0;
    int strAsciiLength = 0;
    byte[] arrInput1;
    public byte[] Convert2ByteArray(string strInput)
    {
        int intCounter;
        char[] arrChar;
        arrChar = strInput.ToCharArray();
        byte[] arrByte = new byte[arrChar.Length];
        for (intCounter = 0; intCounter <= arrChar.Length - 1; intCounter++)
        {
            arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);
        }
        return arrByte;
    }
    public void ConvertNumberIntoSpecialSymbol()
    {
        if (Convert.ToInt32(fetchChar) == 48)
            encryptPassword = encryptPassword + "!";
        if (Convert.ToInt32(fetchChar) == 49)
            encryptPassword = encryptPassword + "#";
        if (Convert.ToInt32(fetchChar) == 50)
            encryptPassword = encryptPassword + "$";
        if (Convert.ToInt32(fetchChar) == 51)
            encryptPassword = encryptPassword + "^";
        if (Convert.ToInt32(fetchChar) == 52)
            encryptPassword = encryptPassword + "&";
        if (Convert.ToInt32(fetchChar) == 53)
            encryptPassword = encryptPassword + "*";
        if (Convert.ToInt32(fetchChar) == 54)
            encryptPassword = encryptPassword + "-";
        if (Convert.ToInt32(fetchChar) == 55)
            encryptPassword = encryptPassword + "+";
        if (Convert.ToInt32(fetchChar) == 56)
            encryptPassword = encryptPassword + "~";
        if (Convert.ToInt32(fetchChar) == 57)
            encryptPassword = encryptPassword + "=";

    }
    public void ConvertAsciiIntoSpecialAlpha()
    {
        while (strAsciiLength > 0)
        {
            char fetchCharForAscii = Convert.ToChar(arrInput1.GetValue(intCounterForAscii));
            string strAscii1 = Convert.ToInt32(fetchCharForAscii).ToString();
            if (Convert.ToInt32(strAscii1) == 48)
                encryptPassword = encryptPassword + "a";
            if (Convert.ToInt32(strAscii1) == 49)
                encryptPassword = encryptPassword + "b";
            if (Convert.ToInt32(strAscii1) == 50)
                encryptPassword = encryptPassword + "c";
            if (Convert.ToInt32(strAscii1) == 51)
                encryptPassword = encryptPassword + "d";
            if (Convert.ToInt32(strAscii1) == 52)
                encryptPassword = encryptPassword + "e";
            if (Convert.ToInt32(strAscii1) == 53)
                encryptPassword = encryptPassword + "f";
            if (Convert.ToInt32(strAscii1) == 54)
                encryptPassword = encryptPassword + "g";
            if (Convert.ToInt32(strAscii1) == 55)
                encryptPassword = encryptPassword + "h";
            if (Convert.ToInt32(strAscii1) == 56)
                encryptPassword = encryptPassword + "i";
            if (Convert.ToInt32(strAscii1) == 57)
                encryptPassword = encryptPassword + "j";

            intCounterForAscii++;
            strAsciiLength--;
        }
    }
    // Fire login button click event
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        byte[] arrInput;
        // creating class object
        var objLogin = new Login();
        string strPassword = txtPassword.Text.ToString().Trim();
        arrInput = Convert2ByteArray(strPassword);
        int strLength = strPassword.Length;
        while (strLength > 0)
        {
            fetchChar = Convert.ToChar(arrInput.GetValue(intCounterForChar));
            strAscii = Convert.ToInt32(fetchChar).ToString();
            strAsciiLength = strAscii.Length;
            arrInput1 = Convert2ByteArray(strAscii);
            if (Convert.ToInt32(fetchChar) >= 48 && Convert.ToInt32(fetchChar) <= 57)
            {
                ConvertNumberIntoSpecialSymbol();
            }
            else
            {
                if ((Convert.ToInt32(fetchChar) >= 65 && Convert.ToInt32(fetchChar) <= 92) || (Convert.ToInt32(fetchChar) >= 97 && Convert.ToInt32(fetchChar) <= 122))
                {
                    ConvertAsciiIntoSpecialAlpha();

                }
                else
                {
                    encryptPassword = encryptPassword + Convert.ToInt32(fetchChar).ToString();
                }

            }

            intCounterForAscii = 0;
            encryptPassword = encryptPassword + "@";
            intCounterForChar++;
            strLength--;
        }
        int id = 0;
        //calling method for check login
        Login objlogin = new Login();
        id = objlogin.IsAuthenticate(txtUsername.Text.Trim(), encryptPassword);
        // TO check id value
        if (id > 0)
        {
            //calling get salary detail method
           
            var dt = objlogin.GetSalesDetail(id);
            // check if datatable has rows
            if (dt != null)
            {
                Session.Timeout = 100;
                // TO add session value
                Session.Add("RoleTypeID", dt.Rows[0]["RoleTypeID"]);
                Session.Add("Technology", dt.Rows[0]["Technology"]);
                Session.Add("SalesDetailID", dt.Rows[0]["ID"]);
                Session.Add("ProjectTypeID", dt.Rows[0]["ProjectHandlingTypeId"]);
                Session.Add("SalesPersonName", dt.Rows[0]["Name"]);
                Session.Add("EmailId", dt.Rows[0]["EmailId"]);
                Session.Add("Password", dt.Rows[0]["Password"]);
                Session.Add("UserId", dt.Rows[0]["UserId"]);
                 var iiiid = Session["SalesDetailID"];

                //if (Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 2)
                //{
                //    Response.Redirect(oAppPath + "/Module/Support/SearchableList.aspx");
                //}
                //else
                //{
                //    //Create by Anuj ......
                     
                //     if (Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 3)
                //    {
                //         //For execution members.....
                //         Response.Redirect(oAppPath + "/Module/ProjectManagement/AllProjectList.aspx");
                //    }

                //     if (Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 4)
                //     {
                //         //For Team Heads....
                //         Response.Redirect(oAppPath + "/Module/ProjectManagement/AllProjectList.aspx");
                //     }
                //    //BUG Tracker=5
                 if (Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 6 || Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 7 || Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 0)
                 {
                     //For Team Heads
                     Response.Redirect(oAppPath + "/Module/BugTracker/home.aspx");
                 }
                //    //End by Anuj .......

                 //if (Convert.ToInt32(dt.Rows[0]["RoleTypeID"]) == 0)
                 //{
                 //    Response.Redirect(oAppPath + "/Module/BugTracker/home.aspx");

                 //}
                //    else
                //    {
                //        if (Convert.ToInt32(dt.Rows[0]["ProjectHandlingTypeId"]) == 1)
                //        {
                //            Response.Redirect(oAppPath + "/Module/Sales/Directposting.aspx");
                //        }
                //        else if (Convert.ToInt32(dt.Rows[0]["ProjectHandlingTypeId"]) == 2)
                //        {
                //            Response.Redirect(oAppPath + "/Module/Sales/Onlineposting.aspx");
                //        }
                //        else if (Convert.ToInt32(dt.Rows[0]["ProjectHandlingTypeId"]) == 3)
                //        {
                //            Response.Redirect(oAppPath + "/Module/Sales/OnlinepostingList.aspx");
                //        }
                //    }
                //}
            }//end inner if
        }//end outer if
        //if there is some problem while login
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = Resources.SalesReporting.Invalidusernameandpassword;
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}