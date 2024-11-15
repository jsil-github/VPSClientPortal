using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangeAllocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       GridView2.DataSource = getData(Session["FolioNo"].ToString());
        GridView2.DataBind();
    }


         private DataTable getData(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = "select QUERIES from jsil_investor_queries j where id = 1";
            object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, query.ToString(), oracleParameterArray).Tables[0];
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int total = Convert.ToInt32(totalTaxable1.Text) + Convert.ToInt32(totalTaxable2.Text) + Convert.ToInt32(totalTaxable3.Text);
        if (total != 100)
        {
            string script = "alert('Allocation Should be 100%');";
            ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript", script, true);
       
        
        }


        else
        {
            string folio = Session["FolioNo"].ToString();
            string participantid=Session["ID"].ToString();
            string qry = "select p.Pension_saving_fund,p.Scheme_code_erp,a.DEBIT,a.EQUITY,a.MONEY_MARKET from ParticipantContributionDetails p inner join CORPORATE_FUND_ALLOCATION_PARTICIPANT a on p.Participant_ID = a.Participant_ID where p.participant_id =@participantid";
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter();
            p[0].ParameterName = "@participantid";
            p[0].SqlDbType = SqlDbType.NVarChar;
            p[0].Value = participantid;
            DataTable tbl = DALSqlServer.ExecuteTable(qry, p);
            if (tbl.Rows.Count > 0)
            {
                string schemecode = tbl.Rows[0]["Scheme_code_erp"].ToString();
                string fundcode = tbl.Rows[0]["Pension_saving_fund"].ToString();
                string Money_Market= totalTaxable2.Text.ToString();
                string Equity = totalTaxable1.Text.ToString();
                string Debt = totalTaxable3.Text.ToString();
                string schemecodeM;
                string schemecodeE;
                string schemecodeD;
                if (fundcode == "00001")
                {
                    schemecodeE = "00030";
                    schemecodeD = "00031";
                    schemecodeM = "00032";
                     

                }
                else
                {
                     schemecodeE = "00036";
                     schemecodeD = "00037";
                     schemecodeM = "00038";
                }
                
                using (OracleConnection connection = new OracleConnection (System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    // Open the database connection
                    connection.Open();

                    // Create a command object for your stored procedure
                    OracleCommand command = new OracleCommand("InsertFundAndScheme", connection);

                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add any parameters that your stored procedure requires
                    command.Parameters.Add("p_pension_fund_code", OracleType.NVarChar).Value = fundcode;
                    command.Parameters.Add("p_scheme_code", OracleType.NVarChar).Value = schemecode;
                    command.Parameters.Add("p_folio_number", OracleType.NVarChar).Value = folio;
                    command.Parameters.Add("p_percentage_allocation1", OracleType.Number).Value = Convert.ToInt32(Money_Market);
                    command.Parameters.Add("p_percentage_allocation2", OracleType.Number).Value = Convert.ToInt32(Equity);
                    command.Parameters.Add("p_percentage_allocation3", OracleType.Number).Value = Convert.ToInt32(Debt);
                    command.Parameters.Add("p_schemecode1", OracleType.NVarChar).Value = schemecodeM;
                    command.Parameters.Add("p_schemecode2", OracleType.NVarChar).Value = schemecodeE;
                    command.Parameters.Add("p_schemecode3", OracleType.NVarChar).Value = schemecodeD;


                    // Execute the stored procedure
                    int result=command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string script = "alert('Your Allocation Change Request Sent'); window.location='Default.aspx';";
                        ClientScript.RegisterStartupScript(this.GetType(), "PasswordUpdatedScript", script, true);
                    }


                }

            }


        }
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
}