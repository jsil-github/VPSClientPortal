using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class newsfeeds : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //CsharpBlog.InnerHtml = CshrpCornerLiveUpdateDiv("https://mettisglobal.news/feed/");
    }
    public XElement ReadXmlFromCShrpCornerPage(string CsharpCornerUrl)
    {
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(CsharpCornerUrl);
        WebResponse myResponse;
        myResponse = myRequest.GetResponse();
        XmlDocument _xmlDoc = new XmlDocument();
        using (Stream responseStream = myResponse.GetResponseStream())
        {
            _xmlDoc.Load(responseStream);
        }
        return XElement.Load(new XmlNodeReader(_xmlDoc));
    }

    public string CshrpCornerLiveUpdateDiv(string CsharpCornerUrl)
    {
        
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // .NET 4.0
        StringBuilder sb = new StringBuilder();
        var dox = ReadXmlFromCShrpCornerPage(CsharpCornerUrl);
        //Description about page heading       
        var Headting = dox.Descendants("channel").Select(d =>
        new
        {
            Title = d.Element("title").Value,
            Description = d.Element("description").Value,
            Link = d.Element("link").Value,
        }).ToList();
        //list of daily updated item in c-corner       
        var UpdatedList = dox.Descendants("item").Select(d =>
        new
        {
            Title = d.Element("title").Value,
            comments = d.Element("comments").Value,
            Link = d.Element("link").Value,
            PubDate = d.Element("pubDate").Value,
            description = d.Element("description").Value
            // AuthorName = d.Element("author").Value
        }).ToList();


        foreach (var PageHeading in UpdatedList)
        {

            sb.Append("<div style='margin - bottom:8px;'><h3>" + PageHeading.Title + "</h3><em>" + PageHeading.PubDate + "</em><p></p>");
            sb.Append(PageHeading.description);

        }

        //for (int i = 0; i < 5; i++)//UpdatedList.Count;    
        //{
        //    sb.Append("<div class=\"left\" style=\"outline: 0px; width: 615px; float: clasleft; line-height: 20px;\">< h3 style =\"outline: 0px; font-size: 17px; color: rgb(51, 51, 51); font-family: Calibri; font-weight: 400; margin: 0px; padding: 0px;\">" + "<a href=\"" + UpdatedList[i].Link + "\"" + "style=\"outline: 0px; text-decoration: none; cursor: pointer; color: rgb(51, 51, 51);\">" + "" + UpdatedList[i].Title + "</a></h3>" + "By<span class=\"Apple-converted - space\"> </span><a class=\"LinkRed\"" + "href=\"http://www.c-sharpcorner.com/authors/ee01e6/manish-kumar-choudhary.aspx\"" + "style=\"outline: 0px; text-decoration: none; cursor: pointer; color: rgb(255, 102, 0); font-family: Calibri; font - weight: 400;\"><span class=\"Apple-converted-space\"> </span></a>< span" + "class=\"Apple-converted-space\"> </span> On " + "" + UpdatedList[i].PubDate + "</div>");


        //    sb.Append("</li></ul></div></div></div>");
        //}
        return sb.ToString();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        ServicePointManager.DefaultConnectionLimit = 9999;
        string filename = "https://jsil.com/wp-content/uploads/2022/02/JSIL_FMR_APR_2022.pdf";

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
}