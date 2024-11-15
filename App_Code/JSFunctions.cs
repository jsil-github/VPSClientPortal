using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class JSFunctions
{
   
    public JSFunctions()
    {

    }
    


    public static string Current_Date_From_Oracle
    {
        get

        {
            if (HttpContext.Current.Session["currentdate"] == null)
            {
                return currentDate();
            }
            else
            {
                string val = "";
                if (HttpContext.Current.Session["currentdate"].ToString().Contains("-"))
                {
                    val = currentDate();
                }
                else
                {
                    val = HttpContext.Current.Session["currentdate"].ToString();
                }
                return val;
            }

        }
        //get { return HttpContext.Current.Session["currentdate"].ToString(); }
    }

    protected static string currentDate()
    {
        object val = null;
        OracleTransaction myTrans = null;
        OracleConnection objConn = null;
        objConn = DAL.clsOracleConnection.GetConnectionTrans();
        myTrans = objConn.BeginTransaction();
        try
        {

            val = DAL.OracleDataAccess.ExecuteScalar(myTrans, CommandType.Text, "select to_date(sysdate) from dual");
            HttpContext.Current.Session["currentdate"] = Convert.ToDateTime(val).ToString("dd/MM/yyyy");
            myTrans.Commit();


        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            // myTrans.Connection.Close();
        }
        return Convert.ToDateTime(val).ToString("dd/MM/yyyy");
    }
     


    public class ErrorLog
    {
        public static void LogError1(string strHeader, string strError)
        {
            string str1 = DateTime.Now.ToString("ddMMyyyy");
            try
            {
                string path1 = ConfigurationSettings.AppSettings["sPathErrorLog"];
                ErrorLog.EnsureDirectory(new DirectoryInfo(path1));
                string path2 = path1 + str1 + ".log";
                string str2 = (string)(object)DateTime.Now + (object)" : " + strHeader + " : " + strError;
                StreamWriter streamWriter = new StreamWriter(path2, true, Encoding.ASCII);
                streamWriter.WriteLine("");
                streamWriter.WriteLine(str2);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LogError(string strErrorHeader, string strEx)
        {
            string path = ((object)ConfigurationSettings.AppSettings["sPathErrorLog"]).ToString();
            StringBuilder stringBuilder = new StringBuilder();
            Page page = new Page();
            string str = page.Session["USER_ID"] != null ? page.Session["USER_ID"].ToString() : "";
            try
            {
                stringBuilder.Append("\\");
                stringBuilder.Append(DateTime.Now.ToString("ddMMyyyy"));
                stringBuilder.Append(".log");
                stringBuilder.Insert(0, page.Server.MapPath(path));
                StreamWriter streamWriter = new StreamWriter(((object)stringBuilder).ToString(), true, Encoding.ASCII);
                streamWriter.WriteLine("Time         -> " + DateTime.Now.ToLongTimeString());
                streamWriter.WriteLine("UserId       -> " + str);
                streamWriter.WriteLine("Error Header -> " + strErrorHeader);
                streamWriter.WriteLine("ErrorMessage -> " + strEx);
                streamWriter.WriteLine("==================================================================================================================");
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public static void EnsureDirectory(DirectoryInfo oDirInfo)
        {
            if (oDirInfo.Exists)
                return;
            oDirInfo.Create();
        }
    }

    public static void ReportLog(string REPORT_ID, string REPORT_UID)
    {
        DAL.clsOracleConnection oracleConnection = new DAL.clsOracleConnection();
        string qry = "";

        try
        {


            qry = "insert into REPORT_LOGS (REPORT_ID, APP_TYPE, REPORT_UID, REPORT_DT) values (:REPORT_ID, :APP_TYPE, :REPORT_UID, sysdate)";
            OracleParameter[] oracleParameterArray = new OracleParameter[3];
            oracleParameterArray[0] = new OracleParameter(":REPORT_ID", OracleType.NVarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = REPORT_ID;
            oracleParameterArray[1] = new OracleParameter(":APP_TYPE", OracleType.NVarChar);
            oracleParameterArray[1].Direction = ParameterDirection.Input;
            oracleParameterArray[1].Value = "PORTAL";
            oracleParameterArray[2] = new OracleParameter(":REPORT_UID", OracleType.NVarChar);
            oracleParameterArray[2].Direction = ParameterDirection.Input;
            oracleParameterArray[2].Value = REPORT_UID;

            DAL.OracleDataAccess.ExecuteNonQuery(oracleConnection.GetConnection(), CommandType.Text, qry, oracleParameterArray);


        }
        catch (Exception ex)
        {


        }
        finally
        {
            oracleConnection.CloseConnection();
        }
    }


    public static void RegisterScript(string mesg, Page p, string withMsg)
    {
        Random rnd = new Random(100);
        ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "Script" + rnd.Next(1, 100).ToString(), "alert('" + mesg + "');" + withMsg + "", true);
    }
    public static void RegisterScriptConfirm(string mesg, Page p)
    {
        Random rnd = new Random(100);
        ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "Script" + rnd.Next(1, 100).ToString(), "confirm ('" + mesg + "');", true);
    }
    public static void RefreshControls(ControlCollection Controls)
    {
        try
        {
            foreach (Control cntrl in Controls)
            {
                if (cntrl.HasControls())
                    RefreshControls(cntrl.Controls);

                if (cntrl.GetType() == typeof(TextBox))
                    ((TextBox)cntrl).Text = "";

                if (cntrl.GetType() == typeof(DropDownList))
                {
                    if (((DropDownList)cntrl).Items.Count > 0)
                        ((DropDownList)cntrl).SelectedIndex = 0;
                }

                if (cntrl.GetType() == typeof(CheckBox))
                    ((CheckBox)cntrl).Checked = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static string dateFormat(string date)
    {
        DateTime newdate = new DateTime();
        string mydate = "";
        if (DateTime.TryParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newdate))
        {
            if (date == "")
            {
                date = DateTime.Now.ToString("d/M/yyyy");
            }


        }
        else
        {
            if (date == "")
            {
                date = DateTime.Now.ToString("d/M/yyyy");
            }
            string dd = "";
            string mm = "";
            string yyyy = "";


            string[] spl = date.Split('/');
            for (int i = 0; i < spl.Length; i++)
            {
                if (i == 0)
                {
                    if (Convert.ToInt16(spl[i]) > 12)
                    {
                        dd = spl[i];
                    }
                    else
                    {
                        mm = spl[i];
                    }

                }
                if (i == 1)
                {
                    if (dd == "")
                    {
                        if (spl[i].Length < 2)
                        {
                            dd = "0" + spl[i];
                        }

                        else
                        {
                            dd = spl[i];
                        }
                    }
                    else
                    {
                        mm = spl[i];
                    }
                }
                if (i == 2)
                {
                    yyyy = spl[i];
                }

            }
            mydate = dd + "/" + mm + "/" + yyyy;
            if (mydate != "")
            {
                if (mydate.IndexOf(" ") > 0)
                {
                    mydate = mydate.Substring(0, mydate.IndexOf(" "));
                }
            }
            DateTime.TryParseExact(mydate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newdate);
        }

        return newdate.ToString("dd-MMM-yyyy");

    }



}