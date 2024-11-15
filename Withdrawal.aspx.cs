using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Withdrawal : System.Web.UI.Page
{
    public static string amount;
    public static string FolioNo;
    public static string fund;
    public static string bankcode;
    public static string bankname;
    public static string bankaddress;
    public static string accountnumber;
    public static string bankcity;
    public static string bankacctitle;
    public static decimal decimalValue;
    public static string withdrawamount;
    public static double decimalValue2;
    public static string CorporateCode;
    public static string registeredaddress;
    public static string unitclass;
    public static decimal amountWithdraw;
    public static decimal percentWithdraw;
    public static double withpercent;

    protected void Page_Load(object sender, EventArgs e)
    {

        FolioNo = Session["FolioNo"] as string;
        CorporateCode = Session["VPS_CORPORATE_ID"] as string;
        DataTable tblcorporate = getcorporatedetails(CorporateCode);
        withdrawamount = tblcorporate.Rows[0]["Withdraw_Percentage"].ToString();
        withpercent = Convert.ToDouble(withdrawamount);
        grdData.DataSource = currentBalance(FolioNo);
        grdData.DataBind();
        //GridView1.DataSource = gettransactionhistory();
        //GridView1.DataBind();
        //MakeAccessible(GridView1);
        //DataTable balancecurrent = currentBalance(Session["FolioNo"].ToString());
        //amount = balancecurrent.Rows[0]["AMOUNT"].ToString();
        DateTime currentDate = DateTime.Now;
        int currentYear = currentDate.Year;

        if (currentDate.Month > 9)
        {
            int previousYear = currentYear - 1;
            int yearBeforePrevious = currentYear - 2;
            Year3Taxable.Text = currentYear.ToString();
            Year2Taxable.Text = previousYear.ToString();
            Year1Taxable.Text = yearBeforePrevious.ToString();
        }
        else
        {
            int previousYear = currentYear - 1;
            int yearBeforePrevious = currentYear - 2;
            int yearMorePrevious = currentYear - 3;
            Year3Taxable.Text = previousYear.ToString();
            Year2Taxable.Text = yearBeforePrevious.ToString();
            Year1Taxable.Text = yearMorePrevious.ToString();
        }
        //Year1Taxable.Text = "2021";
        DataTable tbl = getUserDetail(FolioNo);
        if (tbl.Rows.Count > 0)
        {
            fund = tbl.Rows[0]["fund"].ToString();
            bankcode= tbl.Rows[0]["bank_code"].ToString();
            bankname = tbl.Rows[0]["bank_name"].ToString();
            bankaddress  = tbl.Rows[0]["bank_address"].ToString();
            bankcity  = tbl.Rows[0]["bank_city"].ToString();
            bankacctitle = tbl.Rows[0]["bank_acc_title"].ToString();
            accountnumber  = tbl.Rows[0]["account_number"].ToString();
            registeredaddress  = tbl.Rows[0]["registered_address"].ToString();
            unitclass  = tbl.Rows[0]["unit_class"].ToString();
                
        }
        
    }
    [WebMethod]
    public static void SaveData(decimal percent, decimal amount)
    {
        // Handle the received data on the server
        amountWithdraw = amount;
        percentWithdraw = percent;
    }
    private void MakeAccessible(GridView grid)
    {
        if (grid.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            grid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. Remove if you don't have a footer row
            //grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected DataTable getcorporatedetails(string id)
    {
        string qry = @"SELECT * from Corporate_Pension_Account where VPS_CORPORATE_ID=@Employer_id";
        SqlParameter[] p = new SqlParameter[1];
        p[0] = new SqlParameter();
        p[0].ParameterName = "@Employer_id";
        p[0].SqlDbType = SqlDbType.NVarChar;
        p[0].Value = id;
        DataTable tbl = DALSqlServer.ExecuteTable(qry, p);
        return tbl;

    }
    //private DataTable getData(string folioNumber)
    //{
    //    DataTable tbl = new DataTable();
    //    DataTable tblmain = new DataTable();
    //    DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
    //    try
    //    {



    //        //string qry = "select query_text from jsil_queries where id=1";
    //        string qry = "select QUERIES from jsil_investor_queries j where id = 1";



    //        object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

    //        OracleParameter[] oracleParameterArray = new OracleParameter[1];
    //        oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
    //        oracleParameterArray[0].Direction = ParameterDirection.Input;
    //        oracleParameterArray[0].Value = folioNumber;
    //        tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, query.ToString(), oracleParameterArray).Tables[0];


    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //    finally
    //    {
    //        cn.CloseConnection();
    //    }

    //    return tbl;
    //}
    protected DataTable getUserDetail(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            string qry = @"select distinct f.pension_fund_code as fund,
                    u.folio_number as folio_number,
                    u.bank_code,
                    u.bank_name,
                    u.bank_address,
                    u.account_number,
                    u.bank_city,
                    u.bank_acc_title,
                    u.registered_address,
                    u.unit_class
                    from unit_account u
                    inner join unit_balance_electronic ube
                    on u.folio_number = ube.folio_number
                    inner join fund f on f.fund_code=ube.fund_code
                    where u.folio_number = &FOLIO_NUMBER ";


            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
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


    
    private DataTable currentBalance(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"select TO_CHAR(SUM(amount),'999,999,999.99') as AMOUNT from (select distinct
                            get_fund_folio_balance(to_date(sysdate),
                                                   ube.fund_code,
                                                   ube.folio_number)*
                            get_fund_nav(to_date(sysdate),ube.fund_code) as amount,
                            u.folio_number as folio_number
                            from unit_account u
                            inner join unit_balance_electronic ube
                            on u.folio_number = ube.folio_number
                            inner join fund f on f.fund_code=ube.fund_code
                            where u.folio_number = :FOLIO_NUMBER)";
            //object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
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
    protected void grdData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        GridViewRow selectedRow = grdData.Rows[index];
        TextBox txtNotes = (TextBox)selectedRow.FindControl("withdrawUnits");
        string message = "alert('Sorry! " + txtNotes.Text + " ID')";
        ScriptManager.RegisterStartupScript(this as Control, GetType(), "alert", message, true);

    }

    //string message = "alert('Sorry! "+ ((DataTable)Session["tblUser"]).Rows[0]["VPS_CORPORATE_ID"].ToString() +" ID')";
    //ScriptManager.RegisterStartupScript(this as Control, GetType(), "alert", message, true);


    //protected void withdrawUnits_TextChanged(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < grdData.Rows.Count; i++)
    //    {
    //        //TextBox txtShare = (TextBox)grdData.Rows[i].FindControl("withdrawUnits");
    //        GridViewRow selectedRow = grdData.Rows[i];
    //        TextBox txtNotes = (TextBox)selectedRow.FindControl("withdrawUnits");
    //        TextBox txtWithDrwAmount = (TextBox)selectedRow.FindControl("withdrawAmount");
    //        //string message = "alert('Sorry! " + txtNotes.Text + " ID')";
    //        //ScriptManager.RegisterStartupScript(this as Control, GetType(), "alert", message, true);
    //        string NAV = grdData.Rows[i].Cells[2].Text;
    //        //grdData.Rows[i].Cells[1].Text = "";
    //        double NAVNumber = Convert.ToDouble(NAV != "" ? NAV : "0");
    //        double InputNumber = Convert.ToDouble(txtNotes.Text != "" ? txtNotes.Text : "0");
    //        txtWithDrwAmount.Text = (NAVNumber * InputNumber).ToString() != "0" ? (NAVNumber * InputNumber).ToString() : "";
    //    }

    //    //int index = Convert.ToInt32(e.RowIndex);

    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        decimal amountValue = 0;

        DataTable tbl = new DataTable();
        DataTable table = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"SELECT jsil_vps_redemption_id_seq.NEXTVAL FROM DUAL";
            tbl = DAL.OracleDataAccess.ExecuteDataTable(cn.GetConnection(), CommandType.Text, qry.ToString());
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.CloseConnection();
        }
        DAL.clsOracleConnection cn2 = new DAL.clsOracleConnection();
        try
        {
            DataTable tblData = currentBalance(FolioNo);
            string qry = @"select ua.corporate_client_code from unit_account ua where ua.folio_number = :folio_number";
            OracleParameter[] oracleArray = new OracleParameter[1];
            oracleArray[0] = new OracleParameter(":folio_number", OracleType.VarChar);
            oracleArray[0].Direction = ParameterDirection.Input;
            oracleArray[0].Value = FolioNo;
            table = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleArray).Tables[0];
            qry = @"INSERT INTO JSIL_VPS_REDEMPTION_DETAIL (main_redemption_id, redemption_date, folio_number,
                        payment_instruction, bank_code, bank_name, bank_address, account_number,
                        bank_city, log_id, redemption_amount, 
                        transaction_type, year1, year2, year3, taxpaid1,
                        taxpaid2, taxpaid3, taxable1, taxable2, taxable3, post, save, dc_branch_code,
                        branch_code, user_id, pension_fund_code, red_in_percentage, bank_account_title,
                        available_total_balance, registered_address, realize_voucher_no, payment_voucher_no,
                        dc_sub_branch_code, unit_class, red_payment_mode, employer_id) 
                        VALUES (:main_redemption_id, TO_DATE(SYSDATE,'DD/MM/YY'), :folio_number, :payment_instruction,
                        :bank_code, :bank_name, :bank_address, :account_number, :bank_city, '11391946',
                        :redemption_amount, :transaction_type, :year1, :year2, :year3,
                        :taxpaid1, :taxpaid2, :taxpaid3, :taxable1, :taxable2, :taxable3, :post, :save,
                        :dc_branch_code, :branch_code, 'OPSPOST',:pension_fund_code, :red_in_percentage,
                        :bank_account_title, :available_total_balance, :registered_address, :realize_voucher_no, 
                        :payment_voucher_no, :dc_sub_branch_code, :unit_class, :red_payment_mode, :employer_id)";
            //object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[34];
            oracleParameterArray[0] = new OracleParameter(":main_redemption_id", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = tbl.Rows[0]["NEXTVAL"].ToString();
            oracleParameterArray[1] = new OracleParameter(":folio_number", OracleType.VarChar);
            oracleParameterArray[1].Direction = ParameterDirection.Input;
            oracleParameterArray[1].Value = FolioNo;
            oracleParameterArray[2] = new OracleParameter(":payment_instruction", OracleType.VarChar);
            oracleParameterArray[2].Direction = ParameterDirection.Input;
            oracleParameterArray[2].Value = "SB";
            oracleParameterArray[3] = new OracleParameter(":bank_code", OracleType.VarChar);
            oracleParameterArray[3].Direction = ParameterDirection.Input;
            oracleParameterArray[3].Value = bankcode;
            oracleParameterArray[4] = new OracleParameter(":bank_name", OracleType.VarChar);
            oracleParameterArray[4].Direction = ParameterDirection.Input;
            oracleParameterArray[4].Value = bankname;
            oracleParameterArray[5] = new OracleParameter(":bank_address", OracleType.VarChar);
            oracleParameterArray[5].Direction = ParameterDirection.Input;
            oracleParameterArray[5].Value = bankaddress;
            oracleParameterArray[6] = new OracleParameter(":account_number", OracleType.VarChar);
            oracleParameterArray[6].Direction = ParameterDirection.Input;
            oracleParameterArray[6].Value = accountnumber;
            oracleParameterArray[7] = new OracleParameter(":bank_city", OracleType.VarChar);
            oracleParameterArray[7].Direction = ParameterDirection.Input;
            oracleParameterArray[7].Value = bankcity;
            oracleParameterArray[8] = new OracleParameter(":redemption_amount", OracleType.VarChar);
            oracleParameterArray[8].Direction = ParameterDirection.Input;
            oracleParameterArray[8].Value = amountWithdraw;
            oracleParameterArray[9] = new OracleParameter(":transaction_type", OracleType.VarChar);
            oracleParameterArray[9].Direction = ParameterDirection.Input;
            oracleParameterArray[9].Value = "";
            oracleParameterArray[10] = new OracleParameter(":year1", OracleType.VarChar);
            oracleParameterArray[10].Direction = ParameterDirection.Input;
            oracleParameterArray[10].Value = Year1Taxable.Text;
            oracleParameterArray[11] = new OracleParameter(":year2", OracleType.VarChar);
            oracleParameterArray[11].Direction = ParameterDirection.Input;
            oracleParameterArray[11].Value = Year2Taxable.Text;
            oracleParameterArray[12] = new OracleParameter(":year3", OracleType.VarChar);
            oracleParameterArray[12].Direction = ParameterDirection.Input;
            oracleParameterArray[12].Value = Year3Taxable.Text;
            oracleParameterArray[13] = new OracleParameter(":taxpaid1", OracleType.VarChar);
            oracleParameterArray[13].Direction = ParameterDirection.Input;
            oracleParameterArray[13].Value = taxPayable1.Text;
            oracleParameterArray[14] = new OracleParameter(":taxpaid2", OracleType.VarChar);
            oracleParameterArray[14].Direction = ParameterDirection.Input;
            oracleParameterArray[14].Value = taxPayable1.Text;
            oracleParameterArray[15] = new OracleParameter(":taxpaid3", OracleType.VarChar);
            oracleParameterArray[15].Direction = ParameterDirection.Input;
            oracleParameterArray[15].Value = taxPayable1.Text;
            oracleParameterArray[16] = new OracleParameter(":taxable1", OracleType.VarChar);
            oracleParameterArray[16].Direction = ParameterDirection.Input;
            oracleParameterArray[16].Value = totalTaxable1.Text;
            oracleParameterArray[17] = new OracleParameter(":taxable2", OracleType.VarChar);
            oracleParameterArray[17].Direction = ParameterDirection.Input;
            oracleParameterArray[17].Value = totalTaxable1.Text;
            oracleParameterArray[18] = new OracleParameter(":taxable3", OracleType.VarChar);
            oracleParameterArray[18].Direction = ParameterDirection.Input;
            oracleParameterArray[18].Value = totalTaxable1.Text;
            oracleParameterArray[19] = new OracleParameter(":post", OracleType.VarChar);
            oracleParameterArray[19].Direction = ParameterDirection.Input;
            oracleParameterArray[19].Value = "";
            oracleParameterArray[20] = new OracleParameter(":save", OracleType.VarChar);
            oracleParameterArray[20].Direction = ParameterDirection.Input;
            oracleParameterArray[20].Value = 1;
            oracleParameterArray[21] = new OracleParameter(":dc_branch_code", OracleType.VarChar);
            oracleParameterArray[21].Direction = ParameterDirection.Input;
            oracleParameterArray[21].Value = "";
            oracleParameterArray[22] = new OracleParameter(":branch_code", OracleType.VarChar);
            oracleParameterArray[22].Direction = ParameterDirection.Input;
            oracleParameterArray[22].Value = "01";
            oracleParameterArray[23] = new OracleParameter(":pension_fund_code", OracleType.VarChar);
            oracleParameterArray[23].Direction = ParameterDirection.Input;
            oracleParameterArray[23].Value = fund;
            oracleParameterArray[24] = new OracleParameter(":red_in_percentage", OracleType.VarChar);
            oracleParameterArray[24].Direction = ParameterDirection.Input;
            oracleParameterArray[24].Value = percentWithdraw;
            oracleParameterArray[25] = new OracleParameter(":bank_account_title", OracleType.VarChar);
            oracleParameterArray[25].Direction = ParameterDirection.Input;
            oracleParameterArray[25].Value = bankacctitle;
            oracleParameterArray[26] = new OracleParameter(":available_total_balance", OracleType.VarChar);
            oracleParameterArray[26].Direction = ParameterDirection.Input;
            oracleParameterArray[26].Value = "";
            oracleParameterArray[27] = new OracleParameter(":registered_address", OracleType.VarChar);
            oracleParameterArray[27].Direction = ParameterDirection.Input;
            oracleParameterArray[27].Value = registeredaddress;
            oracleParameterArray[28] = new OracleParameter(":realize_voucher_no", OracleType.VarChar);
            oracleParameterArray[28].Direction = ParameterDirection.Input;
            oracleParameterArray[28].Value = "";
            oracleParameterArray[29] = new OracleParameter(":payment_voucher_no", OracleType.VarChar);
            oracleParameterArray[29].Direction = ParameterDirection.Input;
            oracleParameterArray[29].Value = "";
            oracleParameterArray[30] = new OracleParameter(":dc_sub_branch_code", OracleType.VarChar);
            oracleParameterArray[30].Direction = ParameterDirection.Input;
            oracleParameterArray[30].Value = "";
            oracleParameterArray[31] = new OracleParameter(":unit_class", OracleType.VarChar);
            oracleParameterArray[31].Direction = ParameterDirection.Input;
            oracleParameterArray[31].Value = unitclass;
            oracleParameterArray[32] = new OracleParameter(":red_payment_mode", OracleType.VarChar);
            oracleParameterArray[32].Direction = ParameterDirection.Input;
            oracleParameterArray[32].Value = "";
            oracleParameterArray[33] = new OracleParameter(":employer_id", OracleType.VarChar);
            oracleParameterArray[33].Direction = ParameterDirection.Input;
            oracleParameterArray[33].Value = table.Rows[0]["corporate_client_code"].ToString();
            DAL.OracleDataAccess.ExecuteNonQuery(cn2.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.CloseConnection();
        }
        DateTime currentDate = DateTime.Now;
        var currentYear = currentDate.Year;
        var previousYear = 0;
        var yearBeforePrevious = 0;
        var yearMorePrevious = 0;
        if (currentDate.Month > 9)
        {

            previousYear = currentYear - 1;
            yearBeforePrevious = currentYear - 2;
        }
        else
        {
            previousYear = currentYear - 1;
            yearBeforePrevious = currentYear - 2;
            yearMorePrevious = currentYear - 3;
            //Year3Taxable.Text = previousYear.ToString();
            //Year2Taxable.Text = yearBeforePrevious.ToString();
            //Year1Taxable.Text = yearMorePrevious.ToString();
            currentYear = previousYear;
            previousYear = yearBeforePrevious;
            yearBeforePrevious = yearMorePrevious;

        }
        string extcertificate = Path.GetExtension(taxcertificate1.PostedFile.FileName).ToLower();
        string certificatePath = Server.MapPath("~/Documents/" + FolioNo + " " + "Tax Certificate" + " " + currentYear + extcertificate);
        try
        {
            
            if (!String.IsNullOrEmpty(taxcertificate1.PostedFile.FileName))
            {
                taxcertificate1.PostedFile.SaveAs(certificatePath);
            }

            string qry1 = @"INSERT INTO RedemptionDocument (Folio_Number, DocumentFor, DocumentName, Document, Corporate_client_code)
                            SELECT @Folio_Number, @DocumentFor, @FileName, BulkColumn, @Corporate_client_code
                                FROM OPENROWSET(BULK N'" + certificatePath + "', SINGLE_BLOB) AS DocumentContent";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter();
            p[0].ParameterName = "@Folio_Number";
            p[0].SqlDbType = SqlDbType.NVarChar;
            p[0].Value = Session["FolioNo"].ToString();

            p[1] = new SqlParameter();
            p[1].ParameterName = "@DocumentFor";
            p[1].SqlDbType = SqlDbType.NVarChar;
            p[1].Value = "Tax Certificate " + currentYear.ToString() + extcertificate;

            p[2] = new SqlParameter();
            p[2].ParameterName = "@FileName";
            p[2].SqlDbType = SqlDbType.NVarChar;
            p[2].Value = taxcertificate1.PostedFile.FileName;

            p[3] = new SqlParameter();
            p[3].ParameterName = "@Corporate_client_code";
            p[3].SqlDbType = SqlDbType.NVarChar;
            p[3].Value = table.Rows[0]["corporate_client_code"].ToString();

            DALSqlServer.Execute(qry1, p);

        }
        catch(Exception ex)
        {

        }

        try 
        {
            extcertificate = Path.GetExtension(taxcertificate2.PostedFile.FileName).ToLower();
            certificatePath = Server.MapPath("~/Documents/" + FolioNo + " " + "Tax Certificate" + " " + previousYear + extcertificate);
            if (!String.IsNullOrEmpty(taxcertificate2.PostedFile.FileName))
            {
                taxcertificate2.PostedFile.SaveAs(certificatePath);
            }

            string qry2 = @"INSERT INTO RedemptionDocument (Folio_Number, DocumentFor, DocumentName, Document, Corporate_client_code)
                            SELECT @Folio_Number, @DocumentFor, @FileName, BulkColumn, @Corporate_client_code
                                FROM OPENROWSET(BULK N'" + certificatePath + "', SINGLE_BLOB) AS DocumentContent";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter();
            p[0].ParameterName = "@Folio_Number";
            p[0].SqlDbType = SqlDbType.NVarChar;
            p[0].Value = Session["FolioNo"].ToString();

            p[1] = new SqlParameter();
            p[1].ParameterName = "@DocumentFor";
            p[1].SqlDbType = SqlDbType.NVarChar;
            p[1].Value = "Tax Certificate " + previousYear.ToString() + extcertificate;

            p[2] = new SqlParameter();
            p[2].ParameterName = "@FileName";
            p[2].SqlDbType = SqlDbType.NVarChar;
            p[2].Value = taxcertificate2.PostedFile.FileName;

            p[3] = new SqlParameter();
            p[3].ParameterName = "@Corporate_client_code";
            p[3].SqlDbType = SqlDbType.NVarChar;
            p[3].Value = table.Rows[0]["corporate_client_code"].ToString();

            DALSqlServer.Execute(qry2, p);
        }
        catch(Exception ex)
        {

        }
        try
        {
            extcertificate = Path.GetExtension(taxcertificate3.PostedFile.FileName).ToLower();
            certificatePath = Server.MapPath("~/Documents/" + FolioNo + " " + "Tax Certificate" + " " + yearBeforePrevious + extcertificate);
            if (!String.IsNullOrEmpty(taxcertificate3.PostedFile.FileName))
            {
                taxcertificate3.PostedFile.SaveAs(certificatePath);
            }

            string qry3 = @"INSERT INTO RedemptionDocument (Folio_Number, DocumentFor, DocumentName, Document, Corporate_client_code)
                            SELECT @Folio_Number, @DocumentFor, @FileName, BulkColumn, @Corporate_client_code
                                FROM OPENROWSET(BULK N'" + certificatePath + "', SINGLE_BLOB) AS DocumentContent";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter();
            p[0].ParameterName = "@Folio_Number";
            p[0].SqlDbType = SqlDbType.NVarChar;
            p[0].Value = Session["FolioNo"].ToString();

            p[1] = new SqlParameter();
            p[1].ParameterName = "@DocumentFor";
            p[1].SqlDbType = SqlDbType.NVarChar;
            p[1].Value = "Tax Certificate " + yearBeforePrevious.ToString() + extcertificate;

            p[2] = new SqlParameter();
            p[2].ParameterName = "@FileName";
            p[2].SqlDbType = SqlDbType.NVarChar;
            p[2].Value = taxcertificate3.PostedFile.FileName;

            p[3] = new SqlParameter();
            p[3].ParameterName = "@Corporate_client_code";
            p[3].SqlDbType = SqlDbType.NVarChar;
            p[3].Value = table.Rows[0]["corporate_client_code"].ToString();

            DALSqlServer.Execute(qry3, p);

            string qryred = "select * from ParticipantContributionDetails where VPS_CORPORATE_ID=@VPS_CORPORATE_ID";
            SqlParameter[] p5 = new SqlParameter[1];
            p5[0] = new SqlParameter();
            p5[0].ParameterName = "@VPS_CORPORATE_ID";
            p5[0].SqlDbType = SqlDbType.NVarChar;
            p5[0].Value = Session["VPS_CORPORATE_ID"].ToString();

            DataTable tblred = DALSqlServer.ExecuteTable(qryred, p5);
            if (tblred.Rows.Count > 0)
            {
                if (Session["VPS_CORPORATE_ID"].ToString() == "11")
                {
                    string htmlfile = File.OpenText(HttpContext.Current.Server.MapPath("index2.html")).ReadToEnd();

                    htmlfile = htmlfile.Replace("##Date##", DateTime.Now.ToString("dd/MM/yyyy"));
                    htmlfile = htmlfile.Replace("##Name##", Session["TITLE"].ToString());
                    htmlfile = htmlfile.Replace("##Folio##", Session["FolioNo"].ToString());
                    htmlfile = htmlfile.Replace("##Fund##", tblred.Rows[0]["PENSION_SAVING_FUND"].ToString());
                    htmlfile = htmlfile.Replace("##Amount##", amountValue.ToString());

                    SendSMS.SendEmail("nouman.khan@jsil.com", "JS Investments - VPS Redemption Request", htmlfile, "");
                }
                else
                {
                    string htmlfile = File.OpenText(HttpContext.Current.Server.MapPath("index2.html")).ReadToEnd();

                    htmlfile = htmlfile.Replace("##Name##", Session["TITLE"].ToString());
                    htmlfile = htmlfile.Replace("##Date##", DateTime.Now.ToString("dd/MM/yyyy"));
                    htmlfile = htmlfile.Replace("##Folio##", Session["FolioNo"].ToString());
                    htmlfile = htmlfile.Replace("##Fund##", tblred.Rows[0]["PENSION_SAVING_FUND"].ToString());
                    htmlfile = htmlfile.Replace("##Amount##", amountValue.ToString());

                    SendSMS.SendEmail("nouman.khan@jsil.com", "JS Investments - VPS Redemption Request", htmlfile, "");
                }
            }

            Response.Redirect("Default.aspx");
        }

        catch(Exception ex)
        {

        }



        //string qry = @"SELECT jsil_vps_redemption_id_seq.NEXTVAL FROM DUAL";
        //OracleCommand cmd = new OracleCommand(qry);
        //cmd.Connection = new (System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);






        //string emailbody = @"
        //                        <html>
        //                            <body>
        //                                <h2>Request For Withdrawal</h2>
        //                                <table>
        //                                    <tr>
        //                                        <td>Folio No:</td>
        //                                        <td>" + FolioNo + @"</td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>Withdrawal Units:</td>
        //                                        <td>" + notes + @"</td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>Withdrawal Amount:</td>
        //                                        <td>" + amountunit + @"</td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>Withdrawal Date:</td>
        //                                        <td>" + DateTime.Now.ToString("dd-MMMM-yyyy") + @"</td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>Nav Amount:</td>
        //                                        <td>" + NAV.ToString() + @"</td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>Remaining Units:</td>
        //                                        <td>" + Available_Units.ToString() + @"</td>
        //                                    </tr>
        //                                     <tr>
        //                                        <td>Fund:</td>
        //                                        <td>" + fundname + @"</td>
        //                                    </tr>
        //                                </table>
        //                            </body>
        //                        </html>
        //                                ";

        //try
        //{
        //    string query = @"insert into JSIL_VPS_REDEMPTION_DETAIL
        //                        (FOLIO_NUMBER,
        //                         NAV,
        //                         AMOUNT,
        //                         UNITS,
        //                         REMAINING_UNITS,
        //                         REDEMPTION_DATE,
        //                         FUND_NAME)
        //                      VALUES
        //                        ('" + FolioNo + "',  '" + NAV + "', '" + InputNumber + "', '" + withdrawalUnits + "', '" + (Available_Units - withdrawalUnits) + "', SYSDATE, '" + fundname + "')";

        //    OracleCommand cmd = new OracleCommand(query);
        //    cmd.Connection = new (System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);


        //    cmd.Connection.Open();
        //    recordinsertId = cmd.ExecuteNonQuery();
        //    if (recordinsertId > 0)
        //    {
        //        GridView1.DataSource = gettransactionhistory();
        //        GridView1.DataBind();
        //        MakeAccessible(GridView1);
        //    }
        //    cmd.Connection.Close();
        //}

        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //SendSMS.SendEmail("nouman.khan@jsil.com", "Units With Drawal", emailbody, "tuaha.rasool@jsil.com");
        ////txtNotes.Text = (InputNumber / NAVNumber).ToString() != "0" ? (InputNumber / NAVNumber).ToString() : "";

    }

    
    //public DataTable gettransactionhistory()
    //{

    //    DataTable tbl = new DataTable();
    //    DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
    //    try
    //    {
    //        //making user ,jvrd.APPROVED_DATE
    //        string qry = "select jvrd.fund_name, jvrd.nav, jvrd.amount,jvrd.units,jvrd.remaining_units,jvrd.redemption_date,jvrd.status from JSIL_VPS_REDEMPTION_DETAIL jvrd where jvrd.folio_number = :FOLIO_NUMBER";
    //        OracleParameter[] oracleParameterArray = new OracleParameter[1];
    //        oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
    //        oracleParameterArray[0].Direction = ParameterDirection.Input;
    //        oracleParameterArray[0].Value = FolioNo;

    //        tbl = DAL.OracleDataAccess.ExecuteDataTable(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray);
    //        if (tbl.Rows.Count > 0)
    //        {
    //            return tbl;
    //        }
    //        return tbl;
    //    }

    //    finally
    //    {
    //        cn.CloseConnection();
    //    }
    //}


    //protected void withdrawAmount_TextChanged(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < grdData.Rows.Count; i++)
    //    {
    //        //TextBox txtShare = (TextBox)grdData.Rows[i].FindControl("withdrawUnits");
    //        GridViewRow selectedRow = grdData.Rows[i];
    //        TextBox txtNotes = (TextBox)selectedRow.FindControl("withdrawUnits");
    //        TextBox txtWithDrwAmount = (TextBox)selectedRow.FindControl("withdrawAmount");
    //        //string message = "alert('Sorry! " + txtNotes.Text + " ID')";
    //        //ScriptManager.RegisterStartupScript(this as Control, GetType(), "alert", message, true);
    //        string NAV = grdData.Rows[i].Cells[2].Text;
    //        //double Units =Convert.ToDouble(grdData.Rows[i].Cells[2].Text);
    //        //grdData.Rows[i].Cells[1].Text = "";
    //        double NAVNumber = Convert.ToDouble(NAV != "" ? NAV : "0");rashid js 2
    //        double InputNumber = Convert.ToDouble(txtWithDrwAmount.Text != "" ? txtWithDrwAmount.Text : "0");

    //        //if (Units == 0 || Units < 1)
    //        //{
    //        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Withdrawal Failed", "alert('Sorry you dont have enough Units to Withdraw!')", true);
    //        //    return;
    //        //}
    //        txtNotes.Text = (InputNumber / NAVNumber).ToString() != "0" ? (InputNumber / NAVNumber).ToString() : "";
    //    }
    //}
}