using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComplaintHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.PreRender += new EventHandler(GridView1_PreRender);
        GridView1.DataSource = getData();
        GridView1.DataBind();
    }


    void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    private DataTable getData()
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            string qry = @"SELECT COMPLAINT_ID, COMPLAINT_DATE, PROBLEM_TYPE, FOLIO_NUMBER, FUND_CODE, RESPONSIBLE_CSR, RESPONSIBLE_EMP, RESPONSIBLE_TRUSTEE, RESPONSIBLE_TA, CONTACT_PERSON, 
                    CONTACT_PHONE, CONTACT_PHONE2, COMPLAINT_METHOD, STATUS_ID, PROBLEM_RESOLUTION, POST, LOG_ID, STATUS_DATE, PROBLEM_DESCRIPTION
                    FROM Complaint";

           
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry).Tables[0];


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

    private DataTable getData2()
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        
        try
        {



                    string qry = @"SELECT COMPLAINT_ID, COMPLAINT_DATE, PROBLEM_TYPE, FOLIO_NUMBER, FUND_CODE, RESPONSIBLE_CSR, RESPONSIBLE_EMP, RESPONSIBLE_TRUSTEE, RESPONSIBLE_TA, CONTACT_PERSON, 
                    CONTACT_PHONE, CONTACT_PHONE2, COMPLAINT_METHOD, STATUS_ID, PROBLEM_RESOLUTION, POST, LOG_ID, STATUS_DATE, PROBLEM_DESCRIPTION
                    FROM Complaints";


            tbl = DALSqlServer.ExecuteTable(qry);


           

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
           
        }

        return tbl;
    }

    
}