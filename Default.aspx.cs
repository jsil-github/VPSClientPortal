using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultPage : System.Web.UI.Page
{
    public static string amount;
    public static decimal[] balanceArray;
    public static string balanceArray1;
    public static string balanceArray2;
    public static string balanceArray3;
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnAggrementSubmit.Attributes.Add("onclick", "javascript: " + btnAggrementSubmit.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnAggrementSubmit, ""));
        //btnFMRSubmit.Attributes.Add("onclick", "javascript: " + btnFMRSubmit.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnFMRSubmit, ""));
        //btnAccountStatementReport.Attributes.Add("onclick", "javascript: " + btnAccountStatementReport.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnAccountStatementReport, ""));
        //btnTransStatReport.Attributes.Add("onclick", "javascript: " + btnTransStatReport.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnTransStatReport, ""));
        if (Session["FolioNo"] == null)
        {
            Response.Redirect("signout.aspx");
        }
        //Session["FUNDCODE"] = "00096";
        //if (Session["USERID"] == "SMAPORTAL")
        //{
        //    Response.Redirect("uploadReports.aspx");
        //}
        //Session["FolioNo"] = "42301-6571918-4-PF";
        //Session["TITLE"] = "Mrs SAMINA FAISAL";
        if (!IsPostBack)
        {
            grdData.DataSource = getData(Session["FolioNo"].ToString());
            grdData.DataBind();
            TransactionView.DataSource = TransactionSummary(Session["FolioNo"].ToString());
            TransactionView.DataBind();//first card
            //DataTable CurrentBalancetbl = currentBalance(Session["FolioNo"].ToString());
            //if(CurrentBalancetbl.Rows.Count > 0)
            //{
                //string amountString = CurrentBalancetbl.Rows[0]["AMOUNT"].ToString();
                //amount = amountString;
            //}
            
            //DataTable InvestedAmounttbl = getInvestmentAmount(Session["FolioNo"].ToString());
            //if(InvestedAmounttbl.Rows.Count > 0)
            //{
            //    string amountString = InvestedAmounttbl.Rows[0]["AMOUNT"].ToString();
            //    decimal amount1;
            //    if (decimal.TryParse(amountString, out amount1))
            //    {
            //        amount1 = decimal.Round(amount1, 2, MidpointRounding.AwayFromZero);
            //        amount1 = Math.Max(amount1, 0); // Ensure non-negative value if needed
            //        amount1 = Math.Min(amount1, 999999999.99m); // Limit the maximum value if needed
            //        string formattedAmount = amount1.ToString("#,##0.00");
            //        // Use the formattedAmount as needed
                    
            //    }
            //    amount = amount1.ToString("#,##0.00");
            //    amount = decimal.Round(Convert.ToDecimal(InvestedAmounttbl.Rows[0]["AMOUNT"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00");
            //}
            DataTable balancecurrent = currentBalance(Session["FolioNo"].ToString());
            amount = balancecurrent.Rows[0]["AMOUNT"].ToString();
            getPortfolioRet2(Session["FolioNo"].ToString());//second card
            PortfolioAllocation(Session["FolioNo"].ToString());

            TransactionView.PreRender += new EventHandler(TransactionView_PreRender);

            ////Reportlink();
            //allocationDetail.PreRender += new EventHandler(allocationDetail_PreRender);
            //allocationDetail.DataSource= portfolioAllocationDetail(Session["FUNDCODE"].ToString());
            //allocationDetail.DataBind();
            //profiledetail(Session["FUNDCODE"].ToString());

            //ddlFMRList.DataSource = getReportData("select t.month_name,t.pdf_link from SMA_REPORTS t where t.fund_name ='" + Session["FUNDCODE"].ToString() + "' and t.report_type ='FMR' ORDER BY ID");
            //ddlFMRList.DataTextField = "MONTH_NAME";
            //ddlFMRList.DataValueField = "PDF_LINK";
            //ddlFMRList.DataBind();

            //ddlAgreement.DataSource = getReportData("select t.month_name,t.pdf_link from SMA_REPORTS t where t.fund_name ='" + Session["FUNDCODE"].ToString() + "' and t.report_type ='Agreement'");
            //ddlAgreement.DataTextField = "MONTH_NAME";
            //ddlAgreement.DataValueField = "PDF_LINK";
            //ddlAgreement.DataBind();
        }
    }

    //private DataTable getInvestmentAmount(string folioNumber)
    //{
    //    DataTable tbl = new DataTable();
    //    DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
    //    try
    //    {
    //        string qry = "select nvl(sum(j.amount),0) as AMOUNT from jsil_fund_folio_balance j where FOLIO_NUMBER = :FOLIO_NUMBER";

    //        OracleParameter[] oracleParameterArray = new OracleParameter[1];
    //        oracleParameterArray[0] = new OracleParameter("FOLIO_NUMBER", OracleType.VarChar);
    //        oracleParameterArray[0].Direction = ParameterDirection.Input;
    //        oracleParameterArray[0].Value = folioNumber;

    //        tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray).Tables[0];
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle the exception appropriately for your scenario
    //        Response.Write(ex.Message);
    //    }
    //    finally
    //    {
    //        cn.CloseConnection();
    //    }

    //    return tbl;
    //}


    //private DataTable getInvestmentAmount(string folioNumber)
    //{
    //    DataTable tbl = new DataTable();
    //    DataTable tblmain = new DataTable();
    //    DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
    //    try
    //    {



    //        //string qry = "select query_text from jsil_queries where id=1";
    //        string qry = "select QUERIES from jsil_investor_queries j where id = 4";



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

    private DataTable TransactionSummary(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            //string qry = "select query_text from jsil_queries where id=1";
            string qry = "select QUERIES from jsil_investor_queries j where id = 3";



            object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, query.ToString(), oracleParameterArray).Tables[0];


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
    private DataTable getReportData(string qry)
    {
        DataTable tbl = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

           
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string profiledetailJson(string type)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        string json = "";
        try
        {


            tbl = tblProfiledetailJson(Session["FUNDCODE"].ToString(), "");
            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            List<getdata> data = new List<getdata>();

            amoutdata getdata1 = new amoutdata();
            List<decimal> items2 = new List<decimal>();
            List<getdata> items = new List<getdata>();
            List<amoutdata> dd = new List<amoutdata>();

            var chartData = new object[tbl.Rows.Count + 1];
            List<decimal> lst_dataItem_1 = new List<decimal>();
            List<decimal> lst_dataItem_2 = new List<decimal>();
            int j = 0;
           
            string[] colr = { "#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990" };

            for (int i = 0; i < tbl.Rows.Count; i++)
            {

                dd.Add(new amoutdata
                {
                    y =
                    decimal.Round(Convert.ToDecimal(tbl.Rows[i]["COST"].ToString()), 2, MidpointRounding.AwayFromZero),
                    color = colr[i],
                    name = tbl.Rows[i]["SCRIPT"].ToString(),
                    key = tbl.Rows[i]["'FIS'"].ToString(),


                });



            }

            //items.Add(new getdata
            //{


            //    data = dd,
            //    //name = labels

            //});
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            json = serializer.Serialize(dd);

            // ChartLable = serializer.Serialize(labels);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {

        }
        return json;

    }
    protected void Reportlink()
    {
        DataTable tbl = new DataTable("");
        tbl.Columns.Add("Month");
        tbl.Columns.Add("text");
        tbl.Columns.Add("Link");

        if(Session["USERID"].ToString()== "KMANSOOR20")
        {

        DataRow dtr = tbl.NewRow();
        dtr["Month"] = "31-Dec-2021";
        dtr["text"] = "Download";
        dtr["Link"] = "KhalidMansoor_31-Dec-21.pdf";
        tbl.Rows.Add(dtr);

         dtr = tbl.NewRow();
        dtr["Month"] = "31-Jan-2022";
        dtr["text"] = "Download";
        dtr["Link"] = "KhalidMansoor_31-Jan-22.pdf";
        tbl.Rows.Add(dtr);

         dtr = tbl.NewRow();
        dtr["Month"] = "28-Feb-2022";
        dtr["text"] = "Download";
        dtr["Link"] = "KhalidMansoor_28-Feb-22.pdf";
        tbl.Rows.Add(dtr);
        
        }
        if (Session["USERID"].ToString() == "FNSBHANDARA18")
        {
            DataRow dtr = tbl.NewRow();
            dtr["Month"] = "31-Dec-2021";
            dtr["text"] = "Download";
            dtr["Link"] = "Feroze_and_Shernaz_Bhandara_Charitable_Trust_31-Dec-21.pdf";
            tbl.Rows.Add(dtr);

            dtr = tbl.NewRow();
            dtr["Month"] = "31-Jan-2022";
            dtr["text"] = "Download";
            dtr["Link"] = "Feroze_and_Shernaz_Bhandara_Charitable_Trust_31-Jan-22.pdf";
            tbl.Rows.Add(dtr);

            dtr = tbl.NewRow();
            dtr["Month"] = "28-Feb-2022";
            dtr["text"] = "Download";
            dtr["Link"] = "Feroze_and_Shernaz_Bhandara_Charitable_Trust_28-Feb-22.pdf";
            tbl.Rows.Add(dtr);
        }
        //grdReport.DataSource = tbl;
        //grdReport.DataBind();
    }

    void GridView1_PreRender(object sender, EventArgs e)
    {
        //if (grdTransSummary.Rows.Count > 0)
        //{
        //    grdTransSummary.UseAccessibleHeader = true;
        //    grdTransSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}
    }

    public DataTable getStock2()
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            //string qry = "select distinct index_type,symbol,rate,change from psx_ticker";
            string qry = "select distinct index_type,symbol,rate,change from investment.psx_ticker";
            

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
    public DataTable getStock()
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            //string qry = "select distinct * from indices_value";
            string qry = "select * from investment.indices_value";


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

    private DataTable portfolioAllocationDetail(string FundCode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"with Equity as

 (SELECT equity.PRICE_DATE,
         F.FUND_SHORT_NAME as fundshortname,
         equity.FUND_CODE fundCode,
         F.FUND_NAME as fundName,
         equity.SYMBOL,
         (select distinct (sector_name)
            from sector
           where sector_code = (select sector_code
                                  from security
                                 where SYMBOL = equity.symbol)) sector,
         
         CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END as VOLUME,
         CASE
           WHEN equity.HFT_BLOCKED_VOLUME = 0 THEN
            equity.AFS_BLOCKED_VOLUME
           ELSE
            equity.HFT_BLOCKED_VOLUME
         END BLOCKED_VOLUME,
         
         0 as avgPrice,
         
         (select em.close_rate
            from equity_market em
           where em.symbol = equity.SYMBOL
             and em.price_date = equity.PRICE_DATE) as closingRate,
         
         0 as gainLoss,
         ((CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END) * (select em.close_rate
                    from equity_market em
                   where em.symbol = equity.SYMBOL
                     and em.price_date = equity.PRICE_DATE)) as marketValue,
         
         CASE
           WHEN equity.HFT_MARK_TO_MKT_VALUE = 0 THEN
            equity.AFS_MARK_TO_MKT_VALUE
           ELSE
            equity.HFT_MARK_TO_MKT_VALUE
         END as markToMKTValue,
         
         CASE
           WHEN equity.hft_historical_value = 0 THEN
            equity.afs_historical_value
           ELSE
            equity.hft_historical_value
         END as historicalValue,
         
         N.NET_ASSETS AS netAsset,
         (SELECT sum(amount)
            FROM GL_ALL_VOUCHERS G
           inner join security s
              on s.sl_code = g.gl_sl_code
           WHERE G.fund_code = f.fund_code
             AND G.gl_glmf_code LIKE '50102010101%'
             AND G.gl_form_date >=
                 (select max(start_date) from financial_years)
             and s.symbol = equity.symbol) as Dividend
  
    FROM EQUITY_PORTFOLIO equity, FUND F, NET_ASSETS_BEFORE_FEE N
   WHERE F.fund_code = equity.FUND_CODE
     AND equity.PRICE_DATE =
         (SELECT MAX(PRICE_DATE) - 1
            FROM EQUITY_PORTFOLIO
           WHERE FUND_CODE = EQUITY.FUND_CODE)
     AND N.FUND_CODE = F.FUND_CODE
     AND N.PRICE_DATE =
         (select max(price_date)
            from net_assets_before_fee nn
           where nn.fund_code = equity.fund_code)
     and equity.fund_code = :fund_code
     AND equity.SYMBOL NOT IN ('JSGF')
     and equity.symbol not in ('JSCF',
                               'JSIF',
                               'JSIIF',
                               'JSPSF-DSF',
                               'JSPSF-MMSF',
                               'JSIPSF-DSF',
                               'JSIPSF-MMSF','JSMFSF')
   ORDER BY PRICE_DATE),

FIS as
 (SELECT equity.PRICE_DATE,
         F.FUND_SHORT_NAME as fundshortname,
         equity.FUND_CODE fundCode,
         F.FUND_NAME as fundName,
         equity.SYMBOL,
         (select distinct (sector_name)
            from sector
           where sector_code = (select sector_code
                                  from security
                                 where SYMBOL = equity.symbol)) sector,
         
         CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END as VOLUME,
         CASE
           WHEN equity.HFT_BLOCKED_VOLUME = 0 THEN
            equity.AFS_BLOCKED_VOLUME
           ELSE
            equity.HFT_BLOCKED_VOLUME
         END BLOCKED_VOLUME,
         
         0 as avgPrice,
         
         (select em.close_rate
            from equity_market em
           where em.symbol = equity.SYMBOL
             and em.price_date = equity.PRICE_DATE) as closingRate,
         
         0 as gainLoss,
         ((CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END) * (select em.close_rate
                    from equity_market em
                   where em.symbol = equity.SYMBOL
                     and em.price_date = equity.PRICE_DATE)) as marketValue,
         
         CASE
           WHEN equity.HFT_MARK_TO_MKT_VALUE = 0 THEN
            equity.AFS_MARK_TO_MKT_VALUE
           ELSE
            equity.HFT_MARK_TO_MKT_VALUE
         END as markToMKTValue,
         
         CASE
           WHEN equity.hft_historical_value = 0 THEN
            equity.afs_historical_value
           ELSE
            equity.hft_historical_value
         END as historicalValue,
         
         N.NET_ASSETS AS netAsset,
         (SELECT sum(amount)
            FROM GL_ALL_VOUCHERS G
           inner join security s
              on s.sl_code = g.gl_sl_code
           WHERE G.fund_code = f.fund_code
             AND G.gl_glmf_code LIKE '50102010101%'
             AND G.gl_form_date >=
                 (select max(start_date) from financial_years)
             and s.symbol = equity.symbol) as Dividend
  
    FROM EQUITY_PORTFOLIO equity, FUND F, NET_ASSETS_BEFORE_FEE N
   WHERE F.fund_code = equity.FUND_CODE
     AND equity.PRICE_DATE =
         (SELECT MAX(PRICE_DATE) - 1
            FROM EQUITY_PORTFOLIO
           WHERE FUND_CODE = EQUITY.FUND_CODE)
     AND N.FUND_CODE = F.FUND_CODE
     AND N.PRICE_DATE =
         (select max(price_date)
            from net_assets_before_fee nn
           where nn.fund_code = equity.fund_code)
     and equity.fund_code = :fund_code
     AND equity.SYMBOL NOT IN ('JSGF')
     and equity.symbol in ('JSCF',
                           'JSIF',
                           'JSIIF',
                           'JSPSF-DSF',
                           'JSPSF-MMSF',
                           'JSIPSF-DSF',
                           'JSIPSF-MMSF','JSMFSF')
  
  union all
  SELECT FIS.PRICE_DATE,
         F.FUND_SHORT_NAME as fundshortname,
         FIS.FUND_CODE fundCode,
         F.FUND_NAME as fundName,
         S.SCHEME_NAME AS SYMBOL,
         'SECTOR' AS SECTOR,
         
         CASE
           WHEN FIS.HFT_VOLUME = 0 THEN
            FIS.AFS_VOLUME
           ELSE
            FIS.HFT_VOLUME
         END as VOLUME,
         CASE
           WHEN FIS.HFT_BLOCKED_VOLUME = 0 THEN
            FIS.AFS_BLOCKED_VOLUME
           ELSE
            FIS.HFT_BLOCKED_VOLUME
         END BLOCKED_VOLUME,
         
         0 as avgPrice,
         
         (SELECT RATE
            FROM FIS_MARKET_RATES FR
           WHERE FR.SCHEME_CODE = S.SCHEME_CODE
             AND FR.PRICE_DATE = FIS.PRICE_DATE) as closingRate,
         
         0 as gainLoss,
         ((CASE
           WHEN FIS.HFT_VOLUME = 0 THEN
            FIS.AFS_VOLUME
           ELSE
            FIS.HFT_VOLUME
         END) * (SELECT RATE
                    FROM FIS_MARKET_RATES FR
                   WHERE FR.SCHEME_CODE = S.SCHEME_CODE
                     AND FR.PRICE_DATE = FIS.PRICE_DATE)) as marketValue,
         
         CASE
           WHEN FIS.HFT_MARK_TO_MARKET_VALUE = 0 THEN
            FIS.AFS_MARK_TO_MARKET_VALUE
           ELSE
            FIS.HFT_MARK_TO_MARKET_VALUE
         END as markToMKTValue,
         
         CASE
           WHEN FIS.hft_historical_value = 0 THEN
            FIS.afs_historical_value
           ELSE
            FIS.hft_historical_value
         END as historicalValue,
         
         N.NET_ASSETS AS netAsset,
         0            as Dividend
  
    FROM fis_PORTFOLIO FIS, FUND F, NET_ASSETS_BEFORE_FEE N, SCHEME S
   WHERE F.fund_code = FIS.FUND_CODE
     AND FIS.PRICE_DATE =
         (SELECT MAX(PRICE_DATE) - 1
            FROM FIS_PORTFOLIO
           WHERE FUND_CODE = FIS.FUND_CODE)
     AND N.FUND_CODE = F.FUND_CODE
     AND N.PRICE_DATE = (select max(price_date)
                           from net_assets_before_fee nn
                          where nn.fund_code = fis.fund_code)
     AND FIS.SCHEME_CODE = S.SCHEME_CODE
     and fis.fund_code = :fund_code
     AND S.SCHEME_NAME NOT IN ('JSGF'))

select 'FIS' AS Type,
       FIS.symbol as Security,
       round(FIS.volume, 2) as Volume,
       round(FIS.historicalValue / FIS.volume, 2) AS Avg_cost,
       round(FIS.closingRate, 2) as Price,
       round(FIS.historicalValue, 2) as Cost,
       round(FIS.marketValue, 2) as MarketValue,
       round((FIS.closingRate - (markToMKTValue / volume)) * volume, 2) as GainLossMTM

  from FIS
 where FIS.symbol not like 'JSGF%'

union all

select 'EQUITY' AS Type,
       equity.symbol as Security,
       round(equity.volume, 2) as Volume,
       round(equity.historicalValue / equity.volume, 2) AS AVG_COST,
       round(equity.closingRate, 2) as Price,
       round(equity.historicalValue, 2) as Cost,
       round(equity.marketValue, 2) as MarketValue,
       round((equity.closingRate - (markToMKTValue / volume)) * volume, 2) as GainLossMTM

  from equity
 where equity.symbol not like 'JSGF%'
 
union all

select 'TOTAL','',(select
       sum(round(equity.volume, 2)) as Volume

  from equity
 where equity.symbol not like 'JSGF%')+(select 
       sum(round(FIS.volume, 2)) as Volume from FIS
 where FIS.symbol not like 'JSGF%'),
 (select
       sum(round(equity.historicalValue / equity.volume, 2)) as AVG_COST

  from equity
 where equity.symbol not like 'JSGF%')+(select 
       sum(round(FIS.historicalValue / FIS.volume, 2)) AS Avg_cost
        from FIS
 where FIS.symbol not like 'JSGF%'),
 
(select
       sum(round(equity.closingRate, 2)) as Price

  from equity
 where equity.symbol not like 'JSGF%')+(select 
       sum(round(FIS.closingRate, 2)) as Price
        from FIS
 where FIS.symbol not like 'JSGF%'),
(select
       sum(round(equity.historicalValue, 2)) as Cost

  from equity
 where equity.symbol not like 'JSGF%')+(select 
       sum(round(FIS.historicalValue, 2)) as Cost
        from FIS
 where FIS.symbol not like 'JSGF%'),
(select
       sum(round(equity.marketValue, 2)) as MarketValue

  from equity
 where equity.symbol not like 'JSGF%')+(select 
       sum(round(FIS.marketValue, 2)) as MarketValue
        from FIS
 where FIS.symbol not like 'JSGF%'),
 (select
       sum(round((equity.closingRate - (markToMKTValue / volume)) * volume, 2)) as GainLossMTM

  from equity
 where equity.symbol not like 'JSGF%')+(select 
       sum(round((FIS.closingRate - (markToMKTValue / volume)) * volume, 2)) as GainLossMTM
        from FIS
 where FIS.symbol not like 'JSGF%')
 from dual
";
            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":fund_code", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = FundCode;
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

    private DataTable getTransSummary(string FundCode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {



            //string qry = "select query_text from jsil_queries where id=2";


            //object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);
                    string qry = @"select US.SALE_DATE,
                    f.fund_short_name,
                    'Inflow' as Transaction_Type,
                    us.quantity * pd.offer_price as Amount
                    from unit_sale us
                    inner join fund f
                    on f.fund_code = us.fund_code
                    inner join payment_detail pd
                    on pd.payment_id = us.payment_id
                    where us.fund_code = :fund_code
                    UNION ALL
                     select ur.redemption_date
                    ,f.fund_short_name
                    ,'Withdrawal' as Transaction_Type
                    ,ur.uncertified_quantity*ur.redemption_price as Amount
                    from unit_redemption ur
                    inner join fund f on ur.fund_code=f.fund_code
                    where ur.fund_code=:fund_code";
            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":fund_code", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = FundCode;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(),oracleParameterArray).Tables[0];


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

    private DataTable tblProfiledetailJson(string Fundcode,string fis)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            string qry = @"with data as
 (select EP.FUND_CODE,
         'EQUITY' as Type,
         ep.price_date,
         sum(hft_volume * em.close_rate) as Market_Value,
         (select n.net_assets
            from net_assets_before_fee n
           where n.price_date = (select max(price_date) from net_assets_before_fee where fund_code=n.fund_code)
             and n.fund_code = ep.fund_code) as netAsset
    from equity_portfolio ep
   inner join equity_market em
      on ep.symbol = em.symbol
     and em.price_date = ep.price_date
   where ep.price_date = (select max(price_date) from equity_market where fund_code=ep.fund_code)
     and ep.fund_code = :fund_code
     and ep.symbol not in ('JSCF',
                           'JSIF',
                           'JSIIF',
                           'JSPSF-DSF',
                           'JSPSF-MMSF',
                           'JSIPSF-DSF',
                           'JSIPSF-MMSF','JSMFSF')
   group by ep.fund_code, ep.price_date
  union all
  
  select EP.FUND_CODE,
         'FIS' as Type,
         ep.price_date,
         sum(hft_volume * em.close_rate) as Market_Value,
         (select n.net_assets
            from net_assets_before_fee n
           where n.price_date =
                 (select max(price_date)
                    from net_assets_before_fee
                   where fund_code = ep.fund_code)
             and n.fund_code = ep.fund_code) as netAsset
    from equity_portfolio ep
   inner join equity_market em
      on ep.symbol = em.symbol
     and em.price_date = ep.price_date
   where ep.price_date = (select max(price_date)
                            from equity_market
                           where fund_code = ep.fund_code)
     and ep.fund_code = :fund_Code
     and ep.symbol in ('JSCF',
                       'JSIF',
                       'JSIIF',
                       'JSPSF-DSF',
                       'JSPSF-MMSF',
                       'JSIPSF-DSF',
                       'JSIPSF-MMSF',
                       'JSMFSF')
   group by ep.fund_code, ep.price_date

  
  UNION ALL
  SELECT fp.fund_code,
         'FIS' as Type,
         fp.price_date,
         sum(fp.hft_volume * f.rate) as MarketValue,
         
         (select n.net_assets
            from net_assets_before_fee n
           where n.fund_code = fp.fund_code
             and n.price_date = (select max(price_date) from net_assets_before_fee where fund_code=fp.fund_code)) as NetAsset
    FROM FIS_PORTFOLIO FP
   inner join fis_market_rates f
      on f.price_date = fp.price_date
     and f.scheme_code = fp.scheme_code
     and f.scheme_issue_date = fp.scheme_issue_date
   where fp.price_date = (select max(price_date) from fis_market_rates where fund_code=fp.fund_code)
     and fp.fund_code = :fund_code
   group by fp.fund_code, fp.price_date)

select data.fund_code,
       f.fund_name,
       Type,
       data.price_date,
       sUM(ROUND(data.market_value / netasset * 100, 2)) as Percentage
  from data
 inner join fund f
    on f.fund_code = data.fund_code
 GROUP BY data.fund_code, f.fund_name, Type, data.price_date";
            OracleParameter[] oracleParameterArray = new OracleParameter[2];
            oracleParameterArray[0] = new OracleParameter(":fund_code", (object)Fundcode);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray).Tables[0];


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
    private DataTable tblProfiledetail(string Fundcode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            string qry = @"with Equity as

 (SELECT equity.PRICE_DATE,
         F.FUND_SHORT_NAME as fundshortname,
         equity.FUND_CODE fundCode,
         F.FUND_NAME as fundName,
         equity.SYMBOL,
         (select distinct (sector_name)
            from sector
           where sector_code = (select sector_code
                                  from security
                                 where SYMBOL = equity.symbol)) sector,
         
         CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END as VOLUME,
         CASE
           WHEN equity.HFT_BLOCKED_VOLUME = 0 THEN
            equity.AFS_BLOCKED_VOLUME
           ELSE
            equity.HFT_BLOCKED_VOLUME
         END BLOCKED_VOLUME,
         
         0 as avgPrice,
         
         (select em.close_rate
            from equity_market em
           where em.symbol = equity.SYMBOL
             and em.price_date = equity.PRICE_DATE) as closingRate,
         
         0 as gainLoss,
         ((CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END) * (select em.close_rate
                    from equity_market em
                   where em.symbol = equity.SYMBOL
                     and em.price_date = equity.PRICE_DATE)) as marketValue,
         
         CASE
           WHEN equity.HFT_MARK_TO_MKT_VALUE = 0 THEN
            equity.AFS_MARK_TO_MKT_VALUE
           ELSE
            equity.HFT_MARK_TO_MKT_VALUE
         END as markToMKTValue,
         
         CASE
           WHEN equity.hft_historical_value = 0 THEN
            equity.afs_historical_value
           ELSE
            equity.hft_historical_value
         END as historicalValue,
         
         (select paidup_capital from security where SYMBOL = equity.symbol) paidup,
         N.NET_ASSETS AS netAsset
  
    FROM EQUITY_PORTFOLIO equity, FUND F, NET_ASSETS_BEFORE_FEE N
   WHERE F.fund_code = equity.FUND_CODE
     AND equity.PRICE_DATE =
         (SELECT MAX(PRICE_DATE) - 1
            FROM EQUITY_PORTFOLIO
           WHERE FUND_CODE = EQUITY.FUND_CODE)
     AND N.FUND_CODE = F.FUND_CODE
     AND N.PRICE_DATE = equity.PRICE_DATE
     and equity.fund_code = :fund_code
     AND equity.SYMBOL NOT IN ('JSGF')
     and equity.symbol not in ('JSCF',
                           'JSIF',
                           'JSIIF',
                           'JSPSF-DSF',
                           'JSPSF-MMSF',
                           'JSIPSF-DSF',
                           'JSIPSF-MMSF','JSMFSF')
   ),

FIS as
 (
 SELECT equity.PRICE_DATE,
         F.FUND_SHORT_NAME as fundshortname,
         equity.FUND_CODE fundCode,
         F.FUND_NAME as fundName,
         equity.SYMBOL,
         (select distinct (sector_name)
            from sector
           where sector_code = (select sector_code
                                  from security
                                 where SYMBOL = equity.symbol)) sector,
         
         CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END as VOLUME,
         CASE
           WHEN equity.HFT_BLOCKED_VOLUME = 0 THEN
            equity.AFS_BLOCKED_VOLUME
           ELSE
            equity.HFT_BLOCKED_VOLUME
         END BLOCKED_VOLUME,
         
         0 as avgPrice,
         
         (select em.close_rate
            from equity_market em
           where em.symbol = equity.SYMBOL
             and em.price_date = equity.PRICE_DATE) as closingRate,
         
         0 as gainLoss,
         ((CASE
           WHEN equity.HFT_VOLUME = 0 THEN
            equity.AFS_VOLUME
           ELSE
            equity.HFT_VOLUME
         END) * (select em.close_rate
                    from equity_market em
                   where em.symbol = equity.SYMBOL
                     and em.price_date = equity.PRICE_DATE)) as marketValue,
         
         CASE
           WHEN equity.HFT_MARK_TO_MKT_VALUE = 0 THEN
            equity.AFS_MARK_TO_MKT_VALUE
           ELSE
            equity.HFT_MARK_TO_MKT_VALUE
         END as markToMKTValue,
         
         CASE
           WHEN equity.hft_historical_value = 0 THEN
            equity.afs_historical_value
           ELSE
            equity.hft_historical_value
         END as historicalValue,
         
         N.NET_ASSETS AS netAsset
  
    FROM EQUITY_PORTFOLIO equity, FUND F, NET_ASSETS_BEFORE_FEE N
   WHERE F.fund_code = equity.FUND_CODE
     AND equity.PRICE_DATE =
         (SELECT MAX(PRICE_DATE) - 1
            FROM EQUITY_PORTFOLIO
           WHERE FUND_CODE = EQUITY.FUND_CODE)
     AND N.FUND_CODE = F.FUND_CODE
     AND N.PRICE_DATE = equity.PRICE_DATE
     and equity.fund_code = :fund_code
     AND equity.SYMBOL NOT IN ('JSGF')
     and equity.symbol in ('JSCF',
                           'JSIF',
                           'JSIIF',
                           'JSPSF-DSF',
                           'JSPSF-MMSF',
                           'JSIPSF-DSF',
                           'JSIPSF-MMSF','JSMFSF')
 union all
 SELECT FIS.PRICE_DATE,
         F.FUND_SHORT_NAME as fundshortname,
         FIS.FUND_CODE fundCode,
         F.FUND_NAME as fundName,
         S.SCHEME_NAME AS SYMBOL,
         'SECTOR' AS SECTOR,
         
         CASE
           WHEN FIS.HFT_VOLUME = 0 THEN
            FIS.AFS_VOLUME
           ELSE
            FIS.HFT_VOLUME
         END as VOLUME,
         CASE
           WHEN FIS.HFT_BLOCKED_VOLUME = 0 THEN
            FIS.AFS_BLOCKED_VOLUME
           ELSE
            FIS.HFT_BLOCKED_VOLUME
         END BLOCKED_VOLUME,
         
         0 as avgPrice,
         
         (SELECT RATE
            FROM FIS_MARKET_RATES FR
           WHERE FR.SCHEME_CODE = S.SCHEME_CODE
             AND FR.PRICE_DATE = FIS.PRICE_DATE) as closingRate,
         
         0 as gainLoss,
         ((CASE
           WHEN FIS.HFT_VOLUME = 0 THEN
            FIS.AFS_VOLUME
           ELSE
            FIS.HFT_VOLUME
         END) * (SELECT RATE
                    FROM FIS_MARKET_RATES FR
                   WHERE FR.SCHEME_CODE = S.SCHEME_CODE
                     AND FR.PRICE_DATE = FIS.PRICE_DATE)) as marketValue,
         
         CASE
           WHEN FIS.HFT_MARK_TO_MARKET_VALUE = 0 THEN
            FIS.AFS_MARK_TO_MARKET_VALUE
           ELSE
            -FIS.HFT_MARK_TO_MARKET_VALUE
         END as markToMKTValue,
         
         CASE
           WHEN FIS.hft_historical_value = 0 THEN
            FIS.afs_historical_value
           ELSE
            FIS.hft_historical_value
         END as historicalValue,
         
         N.NET_ASSETS AS netAsset
  
    FROM fis_PORTFOLIO FIS, FUND F, NET_ASSETS_BEFORE_FEE N, SCHEME S
   WHERE F.fund_code = FIS.FUND_CODE
     AND FIS.PRICE_DATE =
         (SELECT MAX(PRICE_DATE) - 1
            FROM FIS_PORTFOLIO
           WHERE FUND_CODE = FIS.FUND_CODE)
     AND N.FUND_CODE = F.FUND_CODE
     AND N.PRICE_DATE = FIS.PRICE_DATE
     AND FIS.SCHEME_CODE = S.SCHEME_CODE
     and fis.fund_code = :fund_code
     AND S.SCHEME_NAME NOT IN ('JSGF'))

select 'FIS',
       FIS.symbol as Script,
       
       round(FIS.historicalValue, 2) as Cost

  from FIS
 where FIS.symbol not like 'JSGF%'

union all

select 'Equity',
       equity.symbol as Script,
       round(equity.historicalValue, 2) as Cost
  from equity
 where equity.symbol not like 'JSGF%'";
            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":fund_code", (object)Fundcode);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray).Tables[0];
            

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
    private DataTable tblPortfolioAllocation(string Fundcode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

                                string qry = @"with data as
                                (select EP.FUND_CODE,
                                'EQUITY' as Type,
                                ep.price_date,
                                sum(hft_volume * em.close_rate) as Market_Value,
                                (select n.net_assets
                                from net_assets_before_fee n
                                where n.price_date = (select max(price_date) from net_assets_before_fee where fund_code=n.fund_code)
                                and n.fund_code = ep.fund_code) as netAsset
                                from equity_portfolio ep
                                inner join equity_market em
                                on ep.symbol = em.symbol
                                and em.price_date = ep.price_date
                                where ep.price_date = (select max(price_date) from equity_portfolio where fund_code=ep.fund_code)
                                and ep.fund_code = :fundCode
                                and ep.symbol not in ('JSCF',
                                'JSIF',
                                'JSIIF',
                                'JSPSF-DSF',
                                'JSPSF-MMSF',
                                'JSIPSF-DSF',
                                'JSIPSF-MMSF','JSMFSF')
                                group by ep.fund_code, ep.price_date
                                union all
  
                                select EP.FUND_CODE,
                                'FIS' as Type,
                                ep.price_date,
                                sum(hft_volume * em.close_rate) as Market_Value,
                                (select n.net_assets
                                from net_assets_before_fee n
                                where n.price_date = (select max(price_date) from net_assets_before_fee where fund_code=ep.fund_code)
                                and n.fund_code = ep.fund_code) as netAsset
                                from equity_portfolio ep
                                inner join equity_market em
                                on ep.symbol = em.symbol
                                and em.price_date = ep.price_date
                                where ep.price_date = (select max(price_date) from equity_portfolio where fund_code=ep.fund_code)
                                and ep.fund_code = :fundCode
                                and ep.symbol in ('JSCF',
                                'JSIF',
                                'JSIIF',
                                'JSPSF-DSF',
                                'JSPSF-MMSF',
                                'JSIPSF-DSF',
                                'JSIPSF-MMSF','JSMFSF')
                                group by ep.fund_code, ep.price_date
  
                                UNION ALL
                                SELECT fp.fund_code,
                                'FIS' as Type,
                                fp.price_date,
                                sum(fp.hft_volume * f.rate) as MarketValue,
         
                                (select n.net_assets
                                from net_assets_before_fee n
                                where n.fund_code = fp.fund_code
                                and n.price_date = (select max(price_date) from net_assets_before_fee where fund_code=fp.fund_code)) as NetAsset
                                FROM FIS_PORTFOLIO FP
                                inner join fis_market_rates f
                                on f.price_date = fp.price_date
                                and f.scheme_code = fp.scheme_code
                                and f.scheme_issue_date = fp.scheme_issue_date
                                where fp.price_date = (select max(price_date) from fis_portfolio where fund_code=fp.fund_code)
                                and fp.fund_code = :fundCode
                                group by fp.fund_code, fp.price_date)

                                select data.fund_code,
                                f.fund_name,
                                Type,
                                sUM(ROUND(data.market_value / netasset * 100, 2)) as Percentage
                                from data
                                inner join fund f
                                on f.fund_code = data.fund_code
                                GROUP BY data.fund_code, f.fund_name, Type";


            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":fundCode", (object)Fundcode);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray).Tables[0];
           

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
    private DataTable getProfitLossData(string fundcode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {

            
                    string qry = @"WITH EXPENSE AS
                    (SELECT G.fund_code,
                    CASE
                    WHEN G.gl_form_date <=
                    (SELECT F.END_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE) - 1) AND G.dc = 'D' AND
                    G.gl_glmf_code LIKE '4%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS EXPENSE_OPEN_DEBIT,
                    CASE
                    WHEN G.gl_form_date <=
                    (SELECT F.END_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE) - 1) AND G.dc = 'C' AND
                    G.gl_glmf_code LIKE '4%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS EXPENSE_OPEN_CREDIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (SELECT F.START_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE)) AND
                    (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS) AND G.dc = 'D' AND
                    G.gl_glmf_code LIKE '4%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS EXPENSE_PERIOD_DEBIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (SELECT F.START_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE)) AND
                    (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS) AND G.dc = 'C' AND
                    G.gl_glmf_code LIKE '4%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS EXPENSE_PERIOD_CREDIT,
         
                    CASE
                    WHEN G.gl_form_date <=
                    (SELECT F.END_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE) - 1) AND G.dc = 'D' AND
                    G.gl_glmf_code LIKE '5%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS INCOME_OPEN_DEBIT,
                    CASE
                    WHEN G.gl_form_date <=
                    (SELECT F.END_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE) - 1) AND G.dc = 'C' AND
                    G.gl_glmf_code LIKE '5%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS INCOME_OPEN_CREDIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (SELECT F.START_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE)) AND
                    (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS) AND G.dc = 'D' AND
                    G.gl_glmf_code LIKE '5%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS INCOME_PERIOD_DEBIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (SELECT F.START_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE)) AND
                    (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS) AND G.dc = 'C' AND
                    G.gl_glmf_code LIKE '5%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS INCOME_PERIOD_CREDIT,
                    CASE
                    WHEN G.gl_form_date <=
                    (SELECT F.END_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE) - 1) AND G.dc = 'D' AND
                    G.gl_glmf_code LIKE '203%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS RESERVES_OPEN_DEBIT,
                    CASE
                    WHEN G.gl_form_date <=
                    (SELECT F.END_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE) - 1) AND G.dc = 'C' AND
                    G.gl_glmf_code LIKE '203%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS RESERVES_OPEN_CREDIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (SELECT F.START_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE)) AND
                    (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS) AND G.dc = 'D' AND
                    G.gl_glmf_code LIKE '203%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS RESERVES_PERIOD_DEBIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (SELECT F.START_DATE
                    FROM FINANCIAL_YEARS F
                    WHERE EXTRACT(YEAR FROM END_DATE) =
                    EXTRACT(YEAR FROM SYSDATE)) AND
                    (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS) AND G.dc = 'C' AND
                    G.gl_glmf_code LIKE '203%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS RESERVES_PERIOD_CREDIT,
         
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (select trunc((sysdate), 'month') as FirstDay from dual) AND
                    TO_DATE(SYSDATE) AND G.dc = 'D' AND G.gl_glmf_code LIKE '4%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS MEXPENSE_PERIOD_DEBIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (select trunc((sysdate), 'month') as FirstDay from dual) AND
                    TO_DATE(SYSDATE) AND G.dc = 'C' AND G.gl_glmf_code LIKE '4%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS MEXPENSE_PERIOD_CREDIT
         
                    ,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (select trunc((sysdate), 'month') as FirstDay from dual) AND
                    TO_DATE(SYSDATE) AND G.dc = 'C' AND G.gl_glmf_code LIKE '5%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS MINCOME_PERIOD_CREDIT,
                    CASE
                    WHEN G.gl_form_date BETWEEN
                    (select trunc((sysdate), 'month') as FirstDay from dual) AND
                    TO_DATE(SYSDATE) AND G.dc = 'D' AND G.gl_glmf_code LIKE '5%' THEN
                    AMOUNT
                    ELSE
                    0
                    END AS MINCOME_PERIOD_DEBIT
  
                    FROM GL_ALL_VOUCHERS G
                    WHERE G.gl_form_date <= to_date(sysdate)
                    and g.fund_code = :fund_code)

                    SELECT e.fund_code,
                    F.FUND_NAME,
                    (((SUM(INCOME_OPEN_CREDIT) - SUM(INCOME_OPEN_DEBIT) +
                    SUM(INCOME_PERIOD_CREDIT) - SUM(INCOME_PERIOD_DEBIT))) +
                    ((SUM(EXPENSE_OPEN_CREDIT) - SUM(EXPENSE_OPEN_DEBIT) +
                    SUM(EXPENSE_PERIOD_CREDIT) - SUM(EXPENSE_PERIOD_DEBIT)))) +
       
                    ((SUM(RESERVES_OPEN_CREDIT) - SUM(RESERVES_OPEN_DEBIT) +
                    SUM(RESERVES_PERIOD_CREDIT) - SUM(RESERVES_PERIOD_DEBIT))) AS INCEPTION,
                    'Inception' as Type

                    FROM EXPENSE E
                    INNER JOIN FUND F
                    ON F.FUND_CODE = E.FUND_CODE
                    inner join unit_nav u
                    on u.fund_code = e.fund_code
                    and u.price_date = (SELECT MAX(START_DATE) FROM FINANCIAL_YEARS F)
                    GROUP BY e.fund_code, F.FUND_NAME
                    -- ORDER BY F.FUND_NAME

                    UNION ALL

                    SELECT e.fund_code,
                    F.FUND_NAME,
       
                    ((SUM(INCOME_OPEN_CREDIT) - SUM(INCOME_OPEN_DEBIT) +
                    SUM(INCOME_PERIOD_CREDIT) - SUM(INCOME_PERIOD_DEBIT))) +
                    ((SUM(EXPENSE_OPEN_CREDIT) - SUM(EXPENSE_OPEN_DEBIT) +
                    SUM(EXPENSE_PERIOD_CREDIT) - SUM(EXPENSE_PERIOD_DEBIT))) as FINANCIAL_YEAR,
                    'Financial Year' as Type
                    FROM EXPENSE E
                    INNER JOIN FUND F
                    ON F.FUND_CODE = E.FUND_CODE
                    inner join unit_nav u
                    on u.fund_code = e.fund_code and
                    u.price_date = (SELECT MAX(START_DATE) FROM FINANCIAL_YEARS F)
                    GROUP BY e.fund_code, F.FUND_NAME
                    --ORDER BY F.FUND_NAME

                    UNION ALL

                    SELECT e.fund_code,
                    F.FUND_NAME,
       
                    (SUM(E.MINCOME_PERIOD_CREDIT) - SUM(e.MINCOME_PERIOD_DEBIT)) +
                    (SUM(E.MEXPENSE_PERIOD_credit) - SUM(E.MEXPENSE_PERIOD_debit)) MONTHLY,
                    'Monthly' as Type

                    FROM EXPENSE E
                    INNER JOIN FUND F
                    ON F.FUND_CODE = E.FUND_CODE
                    inner join unit_nav u
                    on u.fund_code = e.fund_code and
                    u.price_date = (SELECT MAX(START_DATE) FROM FINANCIAL_YEARS F)
                    GROUP BY e.fund_code, F.FUND_NAME
                    ORDER BY FUND_NAME
                    ";


            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":fund_code", (object)fundcode);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray).Tables[0];
            //grdProfitLoss.DataSource = tbl;
            //grdProfitLoss.DataBind();

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

    private void getPortfolioAllocation(string fundcode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();

        try
        {


            tbl = tblPortfolioAllocation(fundcode);
            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            List<getdata> data = new List<getdata>();

            amoutdata getdata1 = new amoutdata();
            List<decimal> items2 = new List<decimal>();
            List<getdata> items = new List<getdata>();
            List<amoutdata> dd = new List<amoutdata>();

            var chartData = new object[tbl.Rows.Count + 1];
            List<decimal> lst_dataItem_1 = new List<decimal>();
            List<decimal> lst_dataItem_2 = new List<decimal>();
            int j = 0;
            foreach (DataRow drow in tbl.Rows)
            {

                labels.Add(drow["TYPE"].ToString());
                //lst_dataItem_1.Add(Convert.ToDecimal(drow["SUBMISSION_AMT"].ToString()));
                //chartData[j] = new object[] { "{" + "name:" + drow["YEAR"].ToString(), "data:" + Convert.ToDecimal(drow["SUBMISSION_AMT"].ToString()) +"}" };
                j++;
            }
            string[] colr = { "#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990" };

            for (int i = 0; i < tbl.Rows.Count; i++)
            {

                dd.Add(new amoutdata
                {
                    y =
                    decimal.Round(Convert.ToDecimal(tbl.Rows[i]["PERCENTAGE"].ToString()), 2, MidpointRounding.AwayFromZero),
                    color = colr[i],
                    name = tbl.Rows[i]["TYPE"].ToString(),
                    key = tbl.Rows[i]["TYPE"].ToString()


                });



            }

            //items.Add(new getdata
            //{


            //    data = dd,
            //    //name = labels

            //});
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Chartdata = serializer.Serialize(dd);
            ChartLable = serializer.Serialize(labels);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {

        }


    }

    private DataTable PortfolioAllocation(string folioNumber)
    {
        DataTable tbl = new DataTable();
        //DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"SELECT VDD.FUND_SHORT_NAME AS FUND_NAME,
       MAX(VDD.NAV) AS NAV,
       MAX(VDD.UNITS) AS UNIT,
       SUM(vdd.balance) AS AMOUNT,
       VDD.FOLIO_NUMBER
  FROM VPS_DASHBOARD_DETAILS VDD
 WHERE VDD.Folio_Number = :Folio_number
 --AND EXTRACT(YEAR FROM VDD.DATED) = EXTRACT(YEAR FROM SYSDATE)
 --AND EXTRACT(MONTH FROM VDD.DATED)= EXTRACT(MONTH FROM SYSDATE)
 GROUP BY VDD.FUND_CODE,
          VDD.FUND_SHORT_NAME,
          VDD.FOLIO_NUMBER";

            //object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":Folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];

            List<amoutdata> dd = new List<amoutdata>();


            //tbl.AsEnumerable().ToList().ForEach(s => { labels.Add(s["FUND_CODE"].ToString()); });


            string[] colr = { "#00008b", "#4682b4", "#0000ff" };

            Random rd = new Random(3);
            //tbl.AsEnumerable().ToList().foreach (d => {

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                string FundColor="";
                if(tbl.Rows[i]["FUND_NAME"].ToString()== "JS PSF-ESF" || tbl.Rows[i]["FUND_NAME"].ToString() == "JSIPSF-ESF")
                {
                    FundColor = "#144c8d"; 
                }
                else if(tbl.Rows[i]["FUND_NAME"].ToString() == "JS PSF-MSF" || tbl.Rows[i]["FUND_NAME"].ToString() == "JSIPSF-MSF")
                {
                    FundColor = "#f9a66f";
                }
                else
                {
                    FundColor = "#c5e7ea";
                }
                dd.Add(new amoutdata
                {
                    y =
                    decimal.Round(Convert.ToDecimal(tbl.Rows[i]["AMOUNT"].ToString()), 2, MidpointRounding.AwayFromZero),
                    color = FundColor,
                    name = tbl.Rows[i]["FUND_NAME"].ToString(),
                    //key = tbl.Rows[i]["TOTAL_UNITS"].ToString(),


                });
            }




            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            profiledetaildata = serializer.Serialize(dd);

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

    private DataTable PortfolioAllocationCustom(string folioNumber,string isIsLamic)
    {
        DataTable tbl = new DataTable();
        //DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"SELECT DISTINCT
            f.fund_short_name AS fund_name,
            get_fund_nav(TO_DATE(sysdate), ube.fund_code) AS NAV,
            get_fund_folio_balance(TO_DATE(sysdate), ube.fund_code, ube.folio_number) AS unit,
            get_fund_folio_balance(TO_DATE(sysdate), ube.fund_code, ube.folio_number) *
            get_fund_nav(TO_DATE(sysdate), ube.fund_code) AS amount,
            u.folio_number AS folio_number
            FROM unit_account u
            INNER JOIN unit_balance_electronic ube ON u.folio_number = ube.folio_number
            INNER JOIN fund f ON f.fund_code = ube.fund_code
            WHERE u.folio_number = :folio_number
            AND f.islamic = :islamic";

            //object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[2];
            oracleParameterArray[0] = new OracleParameter("folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            oracleParameterArray[1] = new OracleParameter("islamic", OracleType.VarChar);
            oracleParameterArray[1].Direction = ParameterDirection.Input;
            oracleParameterArray[1].Value = isIsLamic;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];

            List<amoutdata> dd = new List<amoutdata>();


            //tbl.AsEnumerable().ToList().ForEach(s => { labels.Add(s["FUND_CODE"].ToString()); });


            string[] colr = { "#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990" };

            Random rd = new Random(3);
            //tbl.AsEnumerable().ToList().foreach (d => {

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                dd.Add(new amoutdata
                {
                    y =
                    decimal.Round(Convert.ToDecimal(tbl.Rows[i]["AMOUNT"].ToString()), 2, MidpointRounding.AwayFromZero),
                    color = colr[i],
                    name = tbl.Rows[i]["FUND_NAME"].ToString(),
                    //key = tbl.Rows[i]["TOTAL_UNITS"].ToString(),


                });
            }




            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            profiledetaildata = serializer.Serialize(dd);

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
    private void profiledetail(string fundcode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();

        try
        {


            tbl = tblProfiledetail(fundcode);
            //List<object> iData = new List<object>();
            //List<string> labels = new List<string>();
            //List<getdata> data = new List<getdata>();

            //amoutdata getdata1 = new amoutdata();
            //List<decimal> items2 = new List<decimal>();
            //List<getdata> items = new List<getdata>();
            List<amoutdata> dd = new List<amoutdata>();

            //var chartData = new object[tbl.Rows.Count + 1];
            //List<decimal> lst_dataItem_1 = new List<decimal>();
            //List<decimal> lst_dataItem_2 = new List<decimal>();
            //int j = 0;
            //foreach (DataRow drow in tbl.Rows)
            //{

            //    labels.Add(drow["TYPE"].ToString());
            //    //lst_dataItem_1.Add(Convert.ToDecimal(drow["SUBMISSION_AMT"].ToString()));
            //    //chartData[j] = new object[] { "{" + "name:" + drow["YEAR"].ToString(), "data:" + Convert.ToDecimal(drow["SUBMISSION_AMT"].ToString()) +"}" };
            //    j++;
            //}

            string[] colr = { "#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990" };

            for (int i = 0; i < tbl.Rows.Count; i++)
            {

                dd.Add(new amoutdata
                {
                    y =
                    decimal.Round(Convert.ToDecimal(tbl.Rows[i]["COST"].ToString()), 2, MidpointRounding.AwayFromZero),
                    color = colr[i],
                    name = tbl.Rows[i]["SCRIPT"].ToString(),
                    key = tbl.Rows[i]["'FIS'"].ToString(),


                });



            }


            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //profiledetaildata = serializer.Serialize(dd);
            // ChartLable = serializer.Serialize(labels);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {

        }


    }
    //private void getPortfolioRet2()
    //{
    //    DataTable tbl = new DataTable();
    //    DataTable tblmain = new DataTable();

    //    try
    //    {

    //        string qry = @"SELECT  FUND_CODE, FUND_NAME, TYPE, INCEPTION FROM  PortfolioReturn WHERE  (FUND_CODE = 00100)";
    //        tbl = DALSqlServer.ExecuteTable(qry);

    //        List<string> labels = new List<string>();
    //        List<bardata> dd = new List<bardata>();


    //        tbl.AsEnumerable().ToList().ForEach(s => { labels.Add(s["TYPE"].ToString()); });


    //        string[] colr = { "#011d5b", "#1c4ec5", "#403ec7" };

    //        Random rd = new Random(3);
    //        tbl.AsEnumerable().ToList().ForEach(d => {
    //            dd.Add(new bardata
    //            {
    //                y = decimal.Round(Convert.ToDecimal(d["INCEPTION"].ToString()), 2, MidpointRounding.AwayFromZero),
    //                drilldown = d["type"].ToString(),
    //                name = d["type"].ToString(),
    //                color = colr[rd.Next(0, 3)]


    //            });

    //        });

    //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

    //        barchartdata = serializer.Serialize(dd);
    //        BarChartLable = serializer.Serialize(labels);
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //    finally
    //    {

    //    }


    //}
    public decimal PERCENTAGE
    {
        set { ViewState["PERCENTAGE"] = value; }
        get { return Convert.ToDecimal(ViewState["PERCENTAGE"]); }
    }

    private DataTable getPortfolioRet2(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"SELECT VDD.FUND_CODE,
       VDD.FUND_SHORT_NAME,
       MAX(VDD.UNITS) AS UNITS,
       TO_CHAR(VDD.DATED, 'MON') AS MON,
       SUM(vdd.balance) AS balance,
       TO_CHAR(VDD.DATED, 'FMMM') AS MONTHNUMBER
  FROM VPS_DASHBOARD_DETAILS VDD
 WHERE VDD.Folio_Number = :folio_number
   AND EXTRACT(YEAR FROM VDD.DATED) = EXTRACT(YEAR FROM SYSDATE)
 GROUP BY VDD.DATED, VDD.FUND_CODE, VDD.FUND_SHORT_NAME
 ORDER BY VDD.DATED ASC";


            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];
            int count;
            int rowcount = tbl.Rows.Count;
            int monthnumber = Convert.ToInt32(tbl.Rows[rowcount - 1]["MONTHNUMBER"]);
            count = Convert.ToInt32(tbl.Rows[0]["MONTHNUMBER"]);

            List<string> labels = new List<string>();
            List<decimal> BalanceEnquiryMM = new List<decimal>();
            List<decimal> BalanceEnquiryEQ = new List<decimal>();
            List<decimal> BalanceEnquiryDB = new List<decimal>();
            List<decimal> UnitsEnquiry = new List<decimal>();
            List<bardata2> dd = new List<bardata2>();

            while (count > 1)
            {
                BalanceEnquiryMM.Add(Convert.ToDecimal(0));
                BalanceEnquiryEQ.Add(Convert.ToDecimal(0));
                BalanceEnquiryDB.Add(Convert.ToDecimal(0));

                count--;
            }

            tbl.AsEnumerable().ToList().ForEach(s => {
                if (s["Fund_CODE"].ToString() == "00032" || s["Fund_CODE"].ToString() == "00038")
                {
                    BalanceEnquiryMM.Add(Convert.ToDecimal(s["BALANCE"]));
                }
                if (s["Fund_CODE"].ToString() == "00030" || s["Fund_CODE"].ToString() == "00036")
                {
                    BalanceEnquiryEQ.Add(Convert.ToDecimal(s["BALANCE"]));
                }
                if (s["Fund_CODE"].ToString() == "00031" || s["Fund_CODE"].ToString() == "00037")
                {
                    BalanceEnquiryDB.Add(Convert.ToDecimal(s["BALANCE"]));
                }
            });

            tbl.AsEnumerable().ToList().ForEach(s => { UnitsEnquiry.Add(Convert.ToDecimal(s["Units"])); });
            while (monthnumber < 12)
            {
                BalanceEnquiryMM.Add(Convert.ToDecimal(0));
                BalanceEnquiryEQ.Add(Convert.ToDecimal(0));
                BalanceEnquiryDB.Add(Convert.ToDecimal(0));
                monthnumber++;
            }

            //tbl.AsEnumerable().ToList().ForEach(s => { BalanceEnquiry.Add(Convert.ToDecimal(s["BALANCE"])); });
            balanceArray1 = JsonConvert.SerializeObject(BalanceEnquiryMM);//UnitsEnquiry.ToArray();
            balanceArray2 = JsonConvert.SerializeObject(BalanceEnquiryEQ);
            balanceArray3 = JsonConvert.SerializeObject(BalanceEnquiryDB);

            string[] colr = { "#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990" };

            Random rd = new Random(3);

            //tbl.AsEnumerable().ToList().ForEach (d => {
            dd.Add(new bardata2
            {

                data = datavalueport(tbl),
                data2 = datavalueportunits(tbl),
                name = "BALANCE",
                color = colr[rd.Next(0, 5)],
            });
            //});
            //dd.Add(new bardata2
            //{
            //    data = datavaluebench(tbl),
            //    name = "benchmark return",
            //    color = colr[rd.Next(0, 5)]

            //});

            //});

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            barchartdata = serializer.Serialize(dd);
            BarChartLable = serializer.Serialize(UnitsEnquiry);

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
    private DataTable getPortfolioRet(string folioNumber)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            string qry = @"
 WITH date_series AS
 (SELECT ADD_MONTHS(TRUNC(sysdate, 'MM'), -LEVEL + 1) AS dt
    FROM dual
  CONNECT BY LEVEL <= (select extract(month from sysdate) from dual)),
q1_results AS
 (SELECT DISTINCT u.fund_code, u.folio_number
    FROM unit_balance_electronic u
   WHERE folio_number = :folio_number),
q2_max_dates AS
 (SELECT last_day(dt) AS max_date_of_month,
         TO_CHAR(dt, 'MON') AS month,
         TO_CHAR(dt, 'YYYY') AS year,
         LEAD(last_day(dt)) OVER(ORDER BY dt) AS next_max_date
    FROM date_series)
SELECT q1.fund_code,
       q1.folio_number,
       q2.max_date_of_month,
       q2.month,
       q2.year,
       TO_CHAR(get_fund_folio_balance(q2.max_date_of_month,
                              q1.fund_code,
                              q1.folio_number)*get_fund_nav(q2.max_date_of_month,q1.fund_code), '999,999,999.99') AS balance,
       get_fund_folio_balance(q2.max_date_of_month,
                              q1.fund_code,
                              q1.folio_number) AS Units
                              
  FROM q1_results q1
 CROSS JOIN q2_max_dates q2
 WHERE q2.next_max_date IS NULL
    OR q2.next_max_date > q2.max_date_of_month
 ORDER BY q2.max_date_of_month, q1.fund_code,q1.folio_number
";

            //string qry = "select QUERIES from jsil_investor_queries j where id = 2";
            //            string qry = @"
            //WITH date_series AS (
            //  SELECT ADD_MONTHS(TRUNC(sysdate, 'MM'), -LEVEL + 1) AS dt
            //  FROM dual
            //  CONNECT BY LEVEL <= 13
            //),
            //q1_results AS (
            //  SELECT DISTINCT u.fund_code, u.folio_number
            //  FROM unit_balance_electronic u
            //  WHERE folio_number = :folio_number
            //),
            //q2_max_dates AS (
            //  SELECT
            //    last_day(dt) AS max_date_of_month,
            //    TO_CHAR(dt, 'MON') AS month,
            //    TO_CHAR(dt, 'YYYY') AS year,
            //    LEAD(last_day(dt)) OVER (ORDER BY dt) AS next_max_date
            //  FROM date_series
            //)
            //SELECT
            //  q1.fund_code,
            //  q1.folio_number,
            //  q2.max_date_of_month,
            //  q2.month,
            //  q2.year,
            //  get_fund_folio_balance(q2.max_date_of_month, q1.fund_code, q1.folio_number) AS balance
            //FROM q1_results q1
            //CROSS JOIN q2_max_dates q2
            //WHERE q2.next_max_date IS NULL OR q2.next_max_date > q2.max_date_of_month
            //ORDER BY q2.max_date_of_month DESC, q1.fund_code, q1.folio_number;

            //";


            //object query = DAL.OracleDataAccess.ExecuteScalar(cn.GetConnection(), CommandType.Text, qry);

            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":folio_Number", OracleType.VarChar);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            oracleParameterArray[0].Value = folioNumber;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry.ToString(), oracleParameterArray).Tables[0];


            List<string> labels = new List<string>();
            List<bardata2> dd = new List<bardata2>();


            tbl.AsEnumerable().ToList().ForEach(s => { labels.Add(s["MONTH"].ToString()); });


            string[] colr = { "#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990" };

            Random rd = new Random(3);

            //tbl.AsEnumerable().ToList().ForEach (d => {
            dd.Add(new bardata2
            {

                data = datavalueport(tbl),
                data2 = datavalueportunits(tbl),
                name = "BALANCE",
                color = colr[rd.Next(0, 5)],
            });
            //});
            //dd.Add(new bardata2
            //{
            //    data = datavaluebench(tbl),
            //    name = "benchmark return",
            //    color = colr[rd.Next(0, 5)]

            //});

            //});

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            barchartdata = serializer.Serialize(dd);
            BarChartLable = serializer.Serialize(labels);

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

    public List<decimal> profilevalueport(DataRow[] dtr)
    {

        List<decimal> items = new List<decimal>();

        for (int i = 0; i < dtr.Length; i++)
        {
            items.Add(
           Convert.ToDecimal(dtr[i]["PORTFOLIO_RETURN"])
        );
        }



        return items;
    }
    public List<decimal> AllocationReport(DataTable tbl)
    {

        List<decimal> items = new List<decimal>();

        for (int i = 0; i < tbl.Rows.Count; i++)
        {
            items.Add(
           Convert.ToDecimal(tbl.Rows[i]["BALANCE"])

        );
        }



        return items;
    }

    public List<decimal> datavalueportunits(DataTable tbl)
    {

        List<decimal> items = new List<decimal>();

        for (int i = 0; i < tbl.Rows.Count; i++)
        {

            items.Add(
           Convert.ToDecimal(tbl.Rows[i]["UNITS"])
        );
        }



        return items;
    }
    public List<decimal> datavalueport(DataTable tbl)
    {

        List<decimal> items = new List<decimal>();

        for (int i = 0; i < tbl.Rows.Count; i++)
        {
            items.Add(
           Convert.ToDecimal(tbl.Rows[i]["BALANCE"])
        );
        }
        


        return items;
    }

    public List<decimal> datavaluebench(DataTable tbl)
    {

        List<decimal> items = new List<decimal>();
        for (int i = 0; i < tbl.Rows.Count; i++)
        {
            items.Add(
           Convert.ToDecimal(tbl.Rows[i]["BALANCE"])
        );
        }

        return items;
    }
    public class barprofiledetail
    {
        List<decimal> Lst = new List<decimal>();
        public List<decimal> data

        {

            get

            {

                return Lst;

            }

            set

            {

                Lst = value;

            }



        }

        public string name, color;

    }
    public class bardata2
    {
        List<decimal> Lst = new List<decimal>();
        public List<decimal> data

        {

            get

            {

                return Lst;

            }

            set

            {

                Lst = value;

            }



        }
        List<decimal> Lst2 = new List<decimal>();
        public List<decimal> data2

        {

            get

            {

                return Lst2;

            }

            set

            {

                Lst2 = value;

            }



        }

        public string name, color;

    }
    private DataTable benchMarkdata(string fundcode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
          
                    string qry = @"with Equity as

                    (SELECT equity.PRICE_DATE,
                    F.FUND_SHORT_NAME as fundshortname,
                    equity.FUND_CODE fundCode,
                    F.FUND_NAME as fundName,
                    equity.SYMBOL,
                    (select distinct (sector_name)
                    from sector
                    where sector_code = (select sector_code
                    from security
                    where SYMBOL = equity.symbol)) sector,
         
                    CASE
                    WHEN equity.HFT_VOLUME = 0 THEN
                    equity.AFS_VOLUME
                    ELSE
                    equity.HFT_VOLUME
                    END as VOLUME,
                    CASE
                    WHEN equity.HFT_BLOCKED_VOLUME = 0 THEN
                    equity.AFS_BLOCKED_VOLUME
                    ELSE
                    equity.HFT_BLOCKED_VOLUME
                    END BLOCKED_VOLUME,
         
                    0 as avgPrice,
         
                    (select em.close_rate
                    from equity_market em
                    where em.symbol = equity.SYMBOL
                    and em.price_date = equity.PRICE_DATE) as closingRate,
         
                    0 as gainLoss,
                    ((CASE
                    WHEN equity.HFT_VOLUME = 0 THEN
                    equity.AFS_VOLUME
                    ELSE
                    equity.HFT_VOLUME
                    END) * (select em.close_rate
                    from equity_market em
                    where em.symbol = equity.SYMBOL
                    and em.price_date = equity.PRICE_DATE)) as marketValue,
         
                    CASE
                    WHEN equity.HFT_MARK_TO_MKT_VALUE = 0 THEN
                    equity.AFS_MARK_TO_MKT_VALUE
                    ELSE
                    equity.HFT_MARK_TO_MKT_VALUE
                    END as markToMKTValue,
         
                    CASE
                    WHEN equity.hft_historical_value = 0 THEN
                    equity.afs_historical_value
                    ELSE
                    equity.hft_historical_value
                    END as historicalValue,
         
                    (select paidup_capital from security where SYMBOL = equity.symbol) paidup,
                    N.NET_ASSETS AS netAsset
  
                    FROM EQUITY_PORTFOLIO equity, FUND F, NET_ASSETS_BEFORE_FEE N
                    WHERE F.fund_code = equity.FUND_CODE
                    AND equity.PRICE_DATE =
                    (SELECT MAX(PRICE_DATE) - 1
                    FROM EQUITY_PORTFOLIO
                    WHERE FUND_CODE = EQUITY.FUND_CODE)
                    AND N.FUND_CODE = F.FUND_CODE
                    AND N.PRICE_DATE = equity.PRICE_DATE
                    and equity.fund_code = :fund_code
                    AND equity.SYMBOL NOT IN ('JSGF')
                    ORDER BY PRICE_DATE),

                    FIS as
                    (SELECT FIS.PRICE_DATE,
                    F.FUND_SHORT_NAME as fundshortname,
                    FIS.FUND_CODE fundCode,
                    F.FUND_NAME as fundName,
                    S.SCHEME_NAME AS SYMBOL,
                    'SECTOR' AS SECTOR,
         
                    CASE
                    WHEN FIS.HFT_VOLUME = 0 THEN
                    FIS.AFS_VOLUME
                    ELSE
                    FIS.HFT_VOLUME
                    END as VOLUME,
                    CASE
                    WHEN FIS.HFT_BLOCKED_VOLUME = 0 THEN
                    FIS.AFS_BLOCKED_VOLUME
                    ELSE
                    FIS.HFT_BLOCKED_VOLUME
                    END BLOCKED_VOLUME,
         
                    0 as avgPrice,
         
                    (SELECT RATE
                    FROM FIS_MARKET_RATES FR
                    WHERE FR.SCHEME_CODE = S.SCHEME_CODE
                    AND FR.PRICE_DATE = FIS.PRICE_DATE) as closingRate,
         
                    0 as gainLoss,
                    ((CASE
                    WHEN FIS.HFT_VOLUME = 0 THEN
                    FIS.AFS_VOLUME
                    ELSE
                    FIS.HFT_VOLUME
                    END) * (SELECT RATE
                    FROM FIS_MARKET_RATES FR
                    WHERE FR.SCHEME_CODE = S.SCHEME_CODE
                    AND FR.PRICE_DATE = FIS.PRICE_DATE)) as marketValue,
         
                    CASE
                    WHEN FIS.HFT_MARK_TO_MARKET_VALUE = 0 THEN
                    FIS.AFS_MARK_TO_MARKET_VALUE
                    ELSE
                    -FIS.HFT_MARK_TO_MARKET_VALUE
                    END as markToMKTValue,
         
                    CASE
                    WHEN FIS.hft_historical_value = 0 THEN
                    FIS.afs_historical_value
                    ELSE
                    FIS.hft_historical_value
                    END as historicalValue,
         
                    --   (select paidup_capital from security where SYMBOL = FIS.symbol) paidup,
                    N.NET_ASSETS AS netAsset
  
                    FROM fis_PORTFOLIO FIS, FUND F, NET_ASSETS_BEFORE_FEE N, SCHEME S
                    WHERE F.fund_code = FIS.FUND_CODE
                    AND FIS.PRICE_DATE =
                    (SELECT MAX(PRICE_DATE) - 1
                    FROM FIS_PORTFOLIO
                    WHERE FUND_CODE = FIS.FUND_CODE)
                    AND N.FUND_CODE = F.FUND_CODE
                    AND N.PRICE_DATE = FIS.PRICE_DATE
                    AND FIS.SCHEME_CODE = S.SCHEME_CODE
                    and fis.fund_code = :fund_code
                    AND S.SCHEME_NAME NOT IN ('JSGF'))

                    select FIS.symbol as Script,
       
                    round(FIS.historicalValue, 2) as Cost

                    from FIS
                    where FIS.symbol not like 'JSGF%'

                    union all

                    select equity.symbol as Script, round(equity.historicalValue, 2) as Cost
                    from equity
                    where equity.symbol not like 'JSGF%'";
            OracleParameter[] oracleParameterArray = new OracleParameter[1];
            oracleParameterArray[0] = new OracleParameter(":fund_code", (object)fundcode);
            oracleParameterArray[0].Direction = ParameterDirection.Input;
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry, oracleParameterArray).Tables[0];

            List<string> labels = new List<string>();
            List<bardata> dd = new List<bardata>();


            //tbl.AsEnumerable().ToList().ForEach(s => { labels.Add(s["TYPE"].ToString()); });


            string[] colr = { "#011d5b", "#1c4ec5", "#403ec7" };

            Random rd = new Random(3);
            tbl.AsEnumerable().ToList().ForEach(d => {
                dd.Add(new bardata
                {
                    y = decimal.Round(Convert.ToDecimal(d["COST"].ToString()), 2, MidpointRounding.AwayFromZero),
                    drilldown = d["SCRIPT"].ToString(),
                    name = d["SCRIPT"].ToString(),
                    color = colr[rd.Next(0, 3)]


                });

            });

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            benchmarkdata = serializer.Serialize(dd);
            //BarChartLable = serializer.Serialize(labels);

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

    public string barchartdata
    {
        set { ViewState["barchartdata"] = value; }
        get { return Convert.ToString(ViewState["barchartdata"]); }
    }

    public string benchmarkdata
    {
        set { ViewState["benchmarkdata"] = value; }
        get { return Convert.ToString(ViewState["benchmarkdata"]); }
    }

    public string BarChartLable
    {
        set { ViewState["BarChartLable"] = value; }
        get { return Convert.ToString(ViewState["BarChartLable"]); }
    }

    public string Chartdata
    {
        set { ViewState["Chartdata"] = value; }
        get { return Convert.ToString(ViewState["Chartdata"]); }
    }

    public string profiledetaildata
    {
        set { ViewState["profiledetaildata"] = value; }
        get { return Convert.ToString(ViewState["profiledetaildata"]); }
    }

    public string ChartLable
    {
        set { ViewState["ChartLable"] = value; }
        get { return Convert.ToString(ViewState["ChartLable"]); }
    }

    public class amoutdata
    {
        public decimal y;
        public string name, color,key;

    }
    public class bardata
    {
        public decimal y;
        public string name, drilldown, color;

    }

    
    //public class bardata2
    //{
    //    public decimal data;
    //    public string name, color;

    //}

    public class getdata
    {

        List<amoutdata> Lst = new List<amoutdata>();
        public List<amoutdata> data

        {

            get

            {

                return Lst;

            }

            set

            {

                Lst = value;

            }



        }


    }

    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
       
        string filepath = Server.MapPath("Reports//") + e.CommandArgument;
        string filename = Path.GetFileName(filepath);
        FileInfo file1 = new FileInfo(filepath);

        Response.ClearContent();
        Response.ContentType = "application/pdf";
        //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + file1.Name);
        Response.AddHeader("Content-Length", file1.Length.ToString());
        Response.AppendHeader("Content-disposition", "attachment; filename=" + file1.Name);
        Response.WriteFile(filepath);
    }

    protected void allocationDetail_PreRender(object sender, EventArgs e)
    {
        if (allocationDetail.Rows.Count > 0)
        {
            allocationDetail.UseAccessibleHeader = true;
            allocationDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        //GridViewRow LastRow = allocationDetail.Rows[allocationDetail.Rows.Count - 1];

        
        //LastRow.Font.Bold = true;
        
    }
    protected void InvestmentCertificate(string folionumber)
    {
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            string filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_001.rdf+userid=erp_live/pass1@ABAMCO-DB+PFOLIONUMBER="+ folionumber +"+desformat=pdf+destype=cache";
            System.Net.WebClient webClinent = new System.Net.WebClient();
            byte[] fileContent = webClinent.DownloadData(filename);

            Response.Clear();
            MemoryStream ms = new MemoryStream(fileContent);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=InvestmentCertificate.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
    }
    protected void TaxCertificate(string folionumber)
    {
        try
        {

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            string filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_002.rdf+userid=erp_live/pass1@ABAMCO-DB+PFOLIONUMBER=" + folionumber + "+desformat=pdf+destype=cache";
            System.Net.WebClient webClinent = new System.Net.WebClient();
            byte[] fileContent = webClinent.DownloadData(filename);

            Response.Clear();
            MemoryStream ms = new MemoryStream(fileContent);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AccountStatement.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
    }
    protected void AccountStatementReport(string folionumber)
    {
        try
        {

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            string filename;
            //string filename = "http://10.142.200.178:7778/reports/rwservlet?report=ACCT_STAT.rdf+userid=erp_sma/ERPSMA@ERP_SMA+PFUNDCODE=" + Session["FUNDCODE"].ToString() + "+desformat=pdf+destype=cache";
            if (folionumber.Contains("-PF"))
            {
                filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_004.rdf+userid=erp_live/pass1@ABAMCO-DB+PFOLIONUMBER=" + folionumber + "+desformat=pdf+destype=cache";
            }
            else
            {
                filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_003.rdf+userid=erp_live/pass1@ABAMCO-DB+PFOLIONUMBER=" + folionumber + "+desformat=pdf+destype=cache";
            }
            System.Net.WebClient webClinent = new System.Net.WebClient();
            byte[] fileContent = webClinent.DownloadData(filename);

            Response.Clear();
            MemoryStream ms = new MemoryStream(fileContent);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Account_Statement_Report.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
    }
    

    protected void btnFMRSubmit_Click(object sender, EventArgs e)
    {
        try
        {

       
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        ServicePointManager.DefaultConnectionLimit = 9999;
            string filename =Server.MapPath("reports")+ "\\" + ddlFMRList.SelectedItem.Value;

        //Response.ContentType = "Application/pdf";
        //Response.AppendHeader("Content-Disposition", "attachment; filename=FMR.pdf");
        //Response.TransmitFile(filename);
        //Response.End();

        System.Net.WebClient webClinent = new System.Net.WebClient();
        byte[] fileContent = webClinent.DownloadData(filename);

        Response.Clear();
        MemoryStream ms = new MemoryStream(fileContent);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=FMR_Report.pdf");
        Response.Buffer = true;
        ms.WriteTo(Response.OutputStream);
        Response.End();

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }

    }

    protected void btnAggrementSubmit_Click(object sender, EventArgs e)
    {
        InvestmentCertificate(Session["FolioNo"].ToString());
    }

    protected void btnAccountStatementReport_Click(object sender, EventArgs e)
    {
        AccountStatementReport(Session["FolioNo"].ToString());
    }

    protected void btnTransStatReport_Click(object sender, EventArgs e)
    {
        TaxCertificate(Session["FolioNo"].ToString());
    }

    protected void TransactionView_PreRender(object sender, EventArgs e)
    {
        if (TransactionView.Rows.Count > 0)
        {
            TransactionView.UseAccessibleHeader = true;
            TransactionView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    // recent update by saad //
    protected void BindGridView()
    {
        string folioNumber = Session["FolioNo"].ToString(); // Replace with your actual employer ID
        DataTable dt = TransactionSummary(folioNumber);
        TransactionView.DataSource = dt;
        TransactionView.DataBind();
        // saad commented it  UpdatePanel1.Update(); // Trigger update of the UpdatePanel

    }

    protected void TransactionView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        TransactionView.PageIndex = e.NewPageIndex;
        BindGridView();
    }


    // recent update by saad //

    protected void accType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (accType.SelectedItem.Value == "Islamic")
        {
            PortfolioAllocationCustom(Session["FolioNo"].ToString(),"1");
        }
        else
        {
            PortfolioAllocationCustom(Session["FolioNo"].ToString(),"1");
        }
    }

    protected void SubmitInvestment_Click(object sender, EventArgs e)
    {
        try
        {

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            string startDate = JSFunctions.dateFormat(StartDateInvestment.Text);
            string endDate = JSFunctions.dateFormat(EndDateInvestment.Text);
            string filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_005.rdf+userid=erp_live/pass1@ABAMCO-DB+REP_FIELD_01=" + startDate + "+REP_FIELD_02=" + endDate + "+PFOLIONUMBER=" + Session["FolioNo"].ToString() + "+desformat=pdf+destype=cache";
            //string filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_002.rdf+userid=erp_live/pass1@ABAMCO-DB+PFOLIONUMBER=" + folionumber + "+desformat=pdf+destype=cache";
            System.Net.WebClient webClinent = new System.Net.WebClient();
            byte[] fileContent = webClinent.DownloadData(filename);

            Response.Clear();
            MemoryStream ms = new MemoryStream(fileContent);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=InvestmentCertificate.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
    }

    protected void SubmitTax_Click(object sender, EventArgs e)
    {
        try
        {

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            string startDate = JSFunctions.dateFormat(StartDateInvestment.Text);
            string endDate = JSFunctions.dateFormat(EndDateInvestment.Text);
            string filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_007.rdf+userid=erp_live/pass1@ABAMCO-DB+REP_FIELD_01=" + startDate + "+REP_FIELD_02=" + endDate + "+PFOLIONUMBER=" + Session["FolioNo"].ToString() + "+desformat=pdf+destype=cache";
            //string filename = "http://10.142.200.178:7778/reports/rwservlet?report=REP_002.rdf+userid=erp_live/pass1@ABAMCO-DB+PFOLIONUMBER=" + folionumber + "+desformat=pdf+destype=cache";
            System.Net.WebClient webClinent = new System.Net.WebClient();
            byte[] fileContent = webClinent.DownloadData(filename);

            Response.Clear();
            MemoryStream ms = new MemoryStream(fileContent);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AccountStatement.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
    }
}