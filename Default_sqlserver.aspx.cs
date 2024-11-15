using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default_sqlserver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            grdTransSummary.PreRender += new EventHandler(GridView1_PreRender);
            grdTransSummary.DataSource = getTransSummary("00093");
            grdTransSummary.DataBind();
            getPortfolioRet();
            getPortfolioAllocation();
        }
    }

    void GridView1_PreRender(object sender, EventArgs e)
    {
        if (grdTransSummary.Rows.Count > 0)
        {
            grdTransSummary.UseAccessibleHeader = true;
            grdTransSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private DataTable getData(string FundCode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
       
        try
        {



            string qry = "select query_text from jsil_queries where id=1";
            
           
          tbl =  DALSqlServer.ExecuteTable(qry);

           


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

    private DataTable getTransSummary(string FundCode)
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
      
        try
        {



           
                    string qry = @"SELECT   SALE_DATE, FUND_SHORT_NAME, TRANSACTION_TYPE, AMOUNT  FROM    TransSummary ";
          
            tbl = DALSqlServer.ExecuteTable(qry.ToString());


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

    private DataTable getProfitLossData()
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
                    WHERE G.gl_form_date <= (SELECT MAX(END_DATE) FROM FINANCIAL_YEARS)
                    and g.fund_code=106)

                    SELECT e.fund_code,
                    'INCEPTION' AS TYPE, 
                   
       
       
                    to_char((((SUM(INCOME_OPEN_CREDIT) - SUM(INCOME_OPEN_DEBIT) +
                    SUM(INCOME_PERIOD_CREDIT) - SUM(INCOME_PERIOD_DEBIT))) -
                    ((SUM(EXPENSE_OPEN_CREDIT) - SUM(EXPENSE_OPEN_DEBIT) +
                    SUM(EXPENSE_PERIOD_CREDIT) - SUM(EXPENSE_PERIOD_DEBIT)))) +
       
                    ((SUM(RESERVES_OPEN_CREDIT) - SUM(RESERVES_OPEN_DEBIT) +
                    SUM(RESERVES_PERIOD_CREDIT) - SUM(RESERVES_PERIOD_DEBIT))),'999,999,999,999') AS INCEPTION

                    FROM EXPENSE E
                    INNER JOIN FUND F
                    ON F.FUND_CODE = E.FUND_CODE
                    inner join unit_nav u
                    on u.fund_code = e.fund_code
                    and u.price_date = (SELECT MAX(START_DATE) FROM FINANCIAL_YEARS F)
                    GROUP BY e.fund_code, F.FUND_NAME
                    --ORDER BY F.FUND_NAME
 
                    union all
 

                    SELECT e.fund_code,
                    'FISCAL YEAR',
                    to_char(((SUM(INCOME_OPEN_CREDIT) - SUM(INCOME_OPEN_DEBIT) +
                    SUM(INCOME_PERIOD_CREDIT) - SUM(INCOME_PERIOD_DEBIT))) -
                    ((SUM(EXPENSE_OPEN_CREDIT) - SUM(EXPENSE_OPEN_DEBIT) +
                    SUM(EXPENSE_PERIOD_CREDIT) - SUM(EXPENSE_PERIOD_DEBIT))),'999,999,999,999') as FINANCIAL_YEAR
                    FROM EXPENSE E
                    INNER JOIN FUND F
                    ON F.FUND_CODE = E.FUND_CODE
                    inner join unit_nav u
                    on u.fund_code = e.fund_code
                    and u.price_date = (SELECT MAX(START_DATE) FROM FINANCIAL_YEARS F)
                    GROUP BY e.fund_code, F.FUND_NAME
                    --ORDER BY F.FUND_NAME

                    union all

                    SELECT e.fund_code,
                    'MONTHLY',
       
                    to_char((SUM(E.MINCOME_PERIOD_CREDIT) - SUM(e.MINCOME_PERIOD_DEBIT)) -
                    (SUM(E.MEXPENSE_PERIOD_DEBIT) - SUM(E.MEXPENSE_PERIOD_CREDIT)),'999,999,999,999') MONTHLY

                    FROM EXPENSE E
                    INNER JOIN FUND F
                    ON F.FUND_CODE = E.FUND_CODE
                    inner join unit_nav u
                    on u.fund_code = e.fund_code
                    and u.price_date = (SELECT MAX(START_DATE) FROM FINANCIAL_YEARS F)
                    GROUP BY e.fund_code, F.FUND_NAME";


          
            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(), CommandType.Text, qry).Tables[0];
            grdProfitLoss.DataSource = tbl;
            grdProfitLoss.DataBind();

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

    public decimal PERCENTAGE
    {
        set { ViewState["PERCENTAGE"] = value; }
        get { return Convert.ToDecimal(ViewState["PERCENTAGE"]); }
    }


    public string barchartdata
    {
        set { ViewState["barchartdata"] = value; }
        get { return Convert.ToString(ViewState["barchartdata"]); }
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

    public string ChartLable
    {
        set { ViewState["ChartLable"] = value; }
        get { return Convert.ToString(ViewState["ChartLable"]); }
    }

    private void getPortfolioAllocation()
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();

        try
        {

            string qry = @"SELECT    FUND_CODE, FUND_NAME, TYPE, PRICE_DATE, PERCENTAGE  FROM    PortfolioAllocation";
            tbl = DALSqlServer.ExecuteTable(qry);
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
            string[] colr = { "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6" };

            for (int i = 0; i < tbl.Rows.Count; i++)
            {

                dd.Add(new amoutdata
                {
                    y =
                    decimal.Round(Convert.ToDecimal(tbl.Rows[i]["PERCENTAGE"].ToString()), 2, MidpointRounding.AwayFromZero),
                    color = colr[i],
                    name = tbl.Rows[i]["FUND_NAME"].ToString()


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
    private void getPortfolioRet()
    {
        DataTable tbl = new DataTable();
        DataTable tblmain = new DataTable();
       
        try
        {

             string qry = @"SELECT  FUND_CODE, FUND_NAME, TYPE, INCEPTION FROM  PortfolioReturn WHERE  (FUND_CODE = 00100)";
            tbl = DALSqlServer.ExecuteTable(qry);
            
            List<string> labels = new List<string>();
            List<bardata> dd = new List<bardata>();
           

            tbl.AsEnumerable().ToList().ForEach(s => { labels.Add(s["TYPE"].ToString()); });


            string[] colr = { "#011d5b", "#1c4ec5" , "#403ec7" };

            Random rd = new Random(3);
            tbl.AsEnumerable().ToList().ForEach(d => {
                dd.Add(new bardata
                {
                    y = decimal.Round(Convert.ToDecimal(d["INCEPTION"].ToString()), 2, MidpointRounding.AwayFromZero),
                    drilldown = d["type"].ToString(),
                    name = d["type"].ToString(),
                    color = colr[rd.Next(0,3)]


                });

            });

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
           
        }

      
    }
    public class amoutdata
    {
        public decimal y;
        public string name, color;

    }
    public class bardata
    {
        public decimal y;
        public string name, drilldown,color;

    }
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