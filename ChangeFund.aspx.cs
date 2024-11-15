using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangeFund : System.Web.UI.Page
{
    public static string fundcode;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        DataTable tbl = getData(Session["FolioNo"].ToString());
        if (tbl.Rows.Count > 0)
        {
              var amount =  tbl.Rows[0]["Amount"].ToString();
            TxtBalance.Text = !string.IsNullOrEmpty(amount) ? Convert.ToDecimal(tbl.Rows[0]["Amount"]).ToString("N3") : "0.00";
        }
        else
        {
            TxtBalance.Text = "0";
        }
	GridView2.DataSource = getData(Session["FolioNo"].ToString());
        GridView2.DataBind();
        
    }
    protected void GridView2_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check if the current item is a data row (not a header or footer row)
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the AMOUNT field value
            object amountValue = DataBinder.Eval(e.Row.DataItem, "AMOUNT");

            if (amountValue != null)
            {
                // Convert the value to decimal and format it without commas
                decimal amount = Convert.ToDecimal(amountValue);
                e.Row.Cells[2].Text = amount.ToString("N3"); // Modify this format as needed
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string title;
        DataTable tblfund = getfund(Session["FolioNo"].ToString());
        if (tblfund.Rows.Count > 0)
        {
            fundcode = tblfund.Rows[0]["Pension_fund_code"].ToString();
        }
        if (fundcode == ddlFundType.SelectedValue)
        {
            string script = "alert('Same Fund'); window.location='ChangeFund.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript", script, true);
        }
        else
        {
            if (ddlFundType.SelectedValue == "00001")
            {
                title = "CDC-TRUSTEE JS PENSION SAVINGS FUND";
            }
            else
            {
                title = "CDC-TRUSTEE JS ISLAMIC PENSION SAVINGS FUND";
            }
            DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
            try
            {
                object query = @"Insert into vps_redemption(MAIN_REDEMPTION_ID,FOLIO_NUMBER,REDEMPTION_DATE,REDEMPTION_AMOUNT,RED_IN_PERCENTAGE,TRANSACTION_TYPE,BANK_ACCOUNT_TITLE,PENSION_FUND_CODE,SAVE) values
                             (vps_utility.vps_get_redemption_id,:FOLIO_NUMBER,SYSDATE,:REDEMPTION_AMOUNT,'100','5',:BANK_ACCOUNT_TITLE,:PENSION_FUND_CODE,'1')";

                OracleParameter[] oracleParameterArray = new OracleParameter[4];
                oracleParameterArray[0] = new OracleParameter(":FOLIO_NUMBER", OracleType.VarChar);
                oracleParameterArray[0].Direction = ParameterDirection.Input;
                oracleParameterArray[0].Value = Session["FolioNo"].ToString();
                oracleParameterArray[1] = new OracleParameter(":REDEMPTION_AMOUNT", OracleType.Double);
                oracleParameterArray[1].Direction = ParameterDirection.Input;
                oracleParameterArray[1].Value = Convert.ToDouble(TxtBalance.Text);
                oracleParameterArray[2] = new OracleParameter(":BANK_ACCOUNT_TITLE", OracleType.VarChar);
                oracleParameterArray[2].Direction = ParameterDirection.Input;
                oracleParameterArray[2].Value = title;
                oracleParameterArray[3] = new OracleParameter(":PENSION_FUND_CODE", OracleType.VarChar);
                oracleParameterArray[3].Direction = ParameterDirection.Input;
                oracleParameterArray[3].Value = ddlFundType.SelectedValue;
                int result = DAL.OracleDataAccess.ExecuteNonQuery(cn.GetConnection(), CommandType.Text, query.ToString(), 			     oracleParameterArray);
                if (result > 0)
                {
                    string script = "alert('Request Submitted'); window.location='Default.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript2", script, true);
                }
                else
                {
                    string script = "alert('Something Went Wrong'); window.location='ChangeFund.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript3", script, true);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                string script = "alert('Something Went Wrong'); window.location='ChangeFund.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript", script, true);
            }
        }


    }
    private void LogError(Exception ex)
    {
        string logFilePath = Server.MapPath("ErrorsLog.txt"); // Adjust path as needed
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            
            writer.WriteLine(ex.StackTrace);
            writer.WriteLine(); // Blank line for separation
        }
    }
    private DataTable getfund(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            //string qry = "select query_text from jsil_queries where id=1";
            string qry = "select * from vps_fund_folio_conf where folio_number = :folio_number";

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":folio_number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnectionTest(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];


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
    private DataTable getData(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            //string qry = "select query_text from jsil_queries where id=1";
            string qry = "select QUERIES from jsil_investor_queries j where id = 1";



            object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnectionTest(), CommandType.Text, query.ToString(), oracleParameterArray).Tables[0];
           tbl.Columns.Add("FundType", typeof(string));

            // Iterate through the rows and map the fund values
            foreach (DataRow row in tbl.Rows)
            {
                string fundValue = row["FUND_NAME"].ToString();

                // Map the fund value to the appropriate type
                if (fundValue == "JSIPSF-DSF" || fundValue == "JS PSF-DSF")
                {
                    row["FundType"] = "Debt";
                }
                else if (fundValue == "JSIPSF-MSF" || fundValue == "JS PSF-MSF")
                {
                    row["FundType"] = "Money Market";
                }
                else if (fundValue == "JSIPSF-ESF" || fundValue == "JS PSF-ESF")
                {
                    row["FundType"] = "Equity";
                }
                else
                {
                    row["FundType"] = "unknown"; // Handle any other cases as needed
                }

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

        return tbl;
    }
}