using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for SendSMS
/// </summary>
public class SendSMS
{
    public SendSMS()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void sendMessage(string mobile,string message)
    {
        XDocument xDoc = new XDocument
        (
            new XDeclaration("1.0", "ISO-8859-1", null),
            new XDocumentType("MESSAGE", null, "http://127.0.0.1:80/psms/dtd/messagev12.dtd", null),
            new XElement("MESSAGE",
                new XAttribute("VER", "1.2"),
                new XElement("USER", 
                    new XAttribute("USERNAME", "jsilotpxml"),
                    new XAttribute("PASSWORD", "jsilxmlotp")
                    ),
                new XElement("SMS", 
                    new XAttribute("UDH", "0"),
                    new XAttribute("CODING", "1"),
                    new XAttribute("TEXT", message),
                    new XAttribute("PROPERTY", "0"),
                    new XAttribute("ID","1"),
                    new XElement("ADDRESS",
                        new XAttribute("FROM", "8027"),
                        new XAttribute("TO", mobile),
                        new XAttribute("SEQ", "1"),
                        new XAttribute("TAG", "Login Credentials")
                        )
                    )
                )
        );
        
        using (WebClient wc = new WebClient())
        {
            try
            {
                var responseBody = wc.UploadString("http://pace.myvfirst.com.pk/psms/servlet/psms.Eservice2?data=" + xDoc+ "&action=send", "POST");
                //XDocument responseDocument = XDocument.Parse(responseBody.ToString());
                //String ResponsexmlFile = HttpContext.Current.Server.MapPath("~/Res.xml");
                //responseDocument.Save(ResponsexmlFile);
            }
            catch (WebException ex)
            {
                JSFunctions.ErrorLog.LogError("ERROR SMS API", ex.Message);
            }
        }
    }
    public static string SendEmail(string MailTo, string Subject, string ObjMailBody, string CC = "")
    {
        string retval = "";

        string sSmtpUser = ConfigurationManager.AppSettings.Get("smtpUser");
        string sSmtpPass = ConfigurationManager.AppSettings.Get("smtpPass");
        string sSmtpIPAddress = ConfigurationManager.AppSettings.Get("smtpIP");
        string sEmailFrom = ConfigurationManager.AppSettings.Get("EmailFrom");

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // .NET 4.0           
        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();

        string msg = string.Empty;
        try
        {
            MailAddress fromAddress = new MailAddress(sEmailFrom, "JS Investments");
            message.From = fromAddress;
            message.To.Add(MailTo);

            message.Subject = Subject;


            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Body = ObjMailBody;
            //smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
            smtpClient.Host = "smtp.sendgrid.net";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(sSmtpUser, sSmtpPass);

            smtpClient.Send(message);
            retval = "1";
        }
        catch (Exception ex)
        {
            retval = "0";
        }
        return retval;


    }
}