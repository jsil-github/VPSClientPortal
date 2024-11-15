using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uploadReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
	    if (Session["USERID"] == null)
            {
                Response.Redirect("Signout.aspx");
            }
	    if (Session["USERID"] != "SMAPORTAL")
            {
                Response.Redirect("Default.aspx");
            }
        if(!IsPostBack)
        {
            ddlFundName.DataSource = getData();
            ddlFundName.DataTextField = "fund_name";
            ddlFundName.DataValueField = "fund_code";
            ddlFundName.DataBind();
            ddlFundName.Items.Insert(0, new ListItem("Select Fund", "-1"));
            TableReports();
            //ReportsTable.PreRender += new EventHandler(ReportsTable_PreRender);
            //ReportsTable.DataSource = TableReports();
            ////allocationDetail.DataBind();
            //ReportsTable.DataBind();
        }
    }


    private string ProcessUploadedFile()
    {
        string res = "";
        if (!FileUpload1.HasFile)
            res = "You must select a valid file to upload.";

        if (FileUpload1.FileContent.Length == 0)
            res = "You must select a non empty file to upload.";

        //As the input is external, always do case-insensitive comparison unless you actually care about the case.
        if (FileUpload1.PostedFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
        {
            if (FileUpload1.PostedFile.ContentLength > 0)
            {
                string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (ext == ".pdf")
                {
                    
                    string Filename = FileUpload1.FileName;
                    string FilePath = Server.MapPath("reports") + "\\" + Filename;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("reports") + "\\" + FileUpload1.FileName.Trim().Replace(" ", "_"));
                    save(Filename.Trim().Replace(".pdf",""), txtMonthName.Text, FileUpload1.FileName.Trim().Replace(" ", "_"), ddlReportType.SelectedItem.Text,ddlFundName.SelectedValue);
                    res  = "File Uploaded Successfully...";
                    ddlReportType.SelectedValue = "-1";
                    txtMonthName.Text = "";
                    ddlFundName.SelectedValue = "-1";
                    //txtReportName.Text = "";
                }

            }
        }
        else
        {
            res = "Only PDF files are supported. Uploaded File Type: " + FileUpload1.PostedFile.ContentType;
        }



        //rest of the code to actually process file.

        return res;
    }

    private void save(string REPORT_NAME, string MONTH_NAME, string PDF_LINK, string REPORT_TYPE,string FUND_NAME)
    {
       
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            string qry = @"select nvl(MAX(t.id),0) + 1 from SMA_REPORTS t";
            string maxval = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry).ToString();

            qry = "insert into SMA_REPORTS (ID, REPORT_NAME, MONTH_NAME, PDF_LINK, REPORT_TYPE,FUND_NAME) values (:ID, :REPORT_NAME, :MONTH_NAME, :PDF_LINK, :REPORT_TYPE,:FUND_NAME)";
           
            OracleParameter[] oracleParameterArray = new OracleParameter[6];
            oracleParameterArray[0] = new OracleParameter(":ID", (object)maxval);
            oracleParameterArray[0].Direction = ParameterDirection.Input;

            oracleParameterArray[1] = new OracleParameter(":REPORT_NAME", (object)REPORT_NAME);
            oracleParameterArray[1].Direction = ParameterDirection.Input;

            oracleParameterArray[2] = new OracleParameter(":MONTH_NAME", (object)MONTH_NAME);
            oracleParameterArray[2].Direction = ParameterDirection.Input;

            oracleParameterArray[3] = new OracleParameter(":PDF_LINK", (object)PDF_LINK);
            oracleParameterArray[3].Direction = ParameterDirection.Input;

            oracleParameterArray[4] = new OracleParameter(":REPORT_TYPE", (object)REPORT_TYPE);
            oracleParameterArray[4].Direction = ParameterDirection.Input;

            oracleParameterArray[5] = new OracleParameter(":FUND_NAME", (object)FUND_NAME);
            oracleParameterArray[5].Direction = ParameterDirection.Input;

            DAL.OracleDataAccess.ExecuteNonQuery(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray);


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

    private DataTable getData()
    {
        DataTable tbl = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            string qry = @"select t.fund_code,t.fund_name from FUND t order by t.fund_name";
           

           tbl = DAL.OracleDataAccess.ExecuteDataTable(cn.GetConnection(), CommandType.Text, qry);


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
        lblResult.Text = ProcessUploadedFile();
    }

    private DataTable TableReports()
    {
        DataTable tbl = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            string qry = @"Select * from SMA_REPORTS ORDER BY ID";
            OracleDataAdapter adap = new OracleDataAdapter(qry, cn.GetConnection());
            adap.Fill(tbl);
            ReportsTable.DataSource = tbl;
            ReportsTable.DataBind();


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

    private string DeleteReportFunc(string ID)
    {

        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        string qry = @"delete from SMA_REPORTS where ID = :ID";
        OracleCommand cmd = new OracleCommand(qry, cn.GetConnection());
        cmd.Parameters.AddWithValue("ID", ID);
        cmd.ExecuteNonQuery();
        return "Report has been Deleted";
    }



    protected void DeleteReport_Click(object sender, EventArgs e)
    {

        ReportsLabel.Text = DeleteReportFunc(REPORTID.Text);
        REPORTID.Text = "";
        
    }
    private void rep_bind()
    {
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        
        string query = "select * from Sma_Reports where REPORT_NAME like '%" + char.ToUpper(TextBox1.Text[0]) + TextBox1.Text.Substring(1) + "%' order by ID";
        OracleDataAdapter da = new OracleDataAdapter(query, cn.GetConnection());
        DataTable ds = new DataTable();
        da.Fill(ds);
        ReportsTable.DataSource = ds;
        ReportsTable.DataBind();
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            rep_bind();
        }
        else
        {
            TableReports();
        }
    }
}