using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
/// Summary description for Login
/// </summary>
public class Login
{
     //Method for checking the Login Details from database
    public int IsAuthenticate(string Username,string Password)
    {   
        string sql = "select id from at_user where username='" + Username + "' and Pwd='" + Password + "'";
        var result = SqlHelper.ExecuteScalar(SqlHelper.GetConnectionString("TTMS"), CommandType.Text, sql);
        return Convert.ToInt32(result);
    }
    //Method for fetching the sales detail
    public DataTable GetSalesDetail(int UserId)
    {
        string sql = "Select * from SalesDetail where UserId=" + UserId;
        return SqlHelper.FillDataTable(sql, "SR");
    }
}