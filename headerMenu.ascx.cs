using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class headerMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["FolioNo"] ==null)
        {
            Response.Redirect("signout.aspx");
        }
        //if(Session["USERID"]==null)
        //{
        //    Response.Redirect("signout.aspx");
        //}
    }

    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session["FolioNo"] = "";
        Session["TITLE"] = "";
        Response.Redirect("signout.aspx");
    }

    protected void ChangePasswordBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
}