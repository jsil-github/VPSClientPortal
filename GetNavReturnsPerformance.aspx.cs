using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetNavReturnsPerformance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.PreRender += new EventHandler(GridView1_PreRender);
        GridView1.DataSource = GetAllNAV();
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

    protected void getNavReturn()
    {

        try
        {


            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            string url = "https://www.mufap.com.pk/nav_returns_performance.php?tab=01";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //request.Method = "POST";
            request.Method = "Get";
            request.KeepAlive = true;
            request.ContentType = "text/html";
            var response = request.GetResponse();

            string text;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();

            }
            byte[] pdfBuffer = null;
            if (response != null)
            {

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(text);
                string marketName = "";
                string FundName = "", Rating = "", ValidityDate = "", NAV = "", YTD = "", MTD = "", OneDay = "", FifteenDay = "", ThirteenDay = "", NintyDay = "", OneEightDay = "", TwoSeventyDay = "", ThreeSixtyFiveDay = "";
                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//table[@id='dataTable']"))
                {

                    HtmlNodeCollection head = row.SelectNodes("thead");
                    for (int i = 0; i < head.Count; ++i)
                    {
                        //Response.Write("Heading : " + head[i].InnerText + "<br>");
                        marketName = head[i].InnerText;
                    }

                    HtmlNodeCollection cells = row.SelectNodes("tr");
                    for (int i = 0; i < cells.Count; ++i)
                    {

                        HtmlNodeCollection cells2 = cells[i].SelectNodes("td");
                        if (cells2 != null)
                        {
                            if (i != 0)
                            {
                                for (int j = 0; j < cells2.Count; ++j)
                                {

                                    if (j == 0)
                                    {
                                        FundName = cells2[j].InnerText;
                                    }
                                    else if (j == 1)
                                    {
                                        Rating = cells2[j].InnerText;
                                    }
                                    else if (j == 2)
                                    {
                                        ValidityDate = cells2[j].InnerText;
                                    }
                                    else if (j == 3)
                                    {
                                        NAV = cells2[j].InnerText;
                                    }
                                    else if (j == 4)
                                    {
                                        YTD = cells2[j].InnerText;
                                    }
                                    else if (j == 5)
                                    {
                                        MTD = cells2[j].InnerText;
                                    }
                                    else if (j == 6)
                                    {
                                        OneDay = cells2[j].InnerText;
                                    }
                                    else if (j == 7)
                                    {
                                        FifteenDay = cells2[j].InnerText;
                                    }
                                    else if (j == 8)
                                    {
                                        ThirteenDay = cells2[j].InnerText;
                                    }
                                    else if (j == 9)
                                    {
                                        NintyDay = cells2[j].InnerText;
                                    }
                                    else if (j == 10)
                                    {
                                        OneEightDay = cells2[j].InnerText;
                                    }
                                    else if (j == 11)
                                    {
                                        TwoSeventyDay = cells2[j].InnerText;
                                    }
                                    else if (j == 12)
                                    {
                                        ThreeSixtyFiveDay = cells2[j].InnerText;
                                    }


                                }

                                SaveNAV(marketName, FundName, Rating, ValidityDate, NAV, YTD, MTD, OneDay, FifteenDay, ThirteenDay, NintyDay, OneEightDay, TwoSeventyDay, ThreeSixtyFiveDay);
                            }

                        }

                    }
                }

            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
           // JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
        }
        finally
        {

        }
    }

    private DataTable GetAllNAV()
    {
        DataTable tbl = new DataTable();
        try
        {

            string qry = "Select * from NavReturnsPerformance";
            tbl = DALSqlServer.ExecuteTable(qry);

        }
        catch (Exception ex)
        {

            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
        }
        return tbl;

    }
    private void deleteNAV()
    {

        try
        {

            string qry = "delete from NavReturnsPerformance";
            DALSqlServer.Execute(qry);

        }
        catch (Exception ex)
        {

            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
        }

    }
    private void SaveNAV(string MarketName, string FundName, string Rating, string ValidityDate, string NAV, string YTD, string MTD, string OneDay, string FifteenDay, string ThirteenDay, string NintyDay, string OneEightDay, string TwoSeventyDay, string ThreeSixtyFiveDay)
    {
        
        try
        {

           
          string  qry = @"insert into NavReturnsPerformance ( MarketName, FundName, Rating, ValidityDate, NAV, YTD, MTD, OneDay, FifteenDay, ThirteenDay, NintyDay, OneEightDay, TwoSeventyDay, ThreeSixtyFiveDay, InsertDateTime)
                            values(@MarketName, @FundName, @Rating, @ValidityDate, @NAV, @YTD, @MTD, @OneDay, @FifteenDay, @ThirteenDay, @NintyDay, @OneEightDay, @TwoSeventyDay, @ThreeSixtyFiveDay, getDate())";
            SqlParameter[] p = new SqlParameter[14];
            p[0] = new SqlParameter("@MarketName", MarketName);
            p[1] = new SqlParameter("@FundName", FundName);
            p[2] = new SqlParameter("@Rating", Rating);
            p[3] = new SqlParameter("@ValidityDate", ValidityDate);
            p[4] = new SqlParameter("@NAV", NAV);
            p[5] = new SqlParameter("@YTD", YTD);
            p[6] = new SqlParameter("@MTD", MTD);
            p[7] = new SqlParameter("@OneDay", OneDay);
            p[8] = new SqlParameter("@FifteenDay", FifteenDay);
            p[9] = new SqlParameter("@ThirteenDay", ThirteenDay);
            p[10] = new SqlParameter("@NintyDay", NintyDay);
            p[11] = new SqlParameter("@OneEightDay", OneEightDay);

            p[12] = new SqlParameter("@TwoSeventyDay", TwoSeventyDay);
            p[13] = new SqlParameter("@ThreeSixtyFiveDay", ThreeSixtyFiveDay);
            

            DALSqlServer.Execute(qry, p);

                      

        }
        catch (Exception ex)
        {

            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
        }

    }

    //private List<PhrasalVerb> ExtractVerbsFromMainPage(string content)
    //{
    //    var verbs = new List<PhrasalVerb>(); ;
    //    HtmlDocument doc = new HtmlDocument();
    //    doc.LoadHtml(content);
    //    var rows = doc.DocumentNode.SelectNodes("//table[@class='idioms-table']//tr");
    //    rows.RemoveAt(0); //remove header
    //    foreach (var row in rows)
    //    {
    //        var cols = row.SelectNodes("td");
    //        verbs.Add(new PhrasalVerb
    //        {
    //            Uid = Guid.NewGuid(),
    //            Name = cols[0].InnerHtml,
    //            Definition = cols[1].InnerText,
    //            Count = int.TryParse(cols[2].InnerText, out _) == true ? Convert.ToInt32(cols[2].InnerText) : 0
    //        });
    //    }
    //    return verbs;
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        deleteNAV();
        getNavReturn();
        GridView1.DataSource = GetAllNAV();
        GridView1.DataBind();

    }
}