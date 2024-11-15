using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ForgetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private DataTable getDetails(string userid)
    {
        string qry = "select * from participantcontributiondetails where cnic=@cnic";
        SqlParameter[] p = new SqlParameter[1];
        p[0] = new SqlParameter();
        p[0].ParameterName = "@cnic";
        p[0].SqlDbType = SqlDbType.NVarChar;
        p[0].Value = userid;


        //p[1] = new SqlParameter();
        //p[1].ParameterName = "@password";
        //p[1].SqlDbType = SqlDbType.NVarChar;
        //p[1].Value = Password;
        DataTable tbl = DALSqlServer.ExecuteTable(qry, p);

        return tbl;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        DataTable getUser = getDetails(txtUsername.Text);
        if (getUser.Rows.Count > 0)
        {
            string NAME = getUser.Rows[0]["Participant_Name"].ToString();
            string USERID = getUser.Rows[0]["CNIC"].ToString();
            string PASSWORD = getUser.Rows[0]["Password"].ToString();
            string EMAIL = getUser.Rows[0]["Email"].ToString();

            string emailcheck = SendEmail(NAME, USERID, PASSWORD, EMAIL);
            if (emailcheck == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('An Email has been sent to your registered Email Address');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('There was an error while sending an email to your registered email address, Please try again later.');", true);
            }
        }
        //if (getUser.Rows.Count > 0)
        //{
        //    string folioWithDashes = getUser.Rows[0]["cnic"].ToString();
        //    Session["FolioNo"] = string.Format("{0}-{1}-{2}-PF", folioWithDashes.Substring(0, 5), folioWithDashes.Substring(5, 7), folioWithDashes.Substring(12));
        //    Session["TITLE"] = getUser.Rows[0]["participant_name"];
        //    Session["ID"] = getUser.Rows[0]["Participant_ID"];
        //    Session["VPS_CORPORATE_ID"] = getUser.Rows[0]["VPS_CORPORATE_ID"];

        //    Response.Redirect("Default.aspx");
        //}

        //if (txtUsername.Text == "SMAPORTAL" && txtPassword.Text == "P@ss1234")
        //    {
        //     Session["USERID"] = "SMAPORTAL";
        //        Session["FUNDCODE"] = "ADMIN";
        //        Session["USERNAME"] = "ADMIN";
        //        Session["FUNDNAME"] = "ADMIN";
        //        Session["ROLECODE"] = "";
        //        Session["USERTYPE"] = "";
        //        Response.Redirect("uploadReports.aspx");
        //    }

        //    string roleCode = "";
        //   string  userType="";
        //    ValidateUsers(txtUsername.Text, txtPassword.Text, out roleCode, out userType);
        //    if(roleCode =="" && userType =="")
        //    {
        //        lblMessage.Text = "User id or password not correct!";

        //    }
        //    else
        //    {
        //        CreateSession(txtUsername.Text, roleCode, userType);
        //    }



        //}
    }

    private void CreateSession(string user, string roleCode, string userType)
    {

        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {


            string qry = @"SELECT F.FUND_CODE,F.FUND_NAME, fd.FUND_IDENTIFIER,substr(us.name,0,INSTR(us.name, ' ')) NAME FROM 
            FUND F 
            INNER JOIN UNIT_ACCOUNT U ON U.FOLIO_NUMBER=F.FOLIO_NUMBER
            INNER JOIN UNIT_ACC_CLIENT UAC ON U.FOLIO_NUMBER=UAC.FOLIO_NUMBER
            INNER JOIN CLIENT C ON C.CLIENT_CODE=UAC.CLIENT_CODE
            INNER JOIN USERS US ON US.CLIENT_CODE=C.CLIENT_CODE
            INNER JOIN FUND_DETAIL fd ON fd.fund_code=f.fund_code 
            WHERE US.USER_ID=:USER_ID";
            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":USER_ID", (object)user);
            oracleParameterArray[0].Direction = ParameterDirection.Input;

            DataTable tbl = DAL.OracleDataAccess.ExecuteDataTable(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray);
            if (tbl.Rows.Count > 0)
            {
                Session["USERID"] = user;
                Session["FUNDCODE"] = tbl.Rows[0]["FUND_CODE"].ToString();
                Session["USERNAME"] = tbl.Rows[0]["NAME"].ToString();
                Session["FUNDNAME"] = tbl.Rows[0]["FUND_NAME"].ToString();
                Session["FUND_IDENTIFIER"] = tbl.Rows[0]["FUND_IDENTIFIER"].ToString();
                Session["ROLECODE"] = roleCode;
                Session["USERTYPE"] = userType;

                Response.Redirect("Default.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.CloseConnection();
        }


    }
    private void ValidateUsers(string user, string pass, out string roleCode, out string userType)
    {
        roleCode = "";
        userType = "";
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {


            string qry = "validateUser2";
            OracleParameter[] oracleParameterArray = new OracleParameter[4];
            oracleParameterArray[0] = new OracleParameter("user", (object)user);
            oracleParameterArray[0].Direction = ParameterDirection.Input;

            oracleParameterArray[1] = new OracleParameter("pass", (object)pass);
            oracleParameterArray[1].Direction = ParameterDirection.Input;




            oracleParameterArray[2] = new OracleParameter("roleCode", OracleType.VarChar, 100);
            oracleParameterArray[2].Direction = ParameterDirection.Output;

            oracleParameterArray[3] = new OracleParameter("userType", OracleType.VarChar, 100);
            oracleParameterArray[3].Direction = ParameterDirection.Output;

            DAL.OracleDataAccess.ExecuteNonQuery(cn.GetConnection(), CommandType.StoredProcedure, "validateUser2", oracleParameterArray);
            roleCode = oracleParameterArray[2].Value.ToString();
            userType = oracleParameterArray[3].Value.ToString();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.CloseConnection();
        }


    }
    public string SendEmail(string name, string user_name, string pass, string email)
    {
        string htmlfile = File.OpenText(HttpContext.Current.Server.MapPath("index4.html")).ReadToEnd();
        htmlfile = htmlfile.Replace("##fullname##", name);
        htmlfile = htmlfile.Replace("##Username##", user_name);
        htmlfile = htmlfile.Replace("##Password##", pass);
        htmlfile = htmlfile.Replace("##URL##", "https://fund.jsil.com/vps/Login.aspx");
        string id = SendSMS.SendEmail(email, "JS Investments - VPS Individual Password Recovery", htmlfile, "");
        return id;
    }


}