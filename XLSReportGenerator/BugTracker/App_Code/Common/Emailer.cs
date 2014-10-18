using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Net.Mail;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for Emailer
/// </summary>
public class Emailer
{

    public string Subject { get; set; }
    public string Message { get; set; }

    public Emailer GetEmailBodyByEmailTemplateKey(string emailTemplateKey, string[,] replaceMents)
    {
        var oEmail = new Emailer();
        var dt = GetEmailTemplate(emailTemplateKey);
        if (dt != null && dt.Rows.Count > 0)
        {
            oEmail.Subject = ArrayReplaceMents(replaceMents, dt.Rows[0]["Subject"].ToString());
            oEmail.Message = ArrayReplaceMents(replaceMents, dt.Rows[0]["Message"].ToString());
        }
        return oEmail;
    }
    public string ArrayReplaceMents(string[,] replacementParam, string message)
    {
        string content = message;
        if (replacementParam != null)
        {
            for (int currentRow = 0; currentRow <= replacementParam.GetUpperBound(0); currentRow++)
            {
                content = ReplaceParameter(content, replacementParam[currentRow, 0].ToString(), replacementParam[currentRow, 1].ToString());
            }
        }
        return content;
    }
    public DataTable GetEmailTemplate(string Key)
    {
        string strQuery = string.Empty;
        strQuery = "select * from EmailTemplate where [Key]='" + Key + "'";
        return SqlHelper.FillDataTable(strQuery,"BT");
    }
    public string ReplaceParameter(string content, string Key, string ReplaceValue)
    {
        return content.Replace(Key, ReplaceValue);
    }

    public bool SendEmail(string[,] replaceMents, string emailTemplateKey, string toEmail, bool isEmailToAdmin, string filepath)
    {
        bool status = false;

        var from = new MailAddress(Config.FromEmail, Config.FromName);
        string[] toUserList = toEmail.Split(',');
        MailMessage msg;
        if (toEmail != "" && toUserList.Length == 1)
        {
            var to = new MailAddress(toEmail);
            msg = new MailMessage(from, to);
        }
        else
        {
            msg = new MailMessage(from, from);
        }
        for (var j = 0; j < toUserList.Length; j++)
        {
            if (toUserList[j].Contains("@"))
            {
                msg.To.Add(toUserList[j]);
            }
        }

        try
        {
            
            if (isEmailToAdmin)
            {
                //admin email id 
                msg.CC.Add(Config.AdminEmail);
            }

            if (filepath != "")
            {
                var atcFile = new Attachment(filepath);
                msg.Attachments.Add(atcFile);
            }
            var oEmail = GetEmailBodyByEmailTemplateKey(emailTemplateKey, replaceMents);
            msg.Body = oEmail.Message;
            msg.Subject = oEmail.Subject;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            var client = new SmtpClient(Config.Host, Config.port);//
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            var theCredential = new System.Net.NetworkCredential(Config.UserName, Config.Password);
            client.Credentials = theCredential;
            client.Send(msg);//Sending the mail..
            status = true;

        }
        catch (Exception ex)
        {

        }
        return status;

    }
    // Function to send mail 
    public bool SendEmailNotification(string[,] replaceMents, string emailTemplateKey, string toEmail, string ccEmail,string bccEmail)
    {
        bool status = false;
        MailMessage msg = null;
        var from = new MailAddress(Config.FromEmail, Config.FromName);
        string[] toUserList = toEmail.Split(',');
        string[] ccUserList = ccEmail.Split(',');
        string[] bccUserList = bccEmail.Split(',');
       

        if (toEmail != "")
        {
            if (toEmail != "" && toUserList.Length == 1)
            {
                var to = new MailAddress(toEmail);
                msg = new MailMessage(from, to);
            }
            else
            {
                msg = new MailMessage(from, from);
            }
            for (var j = 0; j < toUserList.Length; j++)
            {
                if (toUserList[j].Contains("@"))
                {
                    msg.To.Add(toUserList[j]);
                }
            }
        }
        if (ccEmail != "")
        {


            if (ccEmail != "" && ccUserList.Length == 1)
            {
                var cc = new MailAddress(ccEmail);
                msg = new MailMessage(from, cc);
            }
            else
            {
                msg = new MailMessage(from, from);
            }
            for (var i = 0; i < ccUserList.Length; i++)
            {
                if (ccUserList[i].Contains("@"))
                {
                    msg.CC.Add(ccUserList[i]);
                }
            }
        }
        if (bccEmail != "")
        {
            if (bccEmail != "" && bccUserList.Length == 1)
            {
                var bcc = new MailAddress(bccEmail);
                msg = new MailMessage(from, bcc);
            }
            else
            {
                msg = new MailMessage(from, from);
            }
            for (var k = 0; k < ccUserList.Length; k++)
            {
                if (bccUserList[k].Contains("@"))
                {
                    msg.Bcc.Add(bccUserList[k]);
                }
            }
        }
        try
        {
            var oEmail = GetEmailBodyByEmailTemplateKey(emailTemplateKey, replaceMents);
            msg.Body = oEmail.Message;
            msg.Subject = oEmail.Subject;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            var client = new SmtpClient(Config.Host, Config.port);//
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            var theCredential = new System.Net.NetworkCredential(Config.UserName, Config.Password);
            client.Credentials = theCredential;
            client.Send(msg);//Sending the mail..
            status = true;

        }
        catch (Exception ex)
        {

        }
        return status;

    }

}
