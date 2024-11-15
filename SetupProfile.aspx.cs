using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SetupProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FolioNo"] != null) { 
            string folio=Session["FolioNo"].ToString();
            DataTable tbl= GetData(folio);
            p_name.Text = tbl.Rows[0]["TITLE"].ToString();
            address.Text = tbl.Rows[0]["Registered_address"].ToString();
            ddlbankname.Text = tbl.Rows[0]["BANK_NAME"].ToString();
            accnumber.Text= tbl.Rows[0]["ACCOUNT_NUMBER"].ToString();
            
            string nic = folio.Replace("-PF", "");
            DataTable tbl2 = GetData2(nic);
            Fname.Text = tbl2.Rows[0]["FATHER_HUSB_NAME"].ToString();
            ddlmaritialstatus.Text=tbl2.Rows[0]["MARITAL_STATUS"].ToString()=="S"?"Single":"Married";
            email.Text= tbl2.Rows[0]["E_MAIL"].ToString();
            mothername.Text = tbl2.Rows[0]["MOTHER_NAME"].ToString();
            cellno.Text= tbl2.Rows[0]["PHONE_ONE"].ToString();
            ddloccupation.Text = "Private Employee";
            DateTime cnicISSUEDate = Convert.ToDateTime(tbl2.Rows[0]["CNIC_ISSUE_DATE"]);
            issuedate.Text = cnicISSUEDate.ToString("yyyy-MM-dd");
            DateTime cnicExpiryDate = Convert.ToDateTime(tbl2.Rows[0]["CNIC_EXPIRY_DATE"]);
            expirydate.Text = cnicExpiryDate.ToString("yyyy-MM-dd");
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private DataTable GetData(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = "select * from unit_account u where u.folio_number=:folio_number";

            
            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.CloseConnection();
        }

        return tbl;
    }

    private DataTable GetData2(string cnic)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = "select * from client c where c.nic_passport=:nic_passport";


            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":nic_passport", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = cnic;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.CloseConnection();
        }

        return tbl;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ID = Session["ID"].ToString();
        string extfront = Path.GetExtension(cnicfront.PostedFile.FileName).ToLower();
        string CnicFrontpath = Server.MapPath("~/Documents/" + ID + " CNIC FRONT" + extfront);
        if (!String.IsNullOrEmpty(cnicfront.PostedFile.FileName))
        {
            cnicfront.PostedFile.SaveAs(CnicFrontpath);
        }
        string extback = Path.GetExtension(cnicback.PostedFile.FileName).ToLower();
        string CNICBACKpath = Server.MapPath("~/Documents/" + ID + " CNIC BACK" + extback);
        if (!String.IsNullOrEmpty(cnicback.PostedFile.FileName))
        {
            cnicback.PostedFile.SaveAs(CNICBACKpath);
        }
        string extzakat = Path.GetExtension(zakatAffidavit.PostedFile.FileName).ToLower();
        string Zakatpath = Server.MapPath("~/Documents/" + ID + " ZAKAT AFFIDAVIT" + extzakat);
        if (!String.IsNullOrEmpty(zakatAffidavit.PostedFile.FileName))
        {
            zakatAffidavit.PostedFile.SaveAs(Zakatpath);
        }

        string extsignature = Path.GetExtension(Signature.PostedFile.FileName).ToLower();
        string Signaturepath = Server.MapPath("~/Documents/" + ID + " SIGNATURE" + extsignature);
        if (!String.IsNullOrEmpty(Signature.PostedFile.FileName))
        {
            Signature.PostedFile.SaveAs(Signaturepath);
        }

        string qry = "INSERT INTO EmployeeDocuments (Participant_ID, DocumentFor, DocumentName, Document,VPS_CORPORATE_ID,docpath) " +
                  "SELECT @Participant_ID, @DocumentFor, @FileName, BulkColumn,@VPS_CORPORATE_ID,@docpath " +
                  "FROM OPENROWSET(BULK N'" + CnicFrontpath + "', SINGLE_BLOB) AS DocumentContent; " +
                "delete from EmployeeDocuments where DocumentFor = 'CNIC Front' and Participant_ID = @Participant_ID and (select Count(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'CNIC Front' and Participant_ID = @Participant_ID) > 1 and DOCUMENTID = (select min(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'CNIC Front' and Participant_ID = @Participant_ID); ";
        SqlParameter[] p = new SqlParameter[5];
        p[0] = new SqlParameter();
        p[0].ParameterName = "@Participant_ID";
        p[0].SqlDbType = SqlDbType.NVarChar;
        p[0].Value = Session["ID"].ToString();

        p[1] = new SqlParameter();
        p[1].ParameterName = "@DocumentFor";
        p[1].SqlDbType = SqlDbType.NVarChar;
        p[1].Value = "CNIC Front";

        p[2] = new SqlParameter();
        p[2].ParameterName = "@FileName";
        p[2].SqlDbType = SqlDbType.NVarChar;
        p[2].Value = cnicfront.PostedFile.FileName;

        p[3] = new SqlParameter();
        p[3].ParameterName = "@VPS_CORPORATE_ID";
        p[3].SqlDbType = SqlDbType.NVarChar;
        p[3].Value = Session["VPS_CORPORATE_ID"].ToString();

        p[4] = new SqlParameter();
        p[4].ParameterName = "@docpath";
        p[4].SqlDbType = SqlDbType.NVarChar;
        p[4].Value = CnicFrontpath;

        DALSqlServer.Execute(qry, p);

        qry = "INSERT INTO EmployeeDocuments (Participant_ID, DocumentFor, DocumentName, Document,VPS_CORPORATE_ID,docpath) " +
                  "SELECT @Participant_ID, @DocumentFor, @FileName, BulkColumn,@VPS_CORPORATE_ID,@docpath " +
                  "FROM OPENROWSET(BULK N'" + CNICBACKpath + "', SINGLE_BLOB) AS DocumentContent; " +
                "delete from EmployeeDocuments where DocumentFor = 'CNIC Back' and Participant_ID = @Participant_ID and (select Count(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'CNIC Back' and Participant_ID = @Participant_ID) > 1 and DOCUMENTID = (select min(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'CNIC Back' and Participant_ID = @Participant_ID); ";
        p = new SqlParameter[5];
        p[0] = new SqlParameter();
        p[0].ParameterName = "@Participant_ID";
        p[0].SqlDbType = SqlDbType.NVarChar;
        p[0].Value = Session["ID"].ToString();

        p[1] = new SqlParameter();
        p[1].ParameterName = "@DocumentFor";
        p[1].SqlDbType = SqlDbType.NVarChar;
        p[1].Value = "CNIC Back";

        p[2] = new SqlParameter();
        p[2].ParameterName = "@FileName";
        p[2].SqlDbType = SqlDbType.NVarChar;
        p[2].Value = cnicback.PostedFile.FileName;

        p[3] = new SqlParameter();
        p[3].ParameterName = "@VPS_CORPORATE_ID";
        p[3].SqlDbType = SqlDbType.NVarChar;
        p[3].Value = Session["VPS_CORPORATE_ID"].ToString();

        p[4] = new SqlParameter();
        p[4].ParameterName = "@docpath";
        p[4].SqlDbType = SqlDbType.NVarChar;
        p[4].Value = CNICBACKpath;

        DALSqlServer.Execute(qry, p);

       
        qry = "INSERT INTO EmployeeDocuments (Participant_ID, DocumentFor, DocumentName, Document,VPS_CORPORATE_ID,docpath) " +
                  "SELECT @Participant_ID, @DocumentFor, @FileName, BulkColumn,@VPS_CORPORATE_ID,@docpath " +
                  "FROM OPENROWSET(BULK N'" + Zakatpath + "', SINGLE_BLOB) AS DocumentContent; " +
                "delete from EmployeeDocuments where DocumentFor = 'Zakat Affidavit' and Participant_ID = @Participant_ID and (select Count(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'Zakat Affidavit' and Participant_ID = @Participant_ID) > 1 and DOCUMENTID = (select min(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'Zakat Affidavit' and Participant_ID = @Participant_ID); ";
        p = new SqlParameter[5];
        p[0] = new SqlParameter();
        p[0].ParameterName = "@Participant_ID";
        p[0].SqlDbType = SqlDbType.NVarChar;
        p[0].Value = Session["ID"].ToString();

        p[1] = new SqlParameter();
        p[1].ParameterName = "@DocumentFor";
        p[1].SqlDbType = SqlDbType.NVarChar;
        p[1].Value = "Zakat Affidavit";

        p[2] = new SqlParameter();
        p[2].ParameterName = "@FileName";
        p[2].SqlDbType = SqlDbType.NVarChar;
        p[2].Value = Signature.PostedFile.FileName;

        p[3] = new SqlParameter();
        p[3].ParameterName = "@VPS_CORPORATE_ID";
        p[3].SqlDbType = SqlDbType.NVarChar;
        p[3].Value = Session["VPS_CORPORATE_ID"].ToString();

        p[4] = new SqlParameter();
        p[4].ParameterName = "@docpath";
        p[4].SqlDbType = SqlDbType.NVarChar;
        p[4].Value = Zakatpath;

        DALSqlServer.Execute(qry, p);

        qry = "INSERT INTO EmployeeDocuments (Participant_ID, DocumentFor, DocumentName, Document,VPS_CORPORATE_ID,docpath) " +
                  "SELECT @Participant_ID, @DocumentFor, @FileName, BulkColumn,@VPS_CORPORATE_ID,@docpath " +
                  "FROM OPENROWSET(BULK N'" + Signaturepath + "', SINGLE_BLOB) AS DocumentContent; " +
                "delete from EmployeeDocuments where DocumentFor = 'Signature' and Participant_ID = @Participant_ID and (select Count(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'Signature' and Participant_ID = @Participant_ID) > 1 and DOCUMENTID = (select min(DOCUMENTID) from EmployeeDocuments where DocumentFor = 'Signature' and Participant_ID = @Participant_ID); ";
        p = new SqlParameter[5];
        p[0] = new SqlParameter();
        p[0].ParameterName = "@Participant_ID";
        p[0].SqlDbType = SqlDbType.NVarChar;
        p[0].Value = Session["ID"].ToString();

        p[1] = new SqlParameter();
        p[1].ParameterName = "@DocumentFor";
        p[1].SqlDbType = SqlDbType.NVarChar;
        p[1].Value = "Signature";

        p[2] = new SqlParameter();
        p[2].ParameterName = "@FileName";
        p[2].SqlDbType = SqlDbType.NVarChar;
        p[2].Value = Signature.PostedFile.FileName;

        p[3] = new SqlParameter();
        p[3].ParameterName = "@VPS_CORPORATE_ID";
        p[3].SqlDbType = SqlDbType.NVarChar;
        p[3].Value = Session["VPS_CORPORATE_ID"].ToString();

        p[4] = new SqlParameter();
        p[4].ParameterName = "@docpath";
        p[4].SqlDbType = SqlDbType.NVarChar;
        p[4].Value = Signaturepath;

        DALSqlServer.Execute(qry, p);


        Page.ClientScript.RegisterStartupScript(this.GetType(), "UploadAlert", "alert('Your document uploaded successfully.');", true);

        Response.Redirect("Default.aspx");


    }
}