using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
	       //if (Session["USERID"] == "SMAPORTAL")
            //       {
         //          Response.Redirect("uploadReports.aspx");
            //       }
	       //if (Session["USERID"] == null)
            //       {
         //          Response.Redirect("signout.aspx");
                //       }
	       //if(!IsPostBack)
         //       {
           
         //       }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
            try
            {

            string cnic = Session["FolioNo"].ToString();
            string b=cnic.Replace("-", "");
            string c = b.Replace("-", "");
            string d = c.Replace("-", "");
            string ecnic = d.Replace("PF", "");
            string pass = ConfirmPass.Text;
            string qry = "update participantcontributiondetails set password=@password where cnic=@cnic";
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter();
            p[0].ParameterName = "@password";
            p[0].SqlDbType = SqlDbType.NVarChar;
            p[0].Value = pass;


            p[1] = new SqlParameter();
            p[1].ParameterName = "@cnic";
            p[1].SqlDbType = SqlDbType.NVarChar;
            p[1].Value = ecnic;
            bool isinserted = DALSqlServer.Execute(qry, p);
            if (isinserted==true)
            {
                string script = "alert('Password Updated'); window.location='ChangePassword.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript", script, true);
            }
            else
            {
                string script = "alert('Error');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", script, true);
            }
            //Response.Write("<script>alert('Passowrd Updated');window.location = 'ChangePassword.aspx';</script>");
              }
              catch (Exception ex)
              {
                  Response.Write(ex.Message);
		          Response.Write("<script>alert('Error');</script>");
              }
              finally
              {
                  cn.CloseConnection();
              }        
        }

}
    
