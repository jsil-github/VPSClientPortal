using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.Params["type"] ==null && Request.Params["code"] == null)
        {
            return;
        }

        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
        string json = "";
        try
        {


            tbl = tblProfiledetailJson(Request.Params["code"].ToString(), Request.Params["type"].ToString());
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
                    key = tbl.Rows[i]["FIS_EQUITY"].ToString(),


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
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(json);
    }

       
    private DataTable tblProfiledetailJson(string Fundcode, string fis)
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

                    SELECT FIS_EQUITY , Script,Cost  FROM (
                    select 'FIS' FIS_EQUITY, FIS.symbol as Script,
       
                    round(FIS.historicalValue, 2) as Cost

                    from FIS
                    where FIS.symbol not like 'JSGF%'

                    union all

                    select 'Equity', equity.symbol as Script, round(equity.historicalValue, 2) as Cost
                    from equity
                    where equity.symbol not like 'JSGF%')
                    WHERE  upper(FIS_EQUITY) = :FIS_EQUITY";
            OracleParameter[] oracleParameterArray = new OracleParameter[2];
            oracleParameterArray[0] = new OracleParameter(":fund_code", (object)Fundcode);
            oracleParameterArray[0].Direction = ParameterDirection.Input;

            oracleParameterArray[1] = new OracleParameter(":FIS_EQUITY", (object)fis);
            oracleParameterArray[1].Direction = ParameterDirection.Input;


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
        public string name, color, key;

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

   
    
}